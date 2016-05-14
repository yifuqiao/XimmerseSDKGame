using UnityEngine;
using Ximmerse.Core;

namespace Ximmerse.CrossInput {

	/// <summary>
	/// 
	/// </summary>
	public class ScaledPoseTransform:PoseTransform {

		#region Fields

		[Header("Scale")]
		[SerializeField]protected bool m_UseScale=true;
		[SerializeField]protected AnimationCurve m_CurveScaleZ=AnimationCurve.Linear(0f,1f,1f,1f);
		[SerializeField]protected AnimationCurve m_CurveOffsetZ=AnimationCurve.Linear(0f,0f,1f,0f);

		#endregion Fields

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		public override bool GetPose(out Vector3 position,out Quaternion rotation) {
			//
			if(!m_UseScale) {
				return base.GetPose(out position,out rotation);
			}
			//
			switch(m_Pose.trackingType) {
				case InputTrackingType.Ximmerse:
					position=m_Pose.rawPosition;
					position.z=m_CurveScaleZ.Evaluate(position.z)*position.z+m_CurveOffsetZ.Evaluate(position.z);
					position=XHawkInput.TransformPoint(position);
				break;
				default:
					Log.w("ScaledPoseTransform","No a scale method for InputTrackingType."+m_Pose.trackingType);
					position=m_Pose.position;
				break;
			}
			rotation=m_Pose.rotation;
			return true;
		}

		#endregion Methods

	}
}
