using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public Texture2D beamTex;
    public Vector2 texSize;

	// Use this for initialization
	void Start () {
        SpawnBeam();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnBeam()
    {
        GameObject beam = Instantiate(Resources.Load("BeamPrefab", typeof(GameObject))) as GameObject;
        beam.transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        //Sprite.Create(beamTex, new Rect(new Vector2(0, 0), texSize), new Vector2(0, 0));
    }
}
