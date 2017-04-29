using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public Pacemaker pacemaker;
    EventDispatcher eventDispatcher;
    UnityEvent MoveUpEvent, MoveDownEvent, MoveLeftEvent, MoveRightEvent, BeatEvent;

    bool moveUp = false, moveDown = false, moveLeft = false, moveRight = false;
    bool movingPlayer = false;
    Vector2 currPos, currPosMoving, newPosMoving;

    public float speed;

    private List<string> pickUpList;

    //For detecting if player is allowed to move
    GameObject visualizer1;
    GameObject visualizer2;

    public float EarlyMoveRange = 8f;
    public float LateMoveRange = 16f;
    private float enableMovementRange;

    private bool movedOnce = false;

    public bool cheats = false;

    enum Direction
    {
        Up = 0,
        Down,
        Left,
        Right
    }

    void Start () {
        pickUpList = new List<string>(1);

        eventDispatcher = EventDispatcher.Instance;

        MoveUpEvent = new UnityEvent();
        MoveUpEvent.AddListener(MoveUp);
        eventDispatcher.RegisterInput(ref MoveUpEvent,0);

        MoveDownEvent = new UnityEvent();
        MoveDownEvent.AddListener(MoveDown);
        eventDispatcher.RegisterInput(ref MoveDownEvent,1);

        MoveLeftEvent = new UnityEvent();
        MoveLeftEvent.AddListener(MoveLeft);
        eventDispatcher.RegisterInput(ref MoveLeftEvent,2);

        MoveRightEvent = new UnityEvent();
        MoveRightEvent.AddListener(MoveRight);
        eventDispatcher.RegisterInput(ref MoveRightEvent,3);

        BeatEvent = new UnityEvent();
        BeatEvent.AddListener(OnBeat);
        eventDispatcher.RegisterBeatListener(ref BeatEvent);

        currPosMoving = transform.position;
        currPos = transform.position;

        visualizer1 = GameObject.Find("Visualizer");
        visualizer2 = GameObject.Find("Visualizer2");

        enableMovementRange = EarlyMoveRange + 3f;
}

