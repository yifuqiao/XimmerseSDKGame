#if !OVR_SDK&&!UVR_SDK
	#if UNITY_5 && !UNITY_5_0
		#define UVR_SDK
	#else
//		#define OVR_SDK
	#endif
#endif

using UnityEngine;
using UnityEngine.VR;

/// <summary>
/// 
/// </summary>
public class VRContext:MonoBehaviour {

	#region Nested Types

	public enum Node{
		None           = -1,
		LeftEye        = 0,
		RightEye       = 1,
		CenterEye      = 2,
		LeftHand       = 3,
		RightHand      = 4,
		TrackerDefault = 5,
		TrackingSpace  = 6,
		Count,
	}

	#endregion Nested Types

	#region Static

	/// <summary>
	/// 
	/// </summary>
	public static VRContext main;

	/// <summary>
	/// 
	/// </summary>
	public static VRContext Main {
		get {
			if(main==null) {
				main=FindObjectOfType<VRContext>();
				if(main!=null) {
					main.InitVRContext();
				}
			}
			return main;
		}
	}
	
	#endregion Static

	#region Static Methods

	/// <summary>
	/// 
	/// </summary>
	public static bool IsCenterEyeAnchor(Transform t) {
		if(Main==null) {
			return true;
		}
		return main.centerEyeAnchor==t;
	}

	/// <summary>
	/// 
	/// </summary>
	public static Transform GetNodeTransform(Node node) {
		if(Main==null) {
			return null;
		}
		return main.m_Anchors[(int)node];
	}
	
	/// <summary>
	/// 
	/// </summary>
	public static Matrix4x4 GetMatrixOnTrackingSpace(Transform anchor) {
		// Invalid child transform.
		if(anchor==null) {// ??
			return Matrix4x4.identity;
		}
		// Invalid VR trackingSpace.
		if(Main==null||main.trackingSpace==null) {// (Legacy)
			return anchor.localToWorldMatrix;
		}
		//
		return main.trackingSpace.worldToLocalMatrix*anchor.localToWorldMatrix;// M1*(M0*P0)
	}
	
	/// <summary>
	/// 
	/// </summary>
	public static Vector3 GetPositionOnTrackingSpace(Transform anchor,Vector3 position) {
		// Invalid child transform.
		if(anchor==null) {// ??
			return position;
		}
		// Invalid VR trackingSpace.
		if(Main==null||main.trackingSpace==null) {// (Legacy)
			return anchor.TransformPoint(position);
		}
		//
		return main.trackingSpace.InverseTransformPoint(anchor.TransformPoint(position));// M1*(M0*P0)
	}

	#endregion Static Methods

	#region Fields
	
	/// <summary>
	/// 
	/// </summary>
	[System.NonSerialized]protected Transform[] m_Anchors;
	
	[Header("Anchors")]

	/// <summary>
	/// 
	/// </summary>
	public Transform trackingSpace;
	
	/// <summary>
	/// 
	/// </summary>
	public Transform centerEyeAnchor;

	/// <summary>
	/// 
	/// </summary>
	public Transform leftHandAnchor;

	/// <summary>
	/// 
	/// </summary>
	public Transform rightHandAnchor;
	
	[Header("Misc")]

	public bool autoCreateHandAnchors=false;
	public KeyOrButton btnReset;
	public UnityEngine.Events.UnityAction onRecenter;

	[System.NonSerialized]protected bool m_IsInited=false;

	#endregion Fields

	#region Unity Messages

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Awake() {
		if(main==null) {
			main=this;
		}else if(main!=this) {
			Log.e("VRContext","Only one instance can be run!!!");
		}
		InitVRContext();
	}
	
	/// <summary>
	/// 
	/// </summary>
	protected virtual void Update(){
		if(btnReset.GetAnyDown()){

			#region Recenter

#if OVR_SDK
			OVRManager.display.RecenterPose();
#elif CARDBOARD_SDK
			Cardboard.SDK.Recenter();
#elif UVR_SDK
			InputTracking.Recenter();
#endif

			#endregion Recenter

			if(onRecenter!=null){
				onRecenter.Invoke();
			}
		}
	}

	#endregion Unity Messages

	#region Methods

	/// <summary>
	/// 
	/// </summary>
	protected virtual void InitVRContext() {
		//
		if(m_IsInited) {
			return;
		}
		m_IsInited=true;
		//
		if(trackingSpace==null&&centerEyeAnchor!=null) {
			trackingSpace=centerEyeAnchor.parent;
		}

		#region Anchors

		m_Anchors=new Transform[(int)Node.Count];
		m_Anchors[(int)Node.TrackingSpace]=trackingSpace;
		//m_Anchors[(int)Node.LeftEye]=leftEyeAnchor;
		//m_Anchors[(int)Node.RightEye]=rightEyeAnchor;
		m_Anchors[(int)Node.CenterEye]=centerEyeAnchor;
		m_Anchors[(int)Node.LeftHand]=leftHandAnchor;
		m_Anchors[(int)Node.RightHand]=rightHandAnchor;
		//m_Anchors[(int)Node.TrackerDefault]=trackerAnchor;

		#endregion Anchors

	}

	#endregion Methods

}