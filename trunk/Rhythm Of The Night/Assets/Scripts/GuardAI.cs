using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


enum GuardDirection
{
    UP = 0,
    DOWN,
    LEFT,
    RIGHT
}

public class GuardAI : MonoBehaviour {

    EventDispatcher eventDispatcher;
    UnityEvent BeatEvent;

    public GameObject[] NavPoints;
    public int currentTarget;

    public Sprite FrontSprite;
    public Sprite BackSprite;
    private SpriteRenderer spriteRenderer;

    private Transform detect1;
    private Transform detect2;
    private Transform detect3;
    private DetectionRange detect1Script;
    private DetectionRange detect2Script;
    private DetectionRange detect3Script;
    private Renderer detect1Ren;
    private Renderer detect2Ren;
    private Renderer detect3Ren;


    private bool movingUp = false;
    private bool movingDown = false;
    private bool movingLeft = false;
    private bool movingRight = false;

    private Vector3 lastPos;

    // Use this for initialization
    void Start () {
        eventDispatcher = EventDispatcher.Instance;

        BeatEvent = new UnityEvent();
        BeatEvent.AddListener(OnBeat);
        eventDispatcher.RegisterBeatListener(ref BeatEvent);

       
        detect1 = transform.FindChild("Detect1");
        detect2 = transform.FindChild("Detect2");
        detect3 = transform.FindChild("Detect3");

        detect1Ren = detect1.GetComponent<Renderer>();
        detect2Ren = detect2.GetComponent<Renderer>();
        detect3Ren = detect3.GetComponent<Renderer>();

        detect1Script = detect1.GetComponent<DetectionRange>();
        detect2Script = detect2.GetComponent<DetectionRange>();
        detect3Script = detect3.GetComponent<DetectionRange>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        CheckEnableDetection();
	}

    private void CheckEnableDetection()
    {

        if(detect1Script.detect1Hit)
        {
            detect1Ren.enabled = false;
            detect2Ren.enabled = false;
            detect3Ren.enabled = false;
        }
        
        else if(detect2Script.detect2Hit)
        {
            detect2Ren.enabled = false;
            detect2Ren.enabled = false;
        }

        else if(detect3Script.detect3Hit)
        {
            detect3Ren.enabled = false;
        }

        else
        {
            detect1Ren.enabled = true;
            detect2Ren.enabled = true;
            detect3Ren.enabled = true;
        }

    }

    private void SwitchSprite(GuardDirection dir)
    {
        if(dir == GuardDirection.UP)
        {
            spriteRenderer.sprite = BackSprite;
        }

        else if (dir == GuardDirection.DOWN)
        {
            spriteRenderer.sprite = FrontSprite;
        }

        else
        {
            //DEFAULT
        }
    }

    private void AlignDetectRange(GuardDirection dir)
    {
        if (dir == GuardDirection.UP)
        {
            detect1.transform.position = transform.position;
            detect2.transform.position = transform.position;
            detect3.transform.position = transform.position;

            detect1.transform.position = new Vector3(detect1.transform.position.x, detect1.transform.position.y - 1, detect1.transform.position.z);
            detect2.transform.position = new Vector3(detect2.transform.position.x, detect2.transform.position.y - 2, detect2.transform.position.z);
            detect3.transform.position = new Vector3(detect3.transform.position.x, detect3.transform.position.y - 3, detect3.transform.position.z);
        }

        else if (dir == GuardDirection.DOWN)
        {
            detect1.transform.position = transform.position;
            detect2.transform.position = transform.position;
            detect3.transform.position = transform.position;

            detect1.transform.position = new Vector3(detect1.transform.position.x, detect1.transform.position.y + 1, detect1.transform.position.z);
            detect2.transform.position = new Vector3(detect2.transform.position.x, detect2.transform.position.y + 2, detect2.transform.position.z);
            detect3.transform.position = new Vector3(detect3.transform.position.x, detect3.transform.position.y + 3, detect3.transform.position.z);
        }

        else if(dir == GuardDirection.LEFT)
        {
            detect1.transform.position = transform.position;
            detect2.transform.position = transform.position;
            detect3.transform.position = transform.position;

            detect1.transform.position = new Vector3(detect1.transform.position.x - 1, detect1.transform.position.y, detect1.transform.position.z);
            detect2.transform.position = new Vector3(detect2.transform.position.x - 2, detect2.transform.position.y, detect2.transform.position.z);
            detect3.transform.position = new Vector3(detect3.transform.position.x - 3, detect3.transform.position.y, detect3.transform.position.z);
        }

        else if(dir == GuardDirection.RIGHT)
        {
            detect1.transform.position = transform.position;
            detect2.transform.position = transform.position;
            detect3.transform.position = transform.position;

            detect1.transform.position = new Vector3(detect1.transform.position.x + 1, detect1.transform.position.y, detect1.transform.position.z);
            detect2.transform.position = new Vector3(detect2.transform.position.x + 2, detect2.transform.position.y, detect2.transform.position.z);
            detect3.transform.position = new Vector3(detect3.transform.position.x + 3, detect3.transform.position.y, detect3.transform.position.z);
        }

        else
        {
            //DEFAULT
        }
    }

