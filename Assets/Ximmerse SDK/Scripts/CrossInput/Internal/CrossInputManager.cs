using System;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput.PlatformSpecific;

namespace Ximmerse.CrossInput/*UnityStandardAssets.CrossPlatformInput*/ {

	/// <summary>
	/// 
	/// </summary>
	public partial/*static*/ class CrossInputManager {
		//public enum ActiveInputMethod {
		//	Hardware,
		//	Touch
		//}


		public static VirtualInput activeInput;

		// <!-- TODO

		/// <summary>
		/// 
		/// </summary>
		public static int timestamp;


		// returns a reference to a named virtual button if it exists otherwise null
		public static VirtualButton VirtualButtonReference(string name) {
			return activeInput.VirtualButtonReference(name);
		}


		// returns a reference to a named virtual axis.
		public static VirtualAxis VirtualAxisReference(object caller,string name,bool canAdd=false) {
			VirtualAxis item=null;
			if(AxisExists(name)){
				item=VirtualAxisReference(name);
			}else{
				RegisterVirtualAxis(item=new VirtualAxis(name));
			}
			if(item!=null){
				item.AddRef(caller);
			}
			return item;
		}


		// returns a reference to a named virtual button.
		public static VirtualButton VirtualButtonReference(object caller,string name,bool canAdd=false) {
			VirtualButton item=null;
			if(ButtonExists(name)){
				item=VirtualButtonReference(name);
			}else{
				RegisterVirtualButton(item=new VirtualButton(name));
			}
			if(item!=null){
				item.AddRef(caller);
			}
			return item;
		}

		#region Vibration Supports


		/// <summary>
		/// 
		/// </summary>
		public static void RegisterVirtualVibration(VirtualVibration vibration) {
			activeInput.RegisterVirtualVibration(vibration);
		}
		
		/// <summary>
		/// 
		/// </summary>
		public static void UnRegisterVirtualVibration(string name) {
			activeInput.UnRegisterVirtualVibration(name);
		}


		// returns a reference to a named virtual vibration.
		public static VirtualVibration VirtualVibrationReference(object caller,string name,bool canAdd=false) {
			VirtualVibration item=null;
			if(VibrationExists(name)){
				item=VirtualVibrationReference(name);
			}else{
				RegisterVirtualVibration(item=new VirtualVibration(name));
			}
			if(item!=null){
				item.AddRef(caller);
			}
			return item;
		}


		// returns a reference to a named virtual vibration if it exists otherwise null
		public static VirtualVibration VirtualVibrationReference(string name) {
			return activeInput.VirtualVibrationReference(name);
		}

		public static bool VibrationExists(string name) {
			return activeInput.VibrationExists(name);
		}

		public static void SetVibration(string name,int waveType,float delay,float duration) {
			activeInput.SetVibration(name,waveType,delay,duration);
		}

		public static void StartVibration(string name,int waveType) {
			activeInput.StartVibration(name,waveType);
		}

		public static void StopVibration(string name) {
			activeInput.StopVibration(name);
		}

		#endregion Vibration Supports
		
		// TODO -->

		//private static VirtualInput s_TouchInput;
		//private static VirtualInput s_HardwareInput;


//		static CrossPlatformInputManager() {
//			s_TouchInput=new MobileInput();
//			s_HardwareInput=new StandaloneInput();
//#if MOBILE_INPUT
//			activeInput = s_TouchInput;
//#else
//			activeInput=s_HardwareInput;
//#endif
//		}

		//public static void SwitchActiveInputMethod(ActiveInputMethod activeInputMethod) {
		//	switch(activeInputMethod) {
		//		case ActiveInputMethod.Hardware:
		//		activeInput=s_HardwareInput;
		//		break;

		//		case ActiveInputMethod.Touch:
		//		activeInput=s_TouchInput;
		//		break;
		//	}
		//}

		public static bool AxisExists(string name) {
			return activeInput.AxisExists(name);
		}

		public static bool ButtonExists(string name) {
			return activeInput.ButtonExists(name);
		}

		public static void RegisterVirtualAxis(VirtualAxis axis) {
			activeInput.RegisterVirtualAxis(axis);
		}


		public static void RegisterVirtualButton(VirtualButton button) {
			activeInput.RegisterVirtualButton(button);
		}


		public static void UnRegisterVirtualAxis(string name) {
			if(name==null) {
				throw new ArgumentNullException("name");
			}
			activeInput.UnRegisterVirtualAxis(name);
		}


		public static void UnRegisterVirtualButton(string name) {
			activeInput.UnRegisterVirtualButton(name);
		}


		// returns a reference to a named virtual axis if it exists otherwise null
		public static VirtualAxis VirtualAxisReference(string name) {
			return activeInput.VirtualAxisReference(name);
		}


		// returns the platform appropriate axis for the given name
		public static float GetAxis(string name) {
			return GetAxis(name,false);
		}


		public static float GetAxisRaw(string name) {
			return GetAxis(name,true);
		}


		// private function handles both types of axis (raw and not raw)
		private static float GetAxis(string name,bool raw) {
			return activeInput.GetAxis(name,raw);
		}


		// -- Button handling --
		public static bool GetButton(string name) {
			return activeInput.GetButton(name);
		}


		public static bool GetButtonDown(string name) {
			return activeInput.GetButtonDown(name);
		}


		public static bool GetButtonUp(string name) {
			return activeInput.GetButtonUp(name);
		}


		public static int GetButtonFrameCount(string name) {
			return activeInput.GetButtonFrameCount(name);
		}


		public static void SetButtonDown(string name) {
			activeInput.SetButtonDown(name);
		}


		public static void SetButtonUp(string name) {
			activeInput.SetButtonUp(name);
		}


		public static void SetAxisPositive(string name) {
			activeInput.SetAxisPositive(name);
		}


		public static void SetAxisNegative(string name) {
			activeInput.SetAxisNegative(name);
		}


		public static void SetAxisZero(string name) {
			activeInput.SetAxisZero(name);
		}


		public static void SetAxis(string name,float value) {
			activeInput.SetAxis(name,value);
		}


		public static Vector3 mousePosition {
			get {
				return activeInput.MousePosition();
			}
		}


		public static void SetVirtualMousePositionX(float f) {
			activeInput.SetVirtualMousePositionX(f);
		}


		public static void SetVirtualMousePositionY(float f) {
			activeInput.SetVirtualMousePositionY(f);
		}


		public static void SetVirtualMousePositionZ(float f) {
			activeInput.SetVirtualMousePositionZ(f);
		}
	}
}
