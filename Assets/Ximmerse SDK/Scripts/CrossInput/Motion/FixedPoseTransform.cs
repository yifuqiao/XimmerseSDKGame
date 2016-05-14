using UnityEngine;
using Ximmerse.Core;
using Ximmerse.CrossInput;

public class FixedPoseTransform:PoseTransform {

	#region Fields

	[Header("Fix It")]
	[SerializeField]protected float m_StartFixDis=1.0f;
	
	#endregion Fields

	#region Methods

	/// <summary>
	/// 
	/// </summary>
	public override bool GetPose(out Vector3 position,out Quaternion rotation) {
		if(m_Pose.rawPosition.z*m_Pose.rawPosition.z>=m_StartFixDis*m_StartFixDis) {
			position=m_Pose.rawPosition;
			position=Vector3.Normalize(position)*m_StartFixDis;
			position=XHawkInput.TransformPoint(position);
		}else {
			position=m_Pose.position;
		}
		rotation=m_Pose.rotation;
		return true;
	}

	#endregion Methods

}