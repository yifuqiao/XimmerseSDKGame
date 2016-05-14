using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Activate or Deactivate a lot of GameObjects and Behaviours by a boolean variable.</para>
/// <para>NOTE:it's a Binary Choice Model.For an example,it can make a better choice between normal mode and OVR mode.<para>
/// </summary>
public class ActivateList:MonoBehaviour {

	#region Helper

	/// <summary>
	/// 
	/// </summary>
	public static void SetGameObjects(GameObject[] gos,bool value) {
		GameObject item;
		for(int i=0,imax=gos.Length;i<imax;++i) {
			item=gos[i];
			//foreach(GameObject item in gos){
			if(item!=null) {
				item.SetActive(value);
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public static void SetEachGameObject(GameObject[] gos,bool[] values) {
		GameObject item;
		for(int i=0,imax=gos.Length;i<imax;++i) {
			item=gos[i];
			//foreach(GameObject item in gos){
			if(item!=null) {
				item.SetActive(values[i]);
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public static void SetBehaviours<T>(T[] coms,bool value) where T:Behaviour {
		T item;
		for(int i=0,imax=coms.Length;i<imax;++i) {
			item=coms[i];
			//foreach(Behaviour item in gos){
			if(item!=null) {
				item.enabled=value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public static void SetColliders<T>(T[] coms,bool value) where T:Collider {
		T item;
		for(int i=0,imax=coms.Length;i<imax;++i) {
			item=coms[i];
			//foreach(Behaviour item in gos){
			if(item!=null) {
				item.enabled=value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public static void SetRenderers<T>(T[] coms,bool value) where T:Renderer {
		T item;
		for(int i=0,imax=coms.Length;i<imax;++i) {
			item=coms[i];
			//foreach(Behaviour item in gos){
			if(item!=null) {
				item.enabled=value;
			}
		}
	}

#if UNITY_EDITOR

	/// <summary>
	/// 
	/// </summary>
	[ContextMenu("Add To Actives")]
	protected virtual void AddToActives(){
		UnityEditor.ArrayUtility.AddRange<GameObject>(ref goActives,UnityEditor.Selection.gameObjects);
	}

	/// <summary>
	/// 
	/// </summary>
	[ContextMenu("Add To Deactives")]
	protected virtual void AddToDeactives(){
		UnityEditor.ArrayUtility.AddRange<GameObject>(ref goDeactives,UnityEditor.Selection.gameObjects);
	}

#endif

	#endregion Helper

	#region Fields

	public bool doOnAwake=false,doOnStart=false;
	[SerializeField]protected bool m_Value=true;
	public virtual bool Value{
		get {
			return m_Value;
		}
		set {
			SetValue(m_Value,false);
		}
	}

	#region List

	public GameObject[]
		goActives=new GameObject[0];
	public Behaviour[]
		comActives=new Behaviour[0];

	public GameObject[]
		goDeactives=new GameObject[0];
	public Behaviour[]
		comDeactives=new Behaviour[0];

	#endregion List

	#endregion Fields

	#region Unity Messages

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Awake() {
		if(doOnAwake)
			SetValueForce(m_Value);
	}

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Start() {
		if(doOnStart)
			SetValueForce(m_Value);
	}

	#endregion Unity Messages

	#region Methods

	/// <summary>
	/// 
	/// </summary>
	public virtual void Enable() {
		SetValue(true);
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void Disable() {
		SetValue(false);
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void Toggle() {
		SetValue(!m_Value);
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void SetValue(bool value) {
		SetValue(value,false);
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void SetValueForce(bool value) {
		SetValue(value,true);
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void SetValue(bool value,bool isForce) {
		//
		if(m_Value==value&&!isForce) {
			return;
		}
		m_Value=value;
		//
		SetGameObjects(goActives,m_Value);
		SetBehaviours<Behaviour>(comActives,m_Value);
		SetGameObjects(goDeactives,!m_Value);
		SetBehaviours<Behaviour>(comDeactives,!m_Value);
	}

	#endregion Methods

}
