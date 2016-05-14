using UnityEngine;

namespace Ximmerse.CrossInput{

	/// <summary>
	///  
	/// </summary>
	public class InputMapSetting:ScriptableObject{

		#region Fields

		[System.NonSerialized]protected VirtualAxis[] m_VirtualAxes;
		[System.NonSerialized]protected VirtualButton[] m_VirtualButtons;
		[System.NonSerialized]protected VirtualPose[] m_VirtualPoses;
		[System.NonSerialized]protected VirtualVibration[] m_VirtualVibrations;
		[System.NonSerialized]protected bool[] m_IsPress;
		[System.NonSerialized]protected string[] m_Axes,m_Buttons,m_Poses,m_Vibrations;
		
		#endregion Fields

		#region Methods
		
		/// <summary>
		/// 
		/// </summary>
		public virtual void InitInput(Object context,string fmt){
			string[] entries;
			int i,imax;
			//
			entries=GetAxisStrings();
			i=0;imax=entries.Length;
			m_VirtualAxes=new VirtualAxis[imax];
			for(;i<imax;++i){
				m_VirtualAxes[i]=CrossInputManager.VirtualAxisReference(context,string.Format(fmt,entries[i],true));
			}
			//
			entries=GetButtonStrings();
			i=0;imax=entries.Length;
			m_VirtualButtons=new VirtualButton[imax];
			for(;i<imax;++i){
				m_VirtualButtons[i]=CrossInputManager.VirtualButtonReference(context,string.Format(fmt,entries[i],true));
			}
			m_IsPress=new bool[imax];
			//
			entries=GetPoseStrings();
			i=0;imax=entries.Length;
			m_VirtualPoses=new VirtualPose[imax];
			for(;i<imax;++i){
				m_VirtualPoses[i]=CrossInputManager.VirtualPoseReference(context,string.Format(fmt,entries[i],true));
				m_VirtualPoses[i].SetMainSupplier(context);//
			}
			//
			entries=GetVibrationStrings();
			i=0;imax=entries.Length;
			m_VirtualVibrations=new VirtualVibration[imax];
			for(;i<imax;++i){
				m_VirtualVibrations[i]=CrossInputManager.VirtualVibrationReference(context,string.Format(fmt,entries[i],true));
				//m_VirtualVibrations[i].SetMainSupplier(context);//
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void ResetInput(){
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void UpdateInput(){
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual string[] GetAxisStrings(){
			return null;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual string[] GetButtonStrings(){
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual string[] GetPoseStrings(){
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual string[] GetVibrationStrings(){
			return null;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual VirtualController ToVirtualController(string fmt) {
			return VirtualController.Instantiate(
				fmt,GetAxisStrings(),
				fmt,GetButtonStrings(),
				fmt,GetPoseStrings(),
				fmt,GetVibrationStrings()
			);
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual T Clone<T>()where T:InputMapSetting{
			return Object.Instantiate(this) as T;
		}
		
		#endregion Methods

	}
}