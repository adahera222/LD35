using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Open()
    {
        GetComponent<Animator>().SetTrigger("open");
    }

    public void Close()
    {
        GetComponent<Animator>().SetTrigger("close");
    }
}
