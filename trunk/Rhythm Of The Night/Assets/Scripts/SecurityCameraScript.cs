using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

enum CameraDirection
{
    LEFT = 0,
    FORWARD,
    RIGHT
}

public class SecurityCameraScript : MonoBehaviour
{
    SpriteRenderer sr;
    EventDispatcher eventDispatcher;
    UnityEvent beatEvent;

    private List<CameraDetection> DetectionList = new List<CameraDetection>(1);

    public Sprite ForwardSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite;

    public bool FaceForward;
    public bool FaceLeft;
    public bool FaceRight;

    public bool PanRight;
    public bool PanLeft;
    public bool ShouldPan = true;

    public int BeatsUntilPan = 1;
    private int beats = 0;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < 5; i++)
        {
            DetectionList.Add(gameObject.transform.GetChild(i).GetComponent<CameraDetection>());
        }

        sr = GetComponent<SpriteRenderer>();
        if (!FaceForward && !FaceLeft && !FaceRight)
        {
            FaceForward = true;
        }
        else
        {
            if (FaceForward)
            {
                sr.sprite = ForwardSprite;
                FaceLeft = false;
                FaceRight = false;
            }
            else if (FaceLeft)
            {
                sr.sprite = LeftSprite;
                FaceForward = false;
                FaceRight = false;
            }
            else//Face Right == true
            {
                sr.sprite = RightSprite;
                FaceForward = false;
                FaceLeft = false;
            }
        }

        if(!PanRight && !PanLeft)
        {
            PanRight = true;
        }
        else
        {
            if(PanRight)
            {
                PanLeft = false;
            }
            else//PanLeft == true
            {
                PanRight = false;
            }
        }


        eventDispatcher = EventDispatcher.Instance;

        beatEvent = new UnityEvent();
        beatEvent.AddListener(OnBeat);
        eventDispatcher.RegisterBeatListener(ref beatEvent);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void OnBeat()
    {
        beats++;

        if(ShouldPan && (beats >= BeatsUntilPan))
        {
            if (FaceForward)
            {
                if (PanRight)
                {
                    sr.sprite = RightSprite;
                    FaceForward = false;
                    FaceRight = true;
                    MoveDetectionSqaures(CameraDirection.RIGHT);
                }
                else//PanLeft == true;
                {
                    sr.sprite = LeftSprite;
                    FaceForward = false;
                    FaceLeft = true;
                    MoveDetectionSqaures(CameraDirection.LEFT);
                }

            }
            else if (FaceRight)
            {
                if (PanRight)
                {
                    PanRight = false;
                    PanLeft = true;
                }

                if (PanLeft)
                {
                    sr.sprite = ForwardSprite;
                    FaceRight = false;
                    FaceForward = true;
                    MoveDetectionSqaures(CameraDirection.FORWARD);
                }
            }
            else//FaceLeft == true
            {
                if (PanLeft)
                {
                    PanRight = true;
                    PanLeft = false;
                }

                if (PanRight)
                {
                    sr.sprite = ForwardSprite;
                    FaceLeft = false;
                    FaceForward = true;
                    MoveDetectionSqaures(CameraDirection.FORWARD);
                }
            }
        }

        if(beats >= BeatsUntilPan)
        {
            beats = 0;
        }
    }

    private void MoveDetectionSqaures(CameraDirection dir)
    {
        for(int i = 0; i < 5; i++)
        {
            if(dir == CameraDirection.FORWARD)
            {
                DetectionList[i].FaceForward();
            }
            else if(dir == CameraDirection.LEFT)
            {
                DetectionList[i].FaceLeft();
            }
            else//dir == CameraDirection.RIGHT
            {
                DetectionList[i].FaceRight();
            }
        }
    }
}
