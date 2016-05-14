using UnityEngine;
using Ximmerse.CrossInput;

/// <summary>
/// 
/// </summary>
public class VibrationTest:MonoBehaviour {

	#region Nested Types

	/// <summary>
	/// 
	/// </summary>
	[System.Serializable]
	public class ButtonEntry{

		public string name;
		public float delay;
		public float duration;

		public ButtonEntry(string name) {
			this.name=name;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	[System.Serializable]
	public class ControllerEntry {
		public string fmt="{0}";
		public string vibration="Hand";
		public ButtonEntry[] buttons=new ButtonEntry[5] {
			new ButtonEntry("Start"),
			new ButtonEntry("One"),
			new ButtonEntry("Two"),
			new ButtonEntry("Three"),
			new ButtonEntry("Trigger"),
		};

		public void Awake() {
			vibration=string.Format(fmt,vibration);
			for(int i=0,imax=buttons.Length;i<imax;++i) {
				buttons[i].name=string.Format(fmt,buttons[i].name);
			}
		}

		public void Update() {
			for(int i=0,imax=buttons.Length;i<imax;++i) {
				if(CrossInputManager.GetButtonDown(buttons[i].name)) {
					CrossInputManager.SetVibration(vibration,0,buttons[i].delay,buttons[i].duration);
				}
			}
		}
	}

	#endregion Nested Types

	#region Fields

	/// <summary>
	/// 
	/// </summary>
	public ControllerEntry[] controllers=new ControllerEntry[2];

	#endregion Fields

	#region Unity Messages

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Awake() {
		for(int i=0,imax=controllers.Length;i<imax;++i) {
			controllers[i].Awake();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Update() {
		for(int i=0,imax=controllers.Length;i<imax;++i) {
			controllers[i].Update();
		}
	}

	#endregion Unity Messages

}