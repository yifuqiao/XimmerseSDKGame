using UnityEngine;
using UnityEngine.UI;

public class UIScaleSlider:MonoBehaviour {

	#region Fields

	[SerializeField]protected Slider m_Slider;
	[SerializeField]protected Transform[] m_Transforms=new Transform[0];

	#endregion Fields

	#region Methods

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Awake() {
		if(m_Slider!=null) {
			m_Slider.onValueChanged.AddListener(UpdateValues);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void UpdateValues(float value) {
		Vector3 scale=Vector3.one*value;
		for(int i=0,imax=m_Transforms.Length;i<imax;++i) {
			if(m_Transforms[i]!=null) {
				m_Transforms[i].localScale=scale;
			}
		}
	}

	#endregion Methods

}