using UnityEngine;
using UnityEngine.UI;

namespace Ximmerse.Core {

	//

	public partial class XHawkInput {
		
		[Header("Correction")]
		[SerializeField]protected bool m_UseCorrection=false;
		[SerializeField]protected bool m_IsCorrection=false;
		[SerializeField]protected GameObject m_CorrectionTextGo;

		public KeyOrButton btnCorrection=new KeyOrButton(
			new KeyCode[0],
			new string[2]{"Left_Start","Right_Start"}
		);

		/// <summary>
		/// 
		/// </summary>
		protected virtual void LateUpdate() {
			if(!m_UseCorrection) {
				return;
			}
			if(m_IsCorrection) {
				if(btnCorrection.GetAnyDown()) {
					ExitCorrection();
				}else {
					DoCorrection();
				}
			}else {
				if(btnCorrection.GetAnyFrameCountGreater((int)(/*Application.targetFrameRate*/60*1.5f))) {
					EnterCorrection();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void EnterCorrection() {
			m_IsCorrection=true;
			EnableCorrectionUI(true);
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void ExitCorrection() {
			m_IsCorrection=false;
			EnableCorrectionUI(false);
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void EnableCorrectionUI(bool value) {
			//
			if(m_CorrectionTextGo==null) {
				m_CorrectionTextGo=m_GUI.msgCorrection;
			}
			//
			if(m_CorrectionTextGo!=null) {
				if(value) {
					Text text=m_CorrectionTextGo.GetComponentInChildren<Text>();
					if(text!=null) {
						if(string.IsNullOrEmpty(text.text)) {
							text.text="Hold two X-Cobras in front of X-Hawk,and press the Left X-Cobra's Trigger.";
						}
					}
				}
				m_CorrectionTextGo.SetActive(value);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void DoCorrection() {
			if((m_Controllers[0].State&XCobraState.POSITION_FOUND)!=0 &&(m_Controllers[1].State&XCobraState.POSITION_FOUND)!=0 ) {
				for(int i=0,imax=m_Controllers.Length;i<imax;++i) {
					if(m_Controllers[i].GetButtonDown(XCobraButtons.Trigger)) {// When Left_Trigger is down.
						LibXHawkAPI.SwapControllers(
							// Swap blob data.
							m_Controllers[0].PositionRaw.x>m_Controllers[1].PositionRaw.x,
							// Swap ble data.
							m_Controllers[i].HandBind!=XCobraHands.LEFT
						);
						//
						Juggler.Main.DelayCall(DoCorrectionDelayed,.1f);
						ExitCorrection();
						return;
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void DoCorrectionDelayed() {
			for(int i=0,imax=m_Controllers.Length;i<imax;++i) {
				m_Controllers[i].ResetRotation();
			}
		}

	}

}