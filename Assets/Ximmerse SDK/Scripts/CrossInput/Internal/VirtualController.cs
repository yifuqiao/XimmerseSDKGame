using System.Collections.Generic;
using UnityEngine;

namespace Ximmerse.CrossInput{

	/// <summary>
	/// 
	/// </summary>
	public class VirtualController:IJoystick{

		#region Static

		public const int
			LEFT_HAND=0,
			RIGHT_HAND=1
		;
		
		public static VirtualController[] controllers=new VirtualController[4];

		/// <summary>
		/// 
		/// </summary>
		public static VirtualController Instantiate(
			string fmtAxis,string[] axes,
			string fmtButton,string[] buttons,
			string fmtPose,string[] poses,
			string fmtVibration,string[] vibrations
		){
			VirtualController vc=new VirtualController();
				//(new GameObject("New HandJoystick",
				//typeof(HandJoystick))).GetComponent<HandJoystick>();
			vc.InitInput(
				fmtAxis,axes,
				fmtButton,buttons,
				fmtPose,poses,
				fmtVibration,vibrations
			);
			return vc;
		}

		/// <summary>
		/// 
		/// </summary>
		public static int GetControllerIndex(int index,string name) {
			if(index>=controllers.Length) {// No supported index
				return -1;
			}
			name=name.ToLower();
			if(name.IndexOf("left")!=-1) {
				return 0;
			}
			if(name.IndexOf("right")!=-1) {
				return 1;
			}
			return -1;
		}

		#endregion Static

		#region CrossInput

		protected VirtualAxis[] m_Axes;
		protected VirtualButton[] m_Buttons;
		protected VirtualPose[] m_Poses;
		protected VirtualVibration[] m_Vibrations;

		protected Dictionary<string,VirtualAxis> m_MapAxes;
		protected Dictionary<string,VirtualButton> m_MapButtons;
		protected Dictionary<string,VirtualPose> m_MapPoses;
		protected Dictionary<string,VirtualVibration> m_MapVibrations;

		public virtual void InitInput(
			string fmtAxis,string[] axes,
			string fmtButton,string[] buttons,
			string fmtPose,string[] poses,
			string fmtVibration,string[] vibrations
		){
			int i,imax;
			//
			if(axes==null) {
				m_Axes=new VirtualAxis[0];m_MapAxes=new Dictionary<string,VirtualAxis>(0);
			}else {
				i=0;imax=axes.Length;
				m_Axes=new VirtualAxis[imax];m_MapAxes=new Dictionary<string,VirtualAxis>(imax);
				for(;i<imax;++i){
					m_Axes[i]=CrossInputManager.VirtualAxisReference(this,string.Format(fmtAxis,axes[i]),true);
					m_MapAxes.Add(axes[i],m_Axes[i]);// Add the raw input name.
				}
			}
			//
			if(buttons==null) {
				m_Buttons=new VirtualButton[0];m_MapButtons=new Dictionary<string,VirtualButton>(0);
			}else {
				i=0;imax=buttons.Length;
				m_Buttons=new VirtualButton[imax];m_MapButtons=new Dictionary<string,VirtualButton>(imax);
				for(;i<imax;++i){
					m_Buttons[i]=CrossInputManager.VirtualButtonReference(this,string.Format(fmtButton,buttons[i]),true);
					m_MapButtons.Add(buttons[i],m_Buttons[i]);// Add the raw input name.
				}
			}
			//
			if(poses==null) {
				m_Poses=new VirtualPose[0];m_MapPoses=new Dictionary<string,VirtualPose>(0);
			}else {
				i=0;imax=poses.Length;
				m_Poses=new VirtualPose[imax];m_MapPoses=new Dictionary<string,VirtualPose>(imax);
				for(;i<imax;++i){
					m_Poses[i]=CrossInputManager.VirtualPoseReference(this,string.Format(fmtPose,poses[i]),true);
					m_MapPoses.Add(poses[i],m_Poses[i]);// Add the raw input name.
				}
			}
			//
			if(vibrations==null) {
				m_Vibrations=new VirtualVibration[0];m_MapVibrations=new Dictionary<string,VirtualVibration>(0);
			}else {
				i=0;imax=vibrations.Length;
				m_Vibrations=new VirtualVibration[imax];m_MapVibrations=new Dictionary<string,VirtualVibration>(imax);
				for(;i<imax;++i){
					m_Vibrations[i]=CrossInputManager.VirtualVibrationReference(this,string.Format(fmtVibration,vibrations[i]),true);
					m_MapVibrations.Add(vibrations[i],m_Vibrations[i]);// Add the raw input name.
				}
			}
		}

