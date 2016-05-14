using UnityEngine;

namespace Ximmerse.Core {

	/// <summary>
	/// 
	/// </summary>
	public class XHawkInputGUI:MonoBehaviour {

		#region Static

		public static XHawkInputGUI main;

		public static XHawkInputGUI Main {
			get {
				if(main==null) {
					GameObject prefab=Resources.Load<GameObject>("XHawkInputGUI");
					if(prefab!=null) {
						main=Instantiate(prefab).GetComponent<XHawkInputGUI>();
						main.InitGUI(prefab);
					} else {
					}
				}
				return main;
			}
		}

		#endregion Static

		#region Fields

		public GameObject msgInitSuccess,msgInitFailure;
		public GameObject msgCorrection;
		[System.NonSerialized]protected GameObject m_MsgBoxPrev;

		#endregion Fields

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		public virtual void InitGUI(GameObject origin) {
			SetActive(msgInitSuccess,false);
			SetActive(msgInitFailure,false);
			SetActive(msgCorrection,false);
			//
			name=(origin==null)?name:origin.name;
			// Transform
			Transform t=main.transform;
			Transform parent=VRContext.GetNodeTransform(VRContext.Node.CenterEye);
			bool isVRMode=(origin!=null&&parent!=null);
			Vector3 position=isVRMode?origin.transform.localPosition:Vector3.zero;
			Vector3 scale=isVRMode?origin.transform.localScale:Vector3.one;
			if(parent==null) {
				GetComponent<Canvas>().renderMode=RenderMode.ScreenSpaceOverlay;
			}else {
				t.SetParent(parent);
				t.ResetLocal();
			}
			t.localPosition=position;
			t.localScale=scale;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void SetActive(GameObject go,bool value) {
			if(go!=null) {
				go.SetActive(value);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void ShowMsgBox(string msg) {
			switch(msg) {
				case "Initialize Success":
					ShowMsgBox(msgInitSuccess);
				break;
				case "Initialize Failure":
					ShowMsgBox(msgInitFailure);
				break;
				default:
				break;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void ShowMsgBox(GameObject msgBox) {
			if(m_MsgBoxPrev!=null) {
				m_MsgBoxPrev.SetActive(false);
				m_MsgBoxPrev=null;
			}
			SetActive(msgBox,true);
			m_MsgBoxPrev=msgBox;
		}

		#endregion Methods

	}
}
