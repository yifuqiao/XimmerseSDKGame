#if !UNITY_EDITOR&&UNITY_ANDROID

using UnityEngine;

namespace Ximmerse.Core {

	/// <summary>
	/// For Android building.
	/// </summary>
	public partial class LibXHawkAPI {

		#region Nested Types

		public class UsbEventCallback{//:AndroidJavaProxy {

			#region Fields

			protected StringDelegate m_OnUsbDeviceAttached,m_OnUsbDeviceDetached;

			#endregion Fields

			#region Methods 

			/// <summary>
			/// 
			/// </summary>
			public UsbEventCallback(){//:base("com.ximmerse.usb.IUsbEventCallback") {
			}

			/// <summary>
			/// 
			/// </summary>
			public virtual void SetOnAttachedListener(StringDelegate onUsbDeviceAttached) {
				m_OnUsbDeviceAttached=onUsbDeviceAttached;
			}

			/// <summary>
			/// 
			/// </summary>
			public virtual void SetOnDetachedListener(StringDelegate onUsbDeviceDetached) {
				m_OnUsbDeviceDetached=onUsbDeviceDetached;
			}

			/// <summary>
			/// 
			/// </summary>
			public virtual void onUsbDeviceAttached(AndroidJavaObject device) {
				if(m_OnUsbDeviceAttached!=null) {
					m_OnUsbDeviceAttached.Invoke("onUsbDeviceAttached");
				}
			}

			/// <summary>
			/// 
			/// </summary>
			public virtual void onUsbDeviceDetached(AndroidJavaObject device) {
				if(m_OnUsbDeviceDetached!=null) {
					m_OnUsbDeviceDetached.Invoke("onUsbDeviceDetached");
				}
			}

			#endregion Methods

		}
		
		#endregion Nested Types

		#region Static Fields

		/// <summary>
		/// X-Hawk library name.
		/// </summary>
		public const string LIB_XHAWK="xhawk";

		protected static AndroidJavaClass s_XHawkApi=new AndroidJavaClass("com.ximmerse.core.XHawkApi");

		protected static UsbEventCallback s_UsbEventCallback=new UsbEventCallback();

		#endregion Static Fields

		#region Static Methods

		/// <summary>
		/// Init library.
		/// </summary>
		public static int Init() {
			int ret=s_XHawkApi.CallStatic<int>("init",Android.App.Activity.currentActivity.unityPtr);
			//s_XHawkApi.CallStatic("setUsbEventListener",m_UsbEventCallback);
			return ret;
		}

		/// <summary>
		/// Exit library.
		/// </summary>
		public static int Exit() {
			return s_XHawkApi.CallStatic<int>("exit");
		}

		/// <summary>
		/// 
		/// </summary>
		public static int XHawkSendMessage(int index,int Msg,int wParam,int lParam) {
			return s_XHawkApi.CallStatic<int>("sendMessage",index,Msg,wParam,lParam);
		}

		/// <summary>
		/// 
		/// </summary>
		public static int XHawkSetOnAttachedListener(StringDelegate callback){
			s_UsbEventCallback.SetOnAttachedListener(callback);
			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		public static int XHawkSetOnDetachedListener(StringDelegate callback){
			s_UsbEventCallback.SetOnDetachedListener(callback);
			return 0;
		}

		#endregion Static Methods

	}

}

#endif