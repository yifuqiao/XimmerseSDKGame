using UnityEngine;
using Ximmerse;

/// <summary>
/// 
/// </summary>
public class YawCopier:Runnable {

	#region Fields

	public Transform destination,source;

	#endregion Fields

	#region Methods

	/// <summary>
	/// 
	/// </summary>
	public override void Run() {
		Vector3 forward=source.rotation*Vector3.forward;forward.y=0.0f;
		destination.rotation=Quaternion.LookRotation(forward,Vector3.up);
	}

	#endregion Methods

}