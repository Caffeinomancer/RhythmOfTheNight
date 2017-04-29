using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class HeadbobScr : MonoBehaviour {

    float maxHeight = 0.04f;
    float minHeight = -0.05f;
    float dropSpeed;

    EventDispatcher eventDispatcher;
    UnityEvent BeatEvent, PreBeatEvent;

    enum HeadState
    {
        rising = 0,
        still,
        dropping
    }

    HeadState state;

	// Use this for initialization
	void Start () {
        eventDispatcher = EventDispatcher.Instance;

        BeatEvent = new UnityEvent();
        BeatEvent.AddListener(OnBeat);
        eventDispatcher.RegisterBeatListener(ref BeatEvent);

        PreBeatEvent = new UnityEvent();
        PreBeatEvent.AddListener(OnPreBeat);
        eventDispatcher.RegisterBeatListener(ref PreBeatEvent);
    }
	
	// Update is called once per frame
	void Update () {
	    if(state==HeadState.dropping)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y-0.05f, -5);
            if (transform.localPosition.y <= minHeight) { state = HeadState.rising; }
        }
        if (state == HeadState.rising)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y + 0.01f, -5);
            if (transform.localPosition.y >= maxHeight) { state = HeadState.still; }
        }
    }

    void OnBeat()
    {
        //state = HeadState.dropping;
        //transform.localPosition = new Vector3(0, maxHeight, -5);
    }

    void OnPreBeat()
    {
        state = HeadState.dropping;
        transform.localPosition = new Vector3(0, maxHeight, -5);
    }
}
