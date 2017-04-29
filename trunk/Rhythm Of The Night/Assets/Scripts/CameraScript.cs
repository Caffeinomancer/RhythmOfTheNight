using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    // Use this for initialization
    public float Camera_Move_Bounds_X = 3.0f;
    public float Camera_Move_Bounds_Y = 2.0f;
    const float Z_CAMERA_POS = -10.0f;

    public float Camera_Speed = 0.05f;

    bool moveUp = false, moveDown = false, moveLeft = false, moveRight = false;
    bool movingCamera = false;

    Vector2 currPos, currPosMoving, newPosMoving;

    GameObject player;
    PlayerScript playerScript;

	void Start () {
        player = GameObject.Find("Player");
        playerScript = player.transform.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCameraLocation();
	}

    private void UpdateCameraLocation()
    {
        if(!movingCamera)
        {
            if (player.transform.position.x > transform.position.x + Camera_Move_Bounds_X)
            {
                newPosMoving = new Vector2(transform.position.x + 1, transform.position.y);
                currPosMoving = transform.position;
                moveRight = true;
                movingCamera = true;
                //transform.position = new Vector3(player.transform.position.x, transform.position.y, Z_CAMERA_POS);
            }

            else if (player.transform.position.x < transform.position.x - Camera_Move_Bounds_X)
            {
                newPosMoving = new Vector2(transform.position.x - 1, transform.position.y);
                currPosMoving = transform.position;
                moveLeft = true;
                movingCamera = true;
                //transform.position = new Vector3(player.transform.position.x, transform.position.y, Z_CAMERA_POS);
            }

            else if (player.transform.position.y > transform.position.y + Camera_Move_Bounds_Y)
            {
                newPosMoving = new Vector2(transform.position.x, transform.position.y + 1);
                currPosMoving = transform.position;
                moveUp = true;
                movingCamera = true;
                //transform.position = new Vector3(transform.position.x, player.transform.position.y, Z_CAMERA_POS);
            }

            else if (player.transform.position.y < transform.position.y - Camera_Move_Bounds_Y)
            {
                newPosMoving = new Vector2(transform.position.x, transform.position.y - 1);
                currPosMoving = transform.position;
                moveDown = true;
                movingCamera = true;
                //transform.position = new Vector3(transform.position.x, player.transform.position.y, Z_CAMERA_POS);
            }

            else
            {
                //default
            }
        }
        

        if(moveUp)
        {
            if (transform.position.y < newPosMoving.y)
            {
                transform.position = new Vector3(currPosMoving.x, currPosMoving.y + Camera_Speed, Z_CAMERA_POS);
                currPosMoving = transform.position;
            }

            else
            {
                currPos = transform.position;
                moveUp = false;
                movingCamera = false;
            }
        }

        else if (moveDown)
        {
            if (transform.position.y > newPosMoving.y)
            {
                transform.position = new Vector3(currPosMoving.x, currPosMoving.y - Camera_Speed, Z_CAMERA_POS);
                currPosMoving = transform.position;
            }

            else
            {
                currPos = transform.position;
                moveDown = false;
                movingCamera = false;
            }
        }

        else if (moveLeft)
        {
            if (transform.position.x > newPosMoving.x)
            {
                transform.position = new Vector3(currPosMoving.x - Camera_Speed, currPosMoving.y, Z_CAMERA_POS);
                currPosMoving = transform.position;
            }

            else
            {
                currPos = transform.position;
                moveLeft = false;
                movingCamera= false;
            }
        }

        else if (moveRight)
        {
            if (transform.position.x < newPosMoving.x)
            {
                transform.position = new Vector3(currPosMoving.x + Camera_Speed, currPosMoving.y, Z_CAMERA_POS);
                currPosMoving = transform.position;
            }

            else
            {
                currPos = transform.position;
                moveRight = false;
                movingCamera = false;
            }
        }

        else
        {
           //default
        }
    }
}
