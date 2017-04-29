using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DynamicBeam : MonoBehaviour {

    enum LaserState
    {
        On = 0,
        Off
    }

    float spawnDelay = 0.0001f;
    float spawnCount = 0;
    bool spawnComplete = false;

    public int onCount;
    public int offCount;
    public int beatCount;

    public int maxLength;

    EventDispatcher eventDispatcher;
    UnityEvent BeatEvent;

    SpriteRenderer spRenderer;

    public bool Is_On;

    // Use this for initialization
    void Start () {
        eventDispatcher = EventDispatcher.Instance;

        BeatEvent = new UnityEvent();
        BeatEvent.AddListener(OnBeat);
        eventDispatcher.RegisterBeatListener(ref BeatEvent);

        spRenderer = this.transform.GetComponent<SpriteRenderer>();

        if (Is_On)
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
        if (!spawnComplete)
        {
            if (spawnCount >= spawnDelay && maxLength>0)
            {
                SpawnBeam();
                spawnComplete = true;
            }
            else
            {
                spawnCount += Time.deltaTime;
            }
        }
	}

    void SpawnBeam()
    {
        GameObject beam = Instantiate(Resources.Load("BeamPrefab", typeof(GameObject))) as GameObject;
        beam.transform.parent = this.transform;
        //beam.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        beam.transform.localPosition = new Vector3(0, 1, 0);
        //beam.transform.rotation.eulerAngles.Set(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
        beam.transform.localRotation = Quaternion.identity;
        DynamicBeam beamScript = beam.GetComponent<DynamicBeam>();
        beamScript.onCount = this.onCount;
        beamScript.offCount = this.offCount;
        beamScript.beatCount = this.beatCount;
        beamScript.Is_On = this.Is_On;
        beamScript.maxLength = maxLength - 1;
        //Sprite.Create(beamTex, new Rect(new Vector2(0, 0), texSize), new Vector2(0, 0));
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.name.Contains("wall" )|| coll.gameObject.name.Contains("Door"))
        {
            spawnComplete = true;
            DestroyObject(gameObject);
        }

        else if (coll.gameObject.name.Contains("Player") && Is_On)
        {
            Debug.Log("Player found");
            PlayerScript player;
            player = coll.gameObject.GetComponentInParent<PlayerScript>();
            if (player.cheats)
            {
                Debug.Log("The power of CHEATING protects you!");
                return;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnBeat()
    {
        beatCount++;
        if (Is_On)
        {
            if (beatCount < onCount) { return; }//exit early if it's not time to switch

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
        if (state == LaserState.On)
        {
            spRenderer.enabled = true;
            
        }

        else
        {
            spRenderer.enabled = false;

        }
    }
}
