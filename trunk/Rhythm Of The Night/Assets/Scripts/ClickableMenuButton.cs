using UnityEngine;
using System.Collections;

public class ClickableMenuButton : MonoBehaviour {

    bool playHeldBool = false;
    bool controlsHeldBool = false;
    bool creditsHeldBool = false;
    bool quitHeldBool = false;

    bool creditsUp = false;
    bool controlsUp = false;

    SpriteRenderer sr;
    public Sprite play;
    public Sprite playHeld;

    public Sprite controls;
    public Sprite controlsHeld;

    public Sprite credits;
    public Sprite creditsHeld;

    public Sprite quit;
    public Sprite quitHeld;

    Renderer controlsRenderer;
    Renderer creditsRenderer;
    GameObject controlsObject;
    GameObject creditsObject;

    // Use this for initialization
    void Start ()
    {
        sr = GetComponent<SpriteRenderer>();

        if(transform.name == "Controls")
        {
            controlsObject = transform.gameObject;
            controlsRenderer = transform.FindChild("ControlsScreen").GetComponent<Renderer>();
            creditsObject = GameObject.Find("Credits");
            creditsRenderer = creditsObject.transform.FindChild("CreditsScreen").GetComponent<Renderer>();
        }

        else if(transform.name == "Credits")
        {
            creditsObject = transform.gameObject;
            creditsRenderer = transform.FindChild("CreditsScreen").GetComponent<Renderer>();
            controlsObject = GameObject.Find("Controls");
            controlsRenderer = controlsObject.transform.FindChild("ControlsScreen").GetComponent<Renderer>();
        }
        
        else
        {

        }

        if(transform.name == "Credits" || transform.name == "Controls")
        {
            controlsRenderer.enabled = false;
            creditsRenderer.enabled = false;
        }

        playHeldBool = false;
        controlsHeldBool = false;
        creditsHeldBool = false;
        quitHeldBool = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        CheckInput();
	}

    private void CheckInput()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
            if (hitInfo)
            {
                if(transform.name == "Start")
                {
                    if (hitInfo.transform.gameObject.name == "Start")
                    {
                        sr.sprite = playHeld;
                        playHeldBool = true;
                    }
                }

                else if(transform.name == "Controls")
                {
                    if (hitInfo.transform.gameObject.name == "Controls")
                    {
                        sr.sprite = controlsHeld;
                        controlsHeldBool = true;
                    }
                }

                else if (transform.name == "Credits")
                {
                    if (hitInfo.transform.gameObject.name == "Credits")
                    {
                        sr.sprite = creditsHeld;
                        creditsHeldBool = true;
                    }
                }

                else if (transform.name == "Quit")
                {
                    if (hitInfo.transform.gameObject.name == "Quit")
                    {
                        sr.sprite = quitHeld;
                        quitHeldBool = true;
                    }
                }

                else
                {

                }
            }

            else
            {
                if(transform.name == "Start")
                {
                    sr.sprite = play;
                    playHeldBool = false;
                }

                else if(transform.name == "Controls")
                {
                    sr.sprite = controls;
                    controlsHeldBool = false;
                }

                else if (transform.name == "Credits")
                {
                    sr.sprite = credits;
                    creditsHeldBool = false;
                }

                else if (transform.name == "Quit")
                {
                    sr.sprite = quit;
                    quitHeldBool = false;
                }

                else
                {

                }
            }
        }

        else
        {
            if(playHeldBool == true)
            {
                sr.sprite = play;
                playHeldBool = false;
                controlsHeldBool = false;
                creditsHeldBool = false;
                quitHeldBool = false;
                Application.LoadLevel("Level_1");
            }

            else if (controlsHeldBool == true)
            {
                sr.sprite = controls;

                if(creditsRenderer.enabled == true)
                {
                    creditsRenderer.enabled = false;
                }


                if(controlsRenderer.enabled == true)
                {
                    controlsRenderer.enabled = false;
                }
                else
                {
                    controlsRenderer.enabled = true;
                }

                playHeldBool = false;
                controlsHeldBool = false;
                creditsHeldBool = false;
                quitHeldBool = false;
            }

            else if (creditsHeldBool == true)
            {
                sr.sprite = credits;

                if (controlsRenderer.enabled == true)
                {
                    controlsRenderer.enabled = false;
                }


                if (creditsRenderer.enabled == true)
                {
                    creditsRenderer.enabled = false;
                }
                else
                {
                    creditsRenderer.enabled = true;
                }

                playHeldBool = false;
                controlsHeldBool = false;
                creditsHeldBool = false;
                quitHeldBool = false;
            }

            else if(quitHeldBool == true)
            {
                sr.sprite = quit;
                playHeldBool = false;
                controlsHeldBool = false;
                creditsHeldBool = false;
                quitHeldBool = false;
                Application.Quit();
            }

            else
            {
                if (transform.name == "Start")
                {
                    sr.sprite = play;
                }

                else if (transform.name == "Controls")
                {
                    sr.sprite = controls;
                }

                else if (transform.name == "Credits")
                {
                    sr.sprite = credits;
                }

                else if (transform.name == "Quit")
                {
                    sr.sprite = quit;
                }

                else
                {

                }
                playHeldBool = false;
                controlsHeldBool = false;
                creditsHeldBool = false;
                quitHeldBool = false;
            }
        }
    }
}
