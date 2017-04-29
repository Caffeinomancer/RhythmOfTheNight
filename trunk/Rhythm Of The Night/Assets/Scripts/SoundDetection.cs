using UnityEngine;
using System.Collections;


public class SoundDetection : MonoBehaviour {

    /*TODO:
    Each sound type only plays once. Create system to offset sound start if multiple sounds to so sounds dont multiply
    */

    private SoundSystem soundSystem;

    private string[] nameSplit;
    private string myName;
    private SoundType type;

    private 

	// Use this for initialization
	void Start () {
        nameSplit = this.name.Split('(');
        myName = nameSplit[0];
        soundSystem = SoundSystem.Instance;
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayableSound sound;
        
        if (coll.gameObject.name.Contains("Laser"))
        {
            sound.source = SoundSource.LASER_ON;
            if (myName == "Laser")
            {
                
                sound.type = SoundType.CLOSE;
                soundSystem.AddSound(sound);
            }
            else if(myName == "NearSound")
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

        if(coll.gameObject.name.Contains("Laser_Off"))
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
        }
    }
}
