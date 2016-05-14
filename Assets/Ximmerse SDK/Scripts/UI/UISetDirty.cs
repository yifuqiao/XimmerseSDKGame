using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fix the issue that Animation couldn't update UnityEngine.UI.Graphic's properties. 
/// </summary>
public class UISetDirty:MonoBehaviour {

	#region Fields

	[System.NonSerialized]protected Graphic m_Graphic;

	#endregion Fields

	#region Unity Messages

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Start() {
		m_Graphic=GetComponent<Graphic>();
	}

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Update() {
		// Animation couldn't call UnityEngine.UI.Graphic.Set****Dirty().
		if(m_Graphic!=null) {
			m_Graphic.SetAllDirty();
		}
	}

	#endregion Unity Messages

}