using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DynamicLaser : MonoBehaviour {

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

    public int maxLength = 10;

    public Texture2D beamTex;
    public Vector2 texSize;

    EventDispatcher eventDispatcher;
    UnityEvent BeatEvent;

    public bool Is_On;

    // Use this for initialization
    void Start () {
        eventDispatcher = EventDispatcher.Instance;
        SpawnBeam();

        BeatEvent = new UnityEvent();
        BeatEvent.AddListener(OnBeat);
        eventDispatcher.RegisterBeatListener(ref BeatEvent);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void SpawnBeam()
    {
        spawnComplete = true;
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

    private void OnBeat()
    {
        //Debug.Log("beat!!!");
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.name.Contains("Player"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
