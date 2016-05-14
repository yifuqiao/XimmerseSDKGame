using UnityEngine;
using Ximmerse.IO;

public class ExportConfigs : MonoBehaviour {

	protected virtual void Awake () {

		if(Application.platform==RuntimePlatform.Android) {
			FileSystem.CopyResourcesToDirectory("Configs/",Ximmerse.Environment.CONFIG_PATH,false);
		}
	}

}
