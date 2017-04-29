using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InputSystem : MonoBehaviour {

    EventDispatcher eventDispatcher;

    private const int Up = 0;
    private const int Down = 1;
    private const int Left = 2;
    private const int Right = 3;



    // Use this for initialization
    void Start () {
        eventDispatcher = EventDispatcher.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            eventDispatcher.InvokeInputEvent(Up);
            
            //Testing Purposes
            //eventDispatcher.InvokeBeatEvent();
            //
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            eventDispatcher.InvokeInputEvent(Down);

            //Testing Purposes
            //eventDispatcher.InvokeBeatEvent();
            //
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            eventDispatcher.InvokeInputEvent(Left);

            //Testing Purposes
            //eventDispatcher.InvokeBeatEvent();
            //
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            eventDispatcher.InvokeInputEvent(Right);

            //Testing Purposes
            //eventDispatcher.InvokeBeatEvent();
            //
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);

        }
    }
}
