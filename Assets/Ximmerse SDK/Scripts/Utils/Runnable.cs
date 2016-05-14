using UnityEngine;
using Ximmerse;

/// <summary>
/// 
/// </summary>
public class Runnable:MonoBehaviour {

	#region Fields

	public UpdateType updateType=UpdateType.FixedUpdate;

	#endregion Fields

	#region Unity Messages

	/// <summary>
	/// 
	/// </summary>
	protected virtual void LateUpdate() {
		if(updateType==UpdateType.LateUpdate) {
			Run();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	protected virtual void FixedUpdate() {
		if(updateType==UpdateType.FixedUpdate) {
			Run();
		}
	}
	
	#endregion Unity Messages
	
	#region Methods

	/// <summary>
	/// 
	/// </summary>
	public virtual void Run() {
	}

	#endregion Methods

}