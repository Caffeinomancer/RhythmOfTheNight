using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

enum EventType
{
    BeatEvent = 0,
    PreBeatEvent,
    InputEvent,
    PickUpEvent
}

enum InputEventType
{
    Up = 0,
    Down,
    Left,
    Right, 
    Default
}

public class EventDispatcher : MonoBehaviour {

    private static EventDispatcher _instance;
    public static EventDispatcher Instance { get { return _instance; } }

    private List<UnityEvent> preBeatListenerList = new List<UnityEvent>(1);
    private List<UnityEvent> beatListenerList = new List<UnityEvent>(1);
    private List<UnityEvent> pickUpListenerList = new List<UnityEvent>(1);
    private UnityEvent MoveUpEvent;
    private UnityEvent MoveDownEvent;
    private UnityEvent MoveLeftEvent;
    private UnityEvent MoveRightEvent;

    private void Awake()
    {
        if(_instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        {
            _instance = this;
        }
    }

	void Start () {
        //beatListenerList = new List<UnityEvent>(1);
    }
	

	// Update is called once per frame
	void Update () {
	}

    public void InvokePreBeatEvent()
    {
        DispatchAllEvents(EventType.PreBeatEvent);
    }

    public void InvokeBeatEvent()
    {
        DispatchAllEvents(EventType.BeatEvent);
    }

    public void InvokePickUpEvent()
    {
        DispatchAllEvents(EventType.PickUpEvent);
    }

    public void InvokeInputEvent(int input)
    {
        InputEventType inputEvent = InputEventType.Default;
        
        switch(input)
        {
            case 0:
                inputEvent = InputEventType.Up;
                break;
            case 1:
                inputEvent = InputEventType.Down;
                break;
            case 2:
                inputEvent = InputEventType.Left;
                break;
            case 3:
                inputEvent = InputEventType.Right;
                break;
            default:
                Debug.Log("ERR: InvokeInputEvent received bad input");
                break;
        }

        DispatchAllEvents(EventType.InputEvent, inputEvent);
    }
  
    public void RegisterBeatListener(ref UnityEvent eventToAdd)
    {
        beatListenerList.Add(eventToAdd);
    }

    public void RegisterPickUpListener(ref UnityEvent eventToAdd)
    {
        pickUpListenerList.Add(eventToAdd);
    }

    public void RegisterInput(ref UnityEvent eventToAdd, int type)
    {
        switch(type)
        {
            case 0:
                MoveUpEvent = eventToAdd;
                break;
            case 1:
                MoveDownEvent = eventToAdd;
                break;
            case 2:
                MoveLeftEvent = eventToAdd;
                break;
            case 3:
                MoveRightEvent = eventToAdd;
                break;
            default:
                Debug.Log("ERR: Failure to register input");
                break;
        }
    }

    private void DispatchAllEvents(EventType type, InputEventType inputType = InputEventType.Default)
    {
        if(type == EventType.BeatEvent)
        {
            if (beatListenerList.Capacity > 0)
                for (int i = 0; i < beatListenerList.Count; i++)
                    beatListenerList[i].Invoke();
        }
        else if (type == EventType.PreBeatEvent)
        {
            if (preBeatListenerList.Capacity > 0)
                for (int i = 0; i < preBeatListenerList.Count; i++)
                    preBeatListenerList[i].Invoke();
        }

        else if(type == EventType.InputEvent)
        {
            switch(inputType)
            {
                case InputEventType.Up:
                    MoveUpEvent.Invoke();
                    break;
                case InputEventType.Down:
                    MoveDownEvent.Invoke();
                    break;
                case InputEventType.Left:
                    MoveLeftEvent.Invoke();
                    break;
                case InputEventType.Right:
                    MoveRightEvent.Invoke();
                    break;
                default:
                    Debug.Log("ERR: Can't invoke input event");
                    break;
            }
        }

        else if(type == EventType.PickUpEvent)
        {
            if (pickUpListenerList.Capacity > 0)
                for (int i = 0; i < pickUpListenerList.Count; i++)
                    pickUpListenerList[i].Invoke();
        }

        else
        {
            //Should never reach here
            Debug.Log("ERR: No event type dispatched");
        }
    }
}
