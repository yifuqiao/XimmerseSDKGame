
namespace Ximmerse{

	public enum UpdateType {// The available methods of updating are:
		FixedUpdate, // Update in FixedUpdate.
		LateUpdate, // Update in LateUpdate.
		ManualUpdate, // user must call to update.
		Update, // Update in Update.
		SubThread, // Update in SubThread.
	}

}