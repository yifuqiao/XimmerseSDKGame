using UnityEngine;

namespace Ximmerse.Core {

	/// <summary>
	/// XHawkInput provides an interface for accessing X-Cobra controllers.
	/// </summary>
	/// <remarks>
	/// This script should be bound to a GameObject in the scene so that its Awake(), Update() and OnDestroy() methods are called.This can be done by adding the XHawkInput prefab to a scene.
	/// The public static interface to the XCobraController objects provides a user friendly way to integrate X-Cobra controllers into your application.
	/// </remarks>
	public partial class XHawkInput:MonoBehaviour {

		#region Static

		public static XHawkInput main;

		/// <summary>
		/// Max number of X-Cobra controllers allowed by X-Hawk.
		/// </summary>
		public const int MAX_CONTROLLERS = 2;

		/// <summary>
		/// 
		/// </summary>
		protected static XCobraController[] m_Controllers = new XCobraController[MAX_CONTROLLERS];

		/// <summary>
		/// Access to XCobraController objects.
		/// </summary>
		public static XCobraController[] Controllers { get { return m_Controllers; } }

		// <summary>
		/// Gets the XCobraController object bound to the specified hand.
		/// </summary>
		public static XCobraController GetController(XCobraHands hand) {
			for(int i = 0;i<MAX_CONTROLLERS;i++) {
				if((m_Controllers[i]!=null)&&(m_Controllers[i].Hand==hand)) {
					return m_Controllers[i];
				}
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		public static Vector3 TransformPoint(Vector3 position) {
			if(main==null) {
				return position;
			}else {
				return VRContext.GetMatrixOnTrackingSpace(main.pivot).MultiplyPoint3x4(position);
			}
		}

		#region Swap

		// These functions will be deprecated in the future.

		// <!--

		/// <summary>
		/// Left XCobraController swap data with right XCobraController.
		/// </summary>
		public static void SwapControllers() {
			LibXHawkAPI.SwapControllers(true,true);
		}
	
		/// <summary>
		///  Left XCobraController swap blob data with right XCobraController.
		/// </summary>
		public static void SwapBlobs() {
			LibXHawkAPI.SwapControllers(true,false);
		}
	
		/// <summary>
		/// Left XCobraController swap ble data with right XCobraController.
		/// </summary>
		public static void SwapBles() {
			LibXHawkAPI.SwapControllers(false,true);
		}

		// -->

		#endregion Swap

		#endregion Static

		#region Fields
		
		[Header("Tracking")]

		/// <summary>
		/// 
		/// </summary>
		[SerializeField]protected Transform m_Pivot;
		public Vector3 center=Vector3.zero;
		public Vector3 sensitivity=Vector3.one*0.001f;// X-Hawk units are in mm
		
		/// <summary>
		/// 
		/// </summary>
		public bool resetXCobraOnLoad=true;

		/// <summary>
		/// When it is true,it will hide X-Cobra automatically when it lose position tracking.
		/// </summary>
		public bool autoHideXCobra=true;

		/// <summary>
		/// When autoHideXCobra is true and lose X-Cobra's position tracking,it will set X-Cobra's position to this value.
		/// </summary>
		public Vector3 hiddenPoint=Vector3.one*-1024;
		
		[Header("Input")]
		
		/// <summary>
		/// (Will be deprecated)
		/// </summary>
		public bool swapBlobs=false;
		
		/// <summary>
		/// (Will be deprecated)
		/// </summary>
		public bool swapBles=false;

		/// <summary>
		/// 
		/// </summary>
		public float axisDeadzone=0.1f;

		protected LibXHawkAPI.joyinfo[] m_JoyInfos;

		protected XHawkInputGUI m_GUI;

		#endregion Fields

		#region Unity Messages

		/// <summary>
		///  Initialize the X-Hawk and allocate the X-Cobra Controllers.
		/// </summary>
		protected virtual void Awake() {
			//
			m_GUI=XHawkInputGUI.Main;
			//
			int ret=LibXHawkAPI.Init();
			m_JoyInfos=new LibXHawkAPI.joyinfo[2];
			int i=0;
			for(;i<MAX_CONTROLLERS;++i) {
				m_JoyInfos[i].Init();
				m_Controllers[i]=new XCobraController();
				m_Controllers[i].Awake(this);
				m_Controllers[i].HandBind=(XCobraHands)i;// Modify the hand type.
			}
			//
			if(ret==0) {
				OnInitializeSuccess();
			}else {
				OnInitializeFailure();
				return;
			}
			//
			LibXHawkAPI.XHawkSetOnAttachedListener(OnDeviceAttached);
			LibXHawkAPI.XHawkSetOnDetachedListener(OnDeviceDetached);
			// Setting.
			float[] floats=new float[3];
			  // Position Offset.
			i=3;while(i-->0){floats[i]=center[i];}
			LibXHawkAPI.XHawkSetPositionOffset(floats);
			  // Swa pControllers.
			LibXHawkAPI.SwapControllers(swapBlobs,swapBles);
			//
			if(main==null) {
				main=this;
			}else if(main!=this){
				Log.e("XHawkInput","Only one instance can be running.");
			}
			//
			if(resetXCobraOnLoad) {
				Juggler.Main.DelayCall(ResetControllers,.1f);
			}
		}

		/// <summary>
		///  Update the static controller data once per frame.
		/// </summary>
		protected virtual void Update() {
			int ret=LibXHawkAPI.Update(m_JoyInfos);
			//Log.i("XHawkInput",ret.ToString());
			if(ret==0) {
				for(int i=0;i<MAX_CONTROLLERS;++i) {
					m_Controllers[i].Update(ref m_JoyInfos[i]);
				}
			}
		}

		/// <summary>
		/// Exit X-Hawk library.
		/// </summary>
		protected virtual void OnDestroy() {
			LibXHawkAPI.XHawkSetOnAttachedListener(null);
			LibXHawkAPI.XHawkSetOnDetachedListener(null);
			LibXHawkAPI.Exit();
		}

		#endregion Unity Messages

		#region Methods
		
		/// <summary>
		/// 
		/// </summary>
		public virtual void OnInitializeSuccess() {
			if(m_GUI!=null) {
				m_GUI.ShowMsgBox("Initialize Success");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual void OnInitializeFailure() {
			enabled=false;
			if(m_GUI!=null) {
				m_GUI.ShowMsgBox("Initialize Failure");
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual void OnDeviceAttached(string msg) {
			Log.i("XHawkInput",msg);
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual void OnDeviceDetached(string msg) {
			Log.i("XHawkInput",msg);
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual void ResetControllers(){
			for(int i=0;i<MAX_CONTROLLERS;++i) {
				m_Controllers[i].ResetRotation();
			}
		}
		
		#endregion Methods

		#region Properties

		/// <summary>
		/// The X-Hawk Transform in Unity3D.
		/// </summary>
		public Transform pivot {
			get {
				//
				if(m_Pivot==null) {
					//
					string pivotName="X-Hawk Anchor";
					VRContext vrCtx=VRContext.Main;
					if(vrCtx==null || vrCtx.centerEyeAnchor==null) {
						m_Pivot=new GameObject(pivotName).transform;
					}else {
						m_Pivot=vrCtx.centerEyeAnchor.FindChild(pivotName);
						if(m_Pivot==null) {
							m_Pivot=new GameObject(pivotName).transform;
						}
						m_Pivot.SetParent(vrCtx.centerEyeAnchor);
						m_Pivot.ResetLocal();
					}
					//DontDestroyOnLoad(m_Pivot);
				}
				return m_Pivot;
			}
		}

		#endregion Properties

	}

}