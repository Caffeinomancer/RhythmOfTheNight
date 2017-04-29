using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class LockedDoorScript : MonoBehaviour {

    public string KeyID1;
    public string KeyID2;
    public string KeyID3;

    private int numRequiredKeys;

    private bool hasKeys = false;

    private GameObject player;
    public List<string> playersInventory;

    EventDispatcher eventDispatcher;
    UnityEvent pickUpEvent;

    // Use this for initialization
    void Start () {
        eventDispatcher = EventDispatcher.Instance;

        pickUpEvent = new UnityEvent();
        pickUpEvent.AddListener(ItemPickedUp);
        eventDispatcher.RegisterPickUpListener(ref pickUpEvent);

        player = GameObject.Find("P1");

        if(KeyID3 != "")
        {
            numRequiredKeys = 3;
        }

        else if(KeyID2 != "")
        {
            numRequiredKeys = 2;
        }

        else
        {
            numRequiredKeys = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void ItemPickedUp()
    {
        playersInventory = player.GetComponent<PlayerScript>().getPickUpList();
        checkInventory();
        if(hasKeys)
        {
            Destroy(gameObject);
        }
    }

    void checkInventory()
    {
        if(playersInventory.Count >= 1)
        {
            if (numRequiredKeys == 1)
            {
                if (playersInventory[0] == KeyID1)
                {
                    hasKeys = true;
                }
            }
        }
        //TODO: KEYS BUST BE PICKED UP IN SEQUENTIAL ORDER FIX BUG LATER
        if(playersInventory.Count >= 2)
        {
            if (numRequiredKeys == 2)
            {
                if (playersInventory[0] == KeyID1)
                {
                    if (playersInventory[1] == KeyID2)
                    {
                        hasKeys = true;
                    }
                }
            }
        }
        

        if(playersInventory.Count >= 3)
        {
            if (playersInventory[0] == KeyID1)
            {
                if (playersInventory[1] == KeyID2)
                {
                    if (playersInventory[2] == KeyID3)
                    {
                        hasKeys = true;
                    }
                }
            }
        }
    }
}
