using UnityEngine;
using Ximmerse.CrossInput;

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class KeyOrButton {

	public KeyCode[] keys=new KeyCode[0];
	public string[] buttons=new string[0];
	
	public KeyOrButton(KeyCode[] i_keys,string[] i_buttons) {
		keys=i_keys;
		buttons=i_buttons;
	}

	/// <summary>
	/// 
	/// </summary>
	public bool GetAnyDown() {
		int i,imax;
		i=0;imax=keys.Length;
		while(i<imax){
			if(Input.GetKeyDown(keys[i])){
				return true;
			}
			++i;
		}
		i=0;imax=buttons.Length;
		while(i<imax){
			if(CrossInputManager.GetButtonDown(buttons[i])){
				return true;
			}
			++i;
		}
		return false;
	}

	/// <summary>
	/// 
	/// </summary>
	public bool GetAnyFrameCountGreater(int value) {
		int i,imax;
		i=0;imax=buttons.Length;
		while(i<imax){
			if(CrossInputManager.GetButtonFrameCount(buttons[i])>value){
				return true;
			}
			++i;
		}
		return false;
	}

}