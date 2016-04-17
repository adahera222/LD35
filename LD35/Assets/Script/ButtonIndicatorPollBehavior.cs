using UnityEngine;
using System.Collections;

public class ButtonIndicatorPollBehavior : MonoBehaviour {

    public ButtonBehavior buttonToPoll;

    public Material[] materials;

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
        if (activated != buttonToPoll.pressed)
        {
            activated = buttonToPoll.pressed;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[buttonToPoll.pressed ? 1 : 0];
        }
        
    }
}
