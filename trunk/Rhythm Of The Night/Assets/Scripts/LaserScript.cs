using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


public class LaserScript : MonoBehaviour {
    
    enum LaserState
    {
        On = 0,
        Off
    }

    EventDispatcher eventDispatcher;
    UnityEvent beatEvent;

    Transform laserOn, laserOff;
    List<Transform> laserBeamList;

    Renderer onLaserRenderer, offLaserRenderer;
    List<Renderer> beamRendererList;

    public int Beam_Number; //number of beam objects (i think?)

    public int onCount=2; //number of beats the beam is on
    public int offCount=2; //number of beats the beam is off
    public int beatCount = 0;

    public bool Is_On;

    void Start () {
        eventDispatcher = EventDispatcher.Instance;

        beatEvent = new UnityEvent();
        beatEvent.AddListener(HandleBeat);
        eventDispatcher.RegisterBeatListener(ref beatEvent);

        laserOn = this.transform.FindChild("Laser_On");
        laserOff = this.transform.FindChild("Laser_Off");
        onLaserRenderer = laserOn.transform.GetComponent<Renderer>();
        offLaserRenderer = laserOff.transform.GetComponent<Renderer>();

        laserBeamList = new List<Transform>(1);
        beamRendererList = new List<Renderer>(1);

        Transform beam;
        string childString;
        for (int i = 0; i < Beam_Number; i++)
        {
            childString = "Beam_" + (i + 1).ToString();
            beam = laserOn.transform.FindChild(childString);
            laserBeamList.Add(beam);
            beamRendererList.Add(beam.transform.GetComponent<Renderer>());
        }

        if(Is_On)
        {
            SwitchLaser(LaserState.On);
        }

        else
        {
            SwitchLaser(LaserState.Off);
        }
    }

    // Update is called once per frame
    void Update () {
	    //Sound code in here
	}

    private void HandleBeat()
    {
        beatCount++;
        if (Is_On)
        {
            if(beatCount < onCount) { return; }//exit early if it's not time to switch

            Is_On = false;
            SwitchLaser(LaserState.Off);
        }

        else
        {
            if (beatCount < offCount) { return; }//exit early if it's not time to switch

            Is_On = true;
            SwitchLaser(LaserState.On);
        }
    }

    private void SwitchLaser(LaserState state)
    {
        beatCount = 0;
        if(state == LaserState.On)
        {
            onLaserRenderer.enabled = true;
            offLaserRenderer.enabled = false;
            for(int i = 0; i < beamRendererList.Count; i++)
            {
                beamRendererList[i].enabled = true;
            }
        }
        
        else
        {
            offLaserRenderer.enabled = true;
            onLaserRenderer.enabled = false;

            for (int i = 0; i < beamRendererList.Count; i++)
            {
                beamRendererList[i].enabled = false;
            }
        }
    }
}
