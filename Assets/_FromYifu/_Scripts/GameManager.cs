using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager s_instance;
    public static GameManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new GameObject("[GameManagerInstance]").AddComponent<GameManager>();
                s_instance.Init();
            }
            return s_instance;
        }
    }

    public InputDispatcher InputDispatcher
    {
        private set; get;
    }

    private void Init()
    {
        InputDispatcher = new InputDispatcher();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        InputDispatcher.DispatchInputEvents();
    }

    private void FixedUpdate()
    {
        InputDispatcher.DispatchMotionInputEvents();
    }
}