    private void OnBeat()
    {
        //check for correct x location
        lastPos = transform.position;
        Vector3 targetPos = NavPoints[currentTarget].transform.position;

        if(targetPos.x != transform.position.x)
        {
            if(targetPos.x < transform.position.x)
            {
                SwitchSprite(GuardDirection.DOWN);
                AlignDetectRange(GuardDirection.LEFT);
                movingLeft = true;
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            }

            else if(targetPos.x > transform.position.x)
            {
                SwitchSprite(GuardDirection.DOWN);
                AlignDetectRange(GuardDirection.RIGHT);
                movingRight = true;
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            }

            else
            {
                Debug.Log("ERR: Guard could not move in X axis");
            }
        }

        else if(targetPos.y != transform.position.y)
        {
            if (targetPos.y < transform.position.y)
            {
                SwitchSprite(GuardDirection.DOWN);
                AlignDetectRange(GuardDirection.UP);
                movingUp = true;
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            }

            else if (targetPos.y > transform.position.y)
            {
                SwitchSprite(GuardDirection.UP);
                AlignDetectRange(GuardDirection.DOWN);
                movingDown = true;
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            }

            else
            {
                Debug.Log("ERR: Guard could not move in Y axis");
            }
        }

        else//nailed it
        {
            NextTarget();
            OnBeat();//rerun this to take a step toward next thing
        }

         
        //Check if moving
        if(movingUp || movingDown || movingLeft || movingRight)
        {
            if(movingUp)
            {
                //if we turn
                if(movingDown || movingLeft || movingRight)
                {
                    movingUp = false;
                    movingDown = false;
                    movingLeft = false;
                    movingRight = false;

                    detect1Script.detect1Hit = false;
                    detect2Script.detect2Hit = false;
                    detect3Script.detect3Hit = false;
                }
            }

            else if (movingDown)
            {
                //if we turn
                if (movingUp || movingLeft || movingRight)
                {
                    movingUp = false;
                    movingDown = false;
                    movingLeft = false;
                    movingRight = false;

                    detect1Script.detect1Hit = false;
                    detect2Script.detect2Hit = false;
                    detect3Script.detect3Hit = false;
                }
            }

            else if (movingLeft)
            {
                //if we turn
                if (movingDown || movingUp || movingRight)
                {
                    movingUp = false;
                    movingDown = false;
                    movingLeft = false;
                    movingRight = false;

                    detect1Script.detect1Hit = false;
                    detect2Script.detect2Hit = false;
                    detect3Script.detect3Hit = false;
                }
            }

            else if (movingRight)
            {
                //if we turn
                if (movingDown || movingUp || movingLeft)
                {
                    movingUp = false;
                    movingDown = false;
                    movingLeft = false;
                    movingRight = false;

                    detect1Script.detect1Hit = false;
                    detect2Script.detect2Hit = false;
                    detect3Script.detect3Hit = false;
                }
            }

            else
            {

            }
        }
    }

    private void NextTarget()
    {
        currentTarget++;
        if(currentTarget >= NavPoints.Length)
        {
            currentTarget = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.name.Contains("Player"))
        {
            PlayerScript player;
            player = coll.gameObject.GetComponentInParent<PlayerScript>();
            if (player.cheats)
            {
                Debug.Log("The power of CHEATING protects you!");
                return;
            }

            //Debug.Log("GUARD HIT" + " " + transform.name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
