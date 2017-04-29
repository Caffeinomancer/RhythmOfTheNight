using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DetectionRange : MonoBehaviour {

    public bool detect1Hit = false;
    public bool detect2Hit = false;
    public bool detect3Hit = false;

    public bool turned = false;

    EventDispatcher eventDispatcher;
    UnityEvent BeatEvent;

    Renderer ren;

    // Use this for initialization
    void Start () {
        eventDispatcher = EventDispatcher.Instance;

        BeatEvent = new UnityEvent();
        BeatEvent.AddListener(OnBeat);
        eventDispatcher.RegisterBeatListener(ref BeatEvent);

        ren = transform.GetComponent<Renderer>();
    }

    private void OnBeat()
    {
        if(turned)
        {
            detect1Hit = false;
            detect2Hit = false;
            detect3Hit = false;
            turned = false;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (transform.name == "Detect1" && (coll.name.Contains("wall") || coll.name.Contains("Wall")))
        {
            detect1Hit = true;
        }

        else if(transform.name == "Detect2" && (coll.name.Contains("wall") || coll.name.Contains("Wall")))
        {
            detect2Hit = true;
        }

        else if (transform.name == "Detect3" && (coll.name.Contains("wall") || coll.name.Contains("Wall")))
        {
            detect3Hit = true;
        }

        else
        {

        }

        if (coll.gameObject.name.Contains("Player"))
        {
            PlayerScript player;
            player = coll.gameObject.GetComponentInParent<PlayerScript>();
            if (player.cheats)
            {
                Debug.Log("The power of CHEATING protects you!");
                return;
            }
            Debug.Log("Busted!");


            if(transform.name == "Detect1")
            {
                if(!detect1Hit)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

            else if (transform.name == "Detect2")
            {
                if (!detect2Hit)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

            else if (transform.name == "Detect3")
            {
                if (!detect3Hit)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }
    }
}
