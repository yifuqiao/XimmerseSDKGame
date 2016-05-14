using UnityEngine;

namespace Ximmerse.CrossInput {

	/// <summary>
	/// A example how to display VirtualPose in the Unity scene.
	/// </summary>
	public class PoseTransform:MonoBehaviour {

		#region Nested Types

		/// <summary>
		/// 
		/// </summary>
		public enum HideMethod:int {
			None,
			Position,
			ActivateList,
			Custom,
			Count
		}

		#endregion Nested Types

		#region Fields

		[SerializeField]protected Transform m_Transform;
		public string poseName;

		[Header("Setting")]

		public bool useSmooth=false;

		public bool usePosition=true;
		public float smoothPosition=.5f;

		public bool useRotation=true;
		public float smoothRotation=.5f;

		[Header("Hide")]
		public HideMethod hideMethod=HideMethod.None;
		public Transform hidePoint;
		public Vector3 hidePos;
		public ActivateList activateList;

		[System.NonSerialized]protected VirtualPose m_Pose;
		[System.NonSerialized]protected bool m_IsVisible=true;
		
		#endregion Fields

		#region Unity Messages

		/// <summary>
		/// 
		/// </summary>
		protected virtual void Start() {
			m_Pose=CrossInputManager.VirtualPoseReference(poseName);
			if(m_Pose==null){
				Log.e("PoseTransform",string.Format("Invalid Pose@{0}",poseName));
				Object.Destroy(this);
				return;
			}
			if(m_Transform==null) m_Transform=transform;
			//
			switch(m_Pose.trackingType) {
				case InputTrackingType.Ximmerse:
					m_Transform.parent=m_Pose.parent;
				break;
				case InputTrackingType.Oculus:
				case InputTrackingType.Morpheus:
				case InputTrackingType.Sixense:
				case InputTrackingType.SteamVR:
				default:
					hideMethod=HideMethod.None;
				break;
			}
		}
	
		/// <summary>
		/// 
		/// </summary>
		protected virtual void FixedUpdate() {
			UpdatePose();
		}

		#endregion Unity Messages

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		public virtual void UpdatePose() {
			//
			Vector3 position;
			Quaternion rotation;
			//
			if(!GetPose(out position,out rotation)) {
				// TODO ???
				return;
			}
			// Auto hide?
			if(hideMethod!=HideMethod.None) {
				SetVisible((m_Pose.trackingResult&TrackingResult.PositionFound)!=0,ref position);
			}
			//
			if(useSmooth){
				if(usePosition) {m_Transform.localPosition=Vector3.Lerp(m_Transform.localPosition,position,smoothPosition);}
				if(useRotation) {m_Transform.localRotation=Quaternion.Slerp(m_Transform.localRotation,rotation,smoothRotation);}
			} else{
				if(usePosition) {m_Transform.localPosition=position;}
				if(useRotation) {m_Transform.localRotation=rotation;}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual bool GetPose(out Vector3 position,out Quaternion rotation) {
			position=m_Pose.position;
			rotation=m_Pose.rotation;
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void SetVisible(bool value,ref Vector3 position) {
			switch(hideMethod) {
				case HideMethod.Position:
					if(!value) {
						if(hidePoint==null) {
							position=hidePos;
						}else {
							if(m_Pose.parent==null) {
								position=hidePoint.position;
							}else {
								position=m_Pose.parent.InverseTransformPoint(hidePoint.position);
							}
						}
					}
				break;
				case HideMethod.ActivateList:
					//
					if(activateList==null) {
						activateList=m_Transform.GetComponentInChildren<ActivateList>();
						if(activateList==null) {
							hideMethod=HideMethod.None;
							return;
						}
					}
					//
					if(value==m_IsVisible) {
						return;
					}
					m_IsVisible=value;
					//
					if(activateList!=null) {
						activateList.SetValueForce(value);
					}
				break;
			}
		}

		#endregion Methods

	}
}