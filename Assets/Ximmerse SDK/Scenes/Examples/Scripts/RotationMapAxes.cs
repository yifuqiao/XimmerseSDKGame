using UnityEngine;
using Ximmerse.CrossInput;

/// <summary>
/// 
/// </summary>
public class RotationMapAxes:MonoBehaviour,IInputSource{

	#region Nested Types

	public interface IEvaluator {
		float Evaluate(MapEntry e,ref Vector3 eulerAngles);
	}

	[System.Serializable]
	public class MapEntry {

		//
		public string axisName;
		public int axisId;
		//
		public bool isInverse=false;
		public float zeroAngle=0,minAngle=-30f,maxAngle=30f;
		public float minDeadAngle=-5f,maxDeadAngle=5f;
		public float lerpIncrease=.75f,lerpDecrease=.5f;

		public VirtualAxis virtualAxis;
		public IEvaluator evaluator;

		protected float m_Value;

		public MapEntry(string axisName,int axisId) {
			this.axisName=axisName;
			this.axisId=axisId;
		}

		public void UpdateFromEulerAngles(ref Vector3 eulerAngles) {
			if(evaluator==null) {
				float valuePrev=virtualAxis.GetValue,value=0f;
				float angle=Repeat(eulerAngles[axisId]-zeroAngle,-180f,180f);
				if(isInverse) {
					angle*=-1f;
				}
				if(angle>0f) {
					if(angle>maxDeadAngle) {
						value=Mathf.Clamp(angle/maxAngle,0f,1f);
					}else {
						value=0f;
					}
				} else if(angle<0f) {
					if(angle<minDeadAngle) {
						value=-Mathf.Clamp(angle/minAngle,0f,1f);
					}else {
						value=0f;
					}
				}
				value=Evaluate(valuePrev,value,lerpIncrease,lerpDecrease);
				virtualAxis.Update(m_Value=value);
			}else {
				virtualAxis.Update(m_Value=evaluator.Evaluate(this,ref eulerAngles));
			}
		}
	}

	#endregion Nested Types

	#region Static

	/// <summary>
	/// 
	/// </summary>
	public static float Repeat(float value,float min,float max) {
		float t = max-min;
		while(value<min)
			value+=t;
		while(value>max)
			value-=t;
		return value;
	}

	/// <summary>
	/// 
	/// </summary>
	public static float Evaluate(float old_value,float new_value,float kIncrease,float kDecrease) {
		if(old_value*new_value<0f) {
			return new_value;
		}
		int sign = 1;
		if(old_value<0) {
			old_value*=-1;
			sign=-1;
		}
		if(new_value<0) {
			new_value*=-1;
			sign=-1;
		}
		if(new_value>old_value) {// Increase.
			return sign*Mathf.Lerp(old_value,new_value,kIncrease);
		} else if(new_value<old_value) {// Decrease.
			return sign*Mathf.Lerp(old_value,new_value,kDecrease);
		}
		return new_value;
	}

	#endregion Static

	#region Fields

	[SerializeField]protected Vector3 m_EulerAngles;
	[SerializeField]protected Transform m_PoseTarget;
	[SerializeField]protected string m_PoseName;
	[SerializeField]protected MapEntry[] m_Entries=new MapEntry[2] {
		new MapEntry("Horizontal",2), // -> Z Axis
		new MapEntry("Vertical"  ,0), // -> X Axis
	};
	[SerializeField]protected KeyOrButton m_BtnReset=new KeyOrButton(
		new KeyCode[1]{KeyCode.R},new string[1]{"Fire1"}
	);

	protected VirtualPose m_Pose;

	#endregion Fields

	#region Unity Messages

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Update() {
		if(m_BtnReset.GetAnyDown()) {
			ResetZeroAngles();
		}
	}

	#endregion Unity Messages

	#region Methods

	/// <summary>
	/// 
	/// </summary>
	public int InitInput() {
		if(!string.IsNullOrEmpty(m_PoseName)) {
			m_Pose=CrossInputManager.VirtualPoseReference(this,m_PoseName,false);
		}
		if(m_Pose==null&&m_PoseTarget==null) {
			Destroy(this);
			return -1;
		}
		for(int i=0,imax=m_Entries.Length;i<imax;++i) {
			m_Entries[i].virtualAxis=CrossInputManager.VirtualAxisReference(this,m_Entries[i].axisName,true);
		}
		return 0;
	}

	/// <summary>
	/// 
	/// </summary>
	public int ExitInput() {
		return 0;
	}
	
	/// <summary>
	/// 
	/// </summary>
	public int EnterInputFrame() {
		//Vector3 euler;
		if(m_Pose==null) {
			m_EulerAngles=m_PoseTarget.rotation.eulerAngles;
		} else {
			m_EulerAngles=m_Pose.rotation.eulerAngles;
		}
		//
		for(int i=0,imax=m_Entries.Length;i<imax;++i) {
			m_Entries[i].UpdateFromEulerAngles(ref m_EulerAngles);
		}
		//
		return 0;
	}

	/// <summary>
	/// 
	/// </summary>
	public int ExitInputFrame() {
		return 0;
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual int IndexOf(string name) {
		for(int i=0,imax=m_Entries.Length;i<imax;++i) {
			if(name==m_Entries[i].axisName) {
				return i;
			}
		}
		return -1;
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void ResetZeroAngles() {
		for(int i=0,imax=m_Entries.Length;i<imax;++i) {
			m_Entries[i].zeroAngle=m_EulerAngles[m_Entries[i].axisId];
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual bool SetEvaluator(string name,IEvaluator evaluator) {
		int i=IndexOf(name);
		if(i==-1) {
			return false;
		}else {
			m_Entries[i].evaluator=evaluator;
			return true;
		}
	}

	#endregion Methods

}