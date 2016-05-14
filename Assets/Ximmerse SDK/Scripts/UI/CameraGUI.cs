using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class CameraGUI:MonoBehaviour {

	#region Fields

	[SerializeField]protected Camera m_Camera;
	[SerializeField]protected Toggle m_Opt_orthographic;
	[SerializeField]protected Slider m_Opt_orthographicSize;
	[SerializeField]protected Slider m_Opt_fieldOfView;

	#endregion Fields

	#region Methods

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Awake() {
		if(m_Camera==null) m_Camera=Camera.main;
		//
		if(m_Opt_orthographic!=null) {
			m_Opt_orthographic.isOn=m_Camera.orthographic;
			m_Opt_orthographic.onValueChanged.AddListener((x)=>{UpdateValues();});
		}
		//
		if(m_Opt_orthographicSize!=null) {
			m_Opt_orthographicSize.value=m_Camera.orthographicSize;
			m_Opt_orthographicSize.onValueChanged.AddListener((x)=>{UpdateValues();});
		}
		//
		if(m_Opt_fieldOfView!=null) {
			m_Opt_fieldOfView.value=m_Camera.fieldOfView;
			m_Opt_fieldOfView.onValueChanged.AddListener((x)=>{UpdateValues();});
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void UpdateValues() {
		//
		if(m_Opt_orthographic!=null) {
			m_Camera.orthographic=m_Opt_orthographic.isOn;
		}
		//
		if(m_Opt_orthographicSize!=null) {
			m_Camera.orthographicSize=m_Opt_orthographicSize.value;
		}
		//
		if(m_Opt_fieldOfView!=null) {
			m_Camera.fieldOfView=m_Opt_fieldOfView.value;
		}
	}

	#endregion Methods

}