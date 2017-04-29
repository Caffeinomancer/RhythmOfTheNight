using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Pacemaker : MonoBehaviour {

    EventDispatcher eventDispatcher;
    public float timeBetweenBeats = 0.48f;
    public float leewayTime; //time before the autobeat that the game accepts input as "on beat"
    public float timeToBeat; //countdown in secounds to the next beat;

    public float beatAnticipation = 0.1f; //time before a real beat that a prebeat tries to run.

    bool preBeatHasRunThisCycle;

    //UnityEvent BeatEvent;

    // Use this for initialization
    void Start () {
        //Time.fixedDeltaTime = 0.48f;
        timeToBeat = timeBetweenBeats;
        eventDispatcher = EventDispatcher.Instance;

        //add itself to the beat listeners
        

        /*BeatEvent = new UnityEvent();
        BeatEvent.AddListener(OnBeat);
        eventDispatcher.RegisterBeatListener(ref BeatEvent);*/


    }
	
	// Update is called once per frame
	void Update () {
        timeToBeat -= Time.deltaTime;
        if(timeToBeat<=0)
        {
            if(!preBeatHasRunThisCycle)
            {
                RunPreBeat();
            }
            RunBeat();
        }
        if(timeToBeat <= beatAnticipation)
        {
            RunPreBeat();
        }
	}

    public void RunBeat()
    {
        eventDispatcher.InvokeBeatEvent();
        timeToBeat = timeBetweenBeats;
        preBeatHasRunThisCycle = false;
    }

    public void RunPreBeat()
    {
        eventDispatcher.InvokePreBeatEvent();
        preBeatHasRunThisCycle = true;
        //timeToBeat = timeBetweenBeats;
    }

    /*void OnBeat()
    {
        timeToBeat = timeBetweenBeats;//for when a beat is declared from player movement instead of automatically
    }*/


    /*void FixedUpdate()
    {
        //Debug.Log("Beat");
        eventDispatcher.InvokeBeatEvent();
    }*/
}
