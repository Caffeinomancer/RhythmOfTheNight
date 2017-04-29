using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraDetection : MonoBehaviour {

    public int DetectionNumber;

    private Vector2 startingPos;
    private Renderer localRenderer;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
        localRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FaceForward()
    {
        transform.position = startingPos;
        if(DetectionNumber == 1 || DetectionNumber == 5 || DetectionNumber == 4 || DetectionNumber == 2)
        {
            localRenderer.enabled = true;
        }
    }

    public void FaceLeft()
    {
        if(DetectionNumber == 4 || DetectionNumber == 5)
        {
            localRenderer.enabled = false;
        }
    }

    public void FaceRight()
    {
        if (DetectionNumber == 1 || DetectionNumber == 2)
        {
            localRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.name.Contains("Player") && localRenderer.enabled == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