// Update is called once per frame
void Update () {

        if(GetDistanceBetweenVisualizers() <= EarlyMoveRange)
        {
            movedOnce = false;
        }

        if(moveUp)
        {
            MovePlayer(Direction.Up);
            
        }

        else if(moveDown)
        {
            MovePlayer(Direction.Down);
        }

        else if (moveLeft)
        {
            MovePlayer(Direction.Left);
        }

        else if (moveRight)
        {
            MovePlayer(Direction.Right);
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (cheats)
            {
                cheats = false;
                Debug.Log("CHEATS DISABLED");
            }
            else
            {
                cheats = true;
                Debug.Log("CHEATS ENABLED");
            }
        }

        else
        {
            //Default
        }

	}

    private void MovePlayer(Direction direction)
    {
      
        if (direction == Direction.Up)
        {
            //playerBoxCollider.transform.position = new Vector3(playerBoxCollider.transform.position.x, playerBoxCollider.transform.position.y - 100, playerBoxCollider.transform.position.z);
            if (transform.position.y != newPosMoving.y)
            {
                transform.position = new Vector2(currPosMoving.x, currPosMoving.y + speed);
                currPosMoving = transform.position;
            }

            else
            {
                currPos = transform.position;
                //ollider.transform.position = currPos;
                moveUp = false;
                movingPlayer = false;
            }
        }

        else if (direction == Direction.Down)
        {
            if (transform.position.y != newPosMoving.y)
            {
                transform.position = new Vector2(currPosMoving.x, currPosMoving.y - speed);
                currPosMoving = transform.position;
            }

            else
            {
                currPos = transform.position;
                moveDown = false;
                movingPlayer = false;
            }
        }

        else if (direction == Direction.Left)
        {
            if (transform.position.x != newPosMoving.x)
            {
                transform.position = new Vector2(currPosMoving.x - speed, currPosMoving.y);
                currPosMoving = transform.position;
            }

            else
            {
                currPos = transform.position;
                moveLeft = false;
                movingPlayer = false;
            }
        }

        else if (direction == Direction.Right)
        {
            if (transform.position.x != newPosMoving.x)
            {
                transform.position = new Vector2(currPosMoving.x + speed, currPosMoving.y);
                currPosMoving = transform.position;
            }

            else
            {
                currPos = transform.position;
                moveRight = false;
                movingPlayer = false;
            }
        }

        else
        {
            Debug.Log("ERR: PlayerScript Movement Error");
        }

    }

    private void MoveUp()
    {
        if (((GetDistanceBetweenVisualizers() <= EarlyMoveRange) || (GetDistanceBetweenVisualizers() >= LateMoveRange)) && !movedOnce)
        {
            if (!movingPlayer)
            {
                moveUp = true;
                movingPlayer = true;
                newPosMoving = new Vector2(transform.position.x, transform.position.y + 1);
                currPosMoving = transform.position;
                movedOnce = true;
                /*if (pacemaker.timeToBeat <= pacemaker.leewayTime)//if a movement occurs in the acceptable beat area...
                {
                    pacemaker.RunBeat();
                }*/
            }
        }
    }

    private void MoveDown()
    {
        if (((GetDistanceBetweenVisualizers() <= EarlyMoveRange) || (GetDistanceBetweenVisualizers() >= LateMoveRange)) && !movedOnce)
        {
            if (!movingPlayer)
            {
                movedOnce = true;
                moveDown = true;
                movingPlayer = true;
                newPosMoving = new Vector2(transform.position.x, transform.position.y - 1);
                currPosMoving = transform.position;

                /*if (pacemaker.timeToBeat <= pacemaker.leewayTime)//if a movement occurs in the acceptable beat area...
                {
                    pacemaker.RunBeat();
                }*/
            }
        }
    }

    private void MoveLeft()
    {
        if (((GetDistanceBetweenVisualizers() <= EarlyMoveRange) || (GetDistanceBetweenVisualizers() >= LateMoveRange)) && !movedOnce)
        {
            if (!movingPlayer)
            {
                movedOnce = true;
                moveLeft = true;
                movingPlayer = true;
                newPosMoving = new Vector2(transform.position.x - 1, transform.position.y);
                currPosMoving = transform.position;

                /*if (pacemaker.timeToBeat <= pacemaker.leewayTime)//if a movement occurs in the acceptable beat area...
                {
                    pacemaker.RunBeat();
                }*/
            }
        }
    }

    private void MoveRight()
    {
        if (((GetDistanceBetweenVisualizers() <= EarlyMoveRange) || (GetDistanceBetweenVisualizers() >= LateMoveRange)) && !movedOnce)
        {
            if (!movingPlayer)
            {
                movedOnce = true;
                moveRight = true;
                movingPlayer = true;
                newPosMoving = new Vector2(transform.position.x + 1, transform.position.y);
                currPosMoving = transform.position;

                /*if (pacemaker.timeToBeat <= pacemaker.leewayTime)//if a movement occurs in the acceptable beat area...
                {
                    pacemaker.RunBeat();
                }*/
            }
        }
    }

    private void OnBeat()
    {
        //Debug.Log("beat!!!");
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {

        Debug.Log(coll.gameObject.name);
        if(coll.gameObject.name.Contains("wall") || coll.gameObject.name.Contains("Door"))
        {
            transform.position = currPos;
            newPosMoving = currPos;
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
            movingPlayer = false;
        }

        else if(coll.gameObject.name.Contains("PickUp"))
        {
            pickUpList.Add(coll.gameObject.name);
            eventDispatcher.InvokePickUpEvent();
            Destroy(coll.gameObject);
        }

        else
        {

        }
    }

    public List<string> getPickUpList()
    {
        return pickUpList;
    }

    private float GetDistanceBetweenVisualizers()
    {
        float distance;
        distance = visualizer2.transform.position.x - visualizer1.transform.position.x;
        if(distance < 0)
        {
            distance *= -1;
        }
        return distance;
    }
}