		#endregion CrossInput

		#region IJoystick
		
		/// <summary>
		/// 
		/// </summary>
		public virtual float GetAxis(string axisName){
			if(!m_MapAxes.ContainsKey(axisName)) return 0.0f;
			return m_MapAxes[axisName].GetValue;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual float GetAxisRaw(string axisName){
			if(!m_MapAxes.ContainsKey(axisName)) return 0.0f;
			return m_MapAxes[axisName].GetValueRaw;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual bool GetButton(string buttonName){
			if(!m_MapButtons.ContainsKey(buttonName)) return false;
			return m_MapButtons[buttonName].GetButton;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual bool GetButtonDown(string buttonName){
			if(!m_MapButtons.ContainsKey(buttonName)) return false;
			return m_MapButtons[buttonName].GetButtonDown;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual bool GetButtonUp(string buttonName){
			if(!m_MapButtons.ContainsKey(buttonName)) return false;
			return m_MapButtons[buttonName].GetButtonUp;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual float GetAxis(int axisId){
			return m_Axes[axisId].GetValue;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual float GetAxisRaw(int axisId){
			return m_Axes[axisId].GetValueRaw;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual bool GetButton(int buttonId){
			return m_Buttons[buttonId].GetButton;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual bool GetButtonDown(int buttonId){
			return m_Buttons[buttonId].GetButtonDown;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual bool GetButtonUp(int buttonId){
			return m_Buttons[buttonId].GetButtonUp;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual int GetButtonFrameCount(string buttonName) {
			if(!m_MapButtons.ContainsKey(buttonName)) return -1;
			return m_MapButtons[buttonName].GetButtonFrameCount;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual int GetButtonFrameCount(int buttonId) {
			return m_Buttons[buttonId].GetButtonFrameCount;
		}

		#endregion IJoystick

		#region Motion Supports

		/// <summary>
		/// 
		/// </summary>
		public virtual VirtualPose GetPose(string poseName) {
			if(!m_MapPoses.ContainsKey(poseName)) return null;
			return m_MapPoses[poseName];
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual VirtualPose GetPose(int poseId) {
			return m_Poses[poseId];
		}

		#endregion Motion Supports

		#region Vibration Supports

		/// <summary>
		/// 
		/// </summary>
		public virtual void SetVibration(string vibrationName,int waveType,float delay,float duration) {
			if(!m_MapVibrations.ContainsKey(vibrationName)) return;
			m_MapVibrations[vibrationName].SetVibration(waveType,delay,duration);
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void SetVibration(int vibrationId,int waveType,float delay,float duration) {
			m_Vibrations[vibrationId].SetVibration(waveType,delay,duration);
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void StartVibration(string vibrationName,int waveType) {
			if(!m_MapVibrations.ContainsKey(vibrationName)) return;
			m_MapVibrations[vibrationName].StartVibration(waveType);
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void StartVibration(int vibrationId,int waveType) {
			m_Vibrations[vibrationId].StartVibration(waveType);
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void StopVibration(string vibrationName) {
			if(!m_MapVibrations.ContainsKey(vibrationName)) return;
			m_MapVibrations[vibrationName].StopVibration();
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void StopVibration(int vibrationId) {
			m_Vibrations[vibrationId].StopVibration();
		}

		#endregion Vibration Supports

		#region Properties

		protected VirtualPose m_DefaultPose;

		/// <summary>
		/// 
		/// </summary>
		public virtual VirtualPose pose {
			get {
				if(m_DefaultPose==null) {
					if(m_Poses!=null&&m_Poses.Length>0) {
						m_DefaultPose=m_Poses[0];
					}else {
						m_DefaultPose=new VirtualPose("Unidentified");
					}
				}
				return m_DefaultPose;
			}
		}

		protected VirtualVibration m_DefaultVibration;

		/// <summary>
		/// 
		/// </summary>
		public virtual VirtualVibration vibration {
			get {
				if(m_DefaultVibration==null) {
					if(m_Vibrations!=null&&m_Vibrations.Length>0) {
						m_DefaultVibration=m_Vibrations[0];
					}else {
						m_DefaultVibration=new VirtualVibration("Unidentified");
					}
				}
				return m_DefaultVibration;
			}
		}
		
		#endregion Properties

	}

}