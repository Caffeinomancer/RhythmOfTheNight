using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class AltVisualizerScr : MonoBehaviour {

    public float maxScale = 2.5f;
    public float minScale = 2.0f;
    public float animSpeed = 0.1f;

    EventDispatcher eventDispatcher;
    UnityEvent BeatEvent, PreBeatEvent;

    enum HeartState
    {
        expanding = 0,
        still,
        retracting
    }

    HeartState state;

    // Use this for initialization
    void Start () {
        eventDispatcher = EventDispatcher.Instance;

        BeatEvent = new UnityEvent();
        BeatEvent.AddListener(OnBeat);
        eventDispatcher.RegisterBeatListener(ref BeatEvent);

        PreBeatEvent = new UnityEvent();
        PreBeatEvent.AddListener(OnPreBeat);
        eventDispatcher.RegisterBeatListener(ref PreBeatEvent);
        state = HeartState.still;
    }
	
	// Update is called once per frame
	void Update () {
        if (state == HeartState.expanding)
        {
            transform.localScale = new Vector3(transform.localScale.x + animSpeed, transform.localScale.y + animSpeed, 1);
            if (transform.localScale.y >= maxScale) { state = HeartState.retracting; }
        }
        if (state == HeartState.retracting)
        {
            transform.localScale = new Vector3(transform.localScale.x - animSpeed, transform.localScale.y - animSpeed, 1);
            if (transform.localScale.y <= minScale) { state = HeartState.still; }
        }
    }

    void OnBeat()
    {
        state = HeartState.retracting;
        //transform.localPosition = new Vector3(0, maxHeight, -5);
    }

    void OnPreBeat()
    {
        state = HeartState.expanding;
        transform.localScale = new Vector3(minScale, minScale, 1);
    }
}
