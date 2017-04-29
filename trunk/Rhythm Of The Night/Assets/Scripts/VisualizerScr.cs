using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class VisualizerScr : MonoBehaviour {

    EventDispatcher eventDispatcher;
    UnityEvent BeatEvent;

    public Pacemaker pacemaker;
    float timeBetweenBeats;
    float timeSinceLastBeat;
    public float offset;
    Vector3 startPos;
    float height = -4.5f;
    public bool left = true;
    public float zPos = 1;
    public float speed;

	// Use this for initialization
	void Start () {
        eventDispatcher = EventDispatcher.Instance;

        BeatEvent = new UnityEvent();
        BeatEvent.AddListener(OnBeat);
        eventDispatcher.RegisterBeatListener(ref BeatEvent);

        timeBetweenBeats = pacemaker.timeBetweenBeats;
        speed = (float)offset / (float)timeBetweenBeats;

        if (left) { offset *= -1; }
        startPos = new Vector3(offset, height, zPos);
        
        transform.localPosition = startPos;
        
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceLastBeat += Time.deltaTime;

        if (left)
        {
            transform.localPosition = new Vector3(offset + timeSinceLastBeat * speed, height, zPos);
            //Debug.Log(transform.localPosition.x);
        }
        else
        {
            transform.localPosition = new Vector3(offset - timeSinceLastBeat * speed, height, zPos);
        }
    }

    private void OnBeat()
    {
        timeSinceLastBeat = 0.0f;
        transform.localPosition = startPos;
    }
}
