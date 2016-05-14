using UnityEngine;

/// <summary>
/// Set Ximmerse.Log.dll's LogLevel.
/// </summary>
public class LogLevel:MonoBehaviour {

	public bool v=true;
	public bool i=true;
	public bool w=true;
	public bool e=true;

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Awake() {
		//
		int mask=0;
		if(v)mask|=Log.k_Filter_v;
		if(i)mask|=Log.k_Filter_i;
		if(w)mask|=Log.k_Filter_w;
		if(e)mask|=Log.k_Filter_e;
		//
		Log.s_Filter=mask;
	}

}
