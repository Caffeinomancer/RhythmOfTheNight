using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public enum SoundType
{
    CLOSE = 0,
    NEAR,
    FAR
}

public enum SoundSource
{
    LASER_ON = 0,
    LASER_OFF,
    CAMERA,
    GUARD
}

public struct PlayableSound
{
    public SoundType type;
    public SoundSource source;
}

public class SoundSystem : MonoBehaviour {

    private static SoundSystem _instance;
    public static SoundSystem Instance { get { return _instance; } }

    EventDispatcher eventDispatcher;
    UnityEvent preBeatEvent; //formerly beat event here.

    List<PlayableSound> SoundsToPlayList = new List<PlayableSound>(1);

    public AudioClip mainBeat1;
    public AudioClip mainBeat2;
    public AudioClip laserOn;//laser0
    public AudioClip laser1;
    public AudioClip laser2;
    public AudioClip laser3;
    public AudioClip laserOff;
    public AudioClip camera;
    public AudioClip guardStepOne;
    public AudioClip guardStepTwo;
    public AudioClip guardStepThree;
    public AudioClip guardStepFour;

    private AudioSource source;
    public AudioSource beatSource;
    public AudioSource laserSource;
    public AudioSource guardSource;

    int laserCount=0;

    public float lowSoundLevel = .5f;
    public float medSoundLevel = 1.0f;
    public float highSoundLevel = 1.5F;
    public float metronomeSoundLevel = .25f;

    private float velToVol = .2F;
    private float velocityClipSplit = 10F;

    private bool playLaserOn = false,
                 playLaserOff = false,
                 playCamera = false,
                 playGuard2 = false,
                 playGuard3 = false,
                 playGuard4 = false;

    public bool soundsOn;
    public bool metronomeOn;
    private bool beatOne = true;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        //guardSource = GetComponent<AudioSource>();
        //laserSource = GetComponent<AudioSource>();

        guardSource.clip = guardStepOne;
        //guardSource.Pause();

        eventDispatcher = EventDispatcher.Instance;

        preBeatEvent = new UnityEvent();
        preBeatEvent.AddListener(PlaySounds);
        eventDispatcher.RegisterBeatListener(ref preBeatEvent); 
    }

    private void Awake()
    {
        if (_instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        {
            _instance = this;
        }
}

    // Update is called once per frame
    void Update () {

	}

    public void AddSound(PlayableSound sound)
    {
        SoundsToPlayList.Add(sound);  
    }

    private void PlaySounds()
    {
        if (soundsOn)
        {
            if (SoundsToPlayList.Count > 0)
                for (int i = 0; i < SoundsToPlayList.Count; i++)
                {
                    PlayableSound sound = SoundsToPlayList[i];
                    PlaySound(sound);
                }

            SoundsToPlayList.Clear();
        }
        if (metronomeOn)
        {
            if (beatOne)
            {
                source.PlayOneShot(mainBeat1, metronomeSoundLevel);//play a sound on beat
                beatOne = false;
            }

            else
            {
                source.PlayOneShot(mainBeat2, metronomeSoundLevel);//play a sound on beat
                beatOne = true;
            }
        }
    }

    /*private void FadeIn()
    {
        if (audio2Volume < 1)
        {
            audio2Volume += 0.1 * Time.deltaTime;
            audio.volume = audio2Volume;
        }
    }*/

    private void PlaySound(PlayableSound sound)
    {

        if (sound.source == SoundSource.LASER_ON)
        {
            if (!laserSource.isPlaying)
            {
                if (laserCount == 0)
                {
                    laserSource.PlayOneShot(laserOn, medSoundLevel);
                    laserCount++;
                }
                else if (laserCount == 1)
                {
                    laserSource.PlayOneShot(laser1, medSoundLevel);
                    laserCount++;
                }
                else if (laserCount == 2)
                {
                    laserSource.PlayOneShot(laser2, medSoundLevel);
                    laserCount++;
                }
                else
                {
                    laserSource.PlayOneShot(laser3, medSoundLevel);
                    laserCount = 0;
                }
                
            }
        }
        else if (sound.source == SoundSource.LASER_OFF)
        {
            if (!laserSource.isPlaying)
            {
                laserSource.PlayOneShot(laserOff, medSoundLevel);
            }   
        }
        else if (sound.source == SoundSource.CAMERA)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(camera, medSoundLevel);
            }
        }
        else if (sound.source == SoundSource.GUARD)
        {
            //TODO: TIE THIS IN WITH SOUND DETECTION SO THAT ONE GUARD DOESNT TRIGGER SO SECOND PLAYS SECOND SOUND
            Debug.Log("checking if guard should play...");
            if (!guardSource.isPlaying)
            {
                Debug.Log("Playing guard...");
                guardSource.PlayOneShot(guardStepOne, medSoundLevel);
                //guardSource.volume = highSoundLevel;//medSoundLevel;
                //guardSource.Play();
                //guardSource.UnPause();
            }

            /*if (!playGuard2 && !playGuard3 && !playGuard4)
            {
                if (sound.type == SoundType.CLOSE)
                {
                    source.PlayOneShot(guardStepOne, highSoundLevel);
                }
                else if (sound.type == SoundType.NEAR)
                {
                    source.PlayOneShot(guardStepOne, medSoundLevel);

                }
                else
                {
                    source.PlayOneShot(guardStepOne, lowSoundLevel);

                }
                playGuard2 = true;
            }

            else if (playGuard2 && !playGuard3 && !playGuard4)
            {
                if (sound.type == SoundType.CLOSE)
                {
                    source.PlayOneShot(guardStepTwo, highSoundLevel);
                }
                else if (sound.type == SoundType.NEAR)
                {
                    source.PlayOneShot(guardStepTwo, medSoundLevel);

                }
                else
                {
                    source.PlayOneShot(guardStepTwo, lowSoundLevel);

                }
                playGuard3 = true;
                playGuard2 = false;
            }

            else if (!playGuard2 && playGuard3 && !playGuard4)
            {
                if (sound.type == SoundType.CLOSE)
                {
                    source.PlayOneShot(guardStepThree, highSoundLevel);
                }
                else if (sound.type == SoundType.NEAR)
                {
                    source.PlayOneShot(guardStepThree, medSoundLevel);

                }
                else
                {
                    source.PlayOneShot(guardStepThree, lowSoundLevel);

                }
                playGuard3 = false;
                playGuard4 = true;
            }

            else if (!playGuard2 && playGuard3 && !playGuard4)
            {
                if (sound.type == SoundType.CLOSE)
                {
                    source.PlayOneShot(guardStepThree, highSoundLevel);
                }
                else if (sound.type == SoundType.NEAR)
                {
                    source.PlayOneShot(guardStepThree, medSoundLevel);

                }
                else
                {
                    source.PlayOneShot(guardStepThree, lowSoundLevel);

                }
                playGuard3 = false;
                playGuard4 = true;
            }

            else if (!playGuard2 && !playGuard3 && playGuard4)
            {
                if (sound.type == SoundType.CLOSE)
                {
                    source.PlayOneShot(guardStepFour, highSoundLevel);
                }
                else if (sound.type == SoundType.NEAR)
                {
                    source.PlayOneShot(guardStepFour, medSoundLevel);

                }
                else
                {
                    source.PlayOneShot(guardStepFour, lowSoundLevel);

                }
                
                playGuard4 = false;
            }*/

        }


    }

}
