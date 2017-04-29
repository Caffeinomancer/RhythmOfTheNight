using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SoundDetectionRayCast : MonoBehaviour {

    EventDispatcher eventDispatcher;
    UnityEvent beatEvent;
    LayerMask layerMask;

    private SoundSystem soundSystem;

    bool sixSoundHit = false;

    // Use this for initialization
    void Start () {
        eventDispatcher = EventDispatcher.Instance;

        beatEvent = new UnityEvent();
        beatEvent.AddListener(BeatEvent);
        eventDispatcher.RegisterBeatListener(ref beatEvent);

        //Set layer mask for ray casting this means ray cast will ignore all objects not on that layer
        layerMask = -1;
        //
        soundSystem = SoundSystem.Instance;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void BeatEvent()
    {
        //Ray cast based on clock directions
        RaycastHit2D twelveOClock = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), Vector2.up);
        RaycastHit2D oneOClock = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y + 1), new Vector2(0.5f, 0.5f));
        RaycastHit2D threeOClock = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y), Vector2.right);
        RaycastHit2D fourOClock = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y - 1), new Vector2(0.5f, -0.5f));
        RaycastHit2D sixOClock = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), Vector2.down);
        RaycastHit2D sevenOClock = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y - 1), new Vector2(-0.5f, -0.5f));
        RaycastHit2D nineOClock = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y), Vector2.left);
        RaycastHit2D tenOClock = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y + 1), new Vector2(-0.5f, 0.5f));


        //
        if (twelveOClock.collider != null)
        {

            //Debug.Log("Twelve Collider: " + twelveOClock.collider.name);
            if (twelveOClock.collider.name.Contains("Guard"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Guard");
            }
            if (twelveOClock.collider.name.Contains("Laser")|| twelveOClock.collider.name.Contains("Beam"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Laser");
            }
        }

        if (oneOClock.collider != null)
        {
            //Debug.Log("One Collider: " + oneOClock.collider.name);
            if (oneOClock.collider.name.Contains("Guard"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Guard");
            }
            if (oneOClock.collider.name.Contains("Laser")|| oneOClock.collider.name.Contains("Beam"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Laser");
            }
        }

        if (threeOClock.collider != null)
        {
            //Debug.Log("Three Collider: " + threeOClock.collider.name);
            if (threeOClock.collider.name.Contains("Guard"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Guard");
            }
            if (threeOClock.collider.name.Contains("Laser")||threeOClock.collider.name.Contains("Beam"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Laser");
            }
        }

        if (fourOClock.collider != null)
        {
            //Debug.Log("Four Collider: " + fourOClock.collider.name);
            if (fourOClock.collider.name.Contains("Guard"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Guard");
            }
            if (fourOClock.collider.name.Contains("Laser")|| fourOClock.collider.name.Contains("Beam"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Laser");
            }
        }

        if (sixOClock.collider != null)
        {
            //Debug.Log("Six Collider: " + sixOClock.collider.name);
            if(sixOClock.collider.name.Contains("Laser") || sixOClock.collider.name.Contains("Beam"))
            {
                AddSound("Laser");
            }
            if (sixOClock.collider.name.Contains("Guard"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Guard");
            }
        }

        if (sevenOClock.collider != null)
        {
            //Debug.Log("Seven Collider: " + sevenOClock.collider.name);
            if (sevenOClock.collider.name.Contains("Guard"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Guard");
            }
            if (sevenOClock.collider.name.Contains("Laser")|| sevenOClock.collider.name.Contains("Beam"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Laser");
            }
        }

        if (nineOClock.collider != null)
        {
            //Debug.Log("Nine Collider: " + nineOClock.collider.name);
            if (nineOClock.collider.name.Contains("Guard"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Guard");
            }
            if (nineOClock.collider.name.Contains("Laser")|| nineOClock.collider.name.Contains("Beam"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Laser");
            }
        }

        if (tenOClock.collider != null)
        {
            //Debug.Log("Ten Collider: " + tenOClock.collider.name);
            if (tenOClock.collider.name.Contains("Guard"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Guard");
            }
            if (tenOClock.collider.name.Contains("Laser")|| tenOClock.collider.name.Contains("Beam"))
            {
                //Debug.Log("Queue guard sound...");
                AddSound("Laser");
            }
        }
    }

    private void AddSound(string name)
    {
        PlayableSound sound;

        if (name.Contains("Laser"))
        {
            sound.type = SoundType.NEAR;
            sound.source = SoundSource.LASER_ON;
            soundSystem.AddSound(sound);
        }

        else if (name.Contains("Beam"))
        {
            sound.type = SoundType.NEAR;
            sound.source = SoundSource.LASER_ON;
            soundSystem.AddSound(sound);
        }

        else if (name.Contains("Guard"))
        {
            Debug.Log("Guard queued.");
            sound.type = SoundType.NEAR;
            sound.source = SoundSource.GUARD;
            soundSystem.AddSound(sound);
        }

        else if (name.Contains("detect"))
        {
            sound.type = SoundType.NEAR;
            sound.source = SoundSource.GUARD;
            soundSystem.AddSound(sound);
        }

        /*if (coll.gameObject.name.Contains("Laser_Off"))
        {
            sound.source = SoundSource.LASER_OFF;
            if (myName == "CloseSound")
            {
                Debug.Log("COLLIDED");
                sound.type = SoundType.CLOSE;
                soundSystem.AddSound(sound);
            }
            else if (myName == "NearSound")
            {
                sound.type = SoundType.NEAR;
                soundSystem.AddSound(sound);
            }
            else
            {
                sound.type = SoundType.FAR;
                soundSystem.AddSound(sound);
            }
        }

        if (coll.gameObject.name.Contains("Camera"))
        {
            sound.source = SoundSource.CAMERA;
            if (myName == "CloseSound")
            {
                sound.type = SoundType.CLOSE;
                soundSystem.AddSound(sound);
            }
            else if (myName == "NearSound")
            {
                sound.type = SoundType.NEAR;
                soundSystem.AddSound(sound);
            }
            else
            {
                sound.type = SoundType.FAR;
                soundSystem.AddSound(sound);
            }
        }

        if (coll.gameObject.name.Contains("Guard"))
        {
            sound.source = SoundSource.GUARD;
            if (myName == "CloseSound")
            {
                sound.type = SoundType.CLOSE;
                soundSystem.AddSound(sound);
            }
            else if (myName == "NearSound")
            {
                sound.type = SoundType.NEAR;
                soundSystem.AddSound(sound);
            }
            else
            {
                sound.type = SoundType.FAR;
                soundSystem.AddSound(sound);
            }
        }*/
    }
}
