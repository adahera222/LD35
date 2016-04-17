using UnityEngine;
using System.Collections;

public class ButtonIndicatorCheckAllPollBehavior: MonoBehaviour {

    public ButtonIndicatorPollBehavior[] allIndicator;

    public bool singleUse = false;
    public Material[] materials;

    public DoorBehavior[] attachedDoors;

    bool _activated = false;
    public bool activated
    {
        get
        {
            return _activated;
        }
        private set
        {
            _activated = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!singleUse || !activated)
        {
            bool allActivated = true;
	        foreach (var indicator in allIndicator)
            {
                allActivated = allActivated && indicator.activated;
            }

            if (allActivated != activated)
            {
                activated = allActivated;
                transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[activated ? 1 : 0];

                foreach (var door in attachedDoors)
                {
                    if (activated)
                    {
                        door.GetComponent<DoorBehavior>().Open();
                    }
                    else
                    {
                        door.GetComponent<DoorBehavior>().Close();
                    }
                }
            }
        }
	}
}
