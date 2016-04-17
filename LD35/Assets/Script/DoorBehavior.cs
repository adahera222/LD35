using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

    public ParticleSystem brokenParticleSystem;
    public bool openBrokes = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Open()
    {
        if (openBrokes)
        {
            Break();
        }
        else
        {
            GetComponent<Animator>().SetTrigger("open");
        }
    }

    public void Close()
    {
        GetComponent<Animator>().SetTrigger("close");
    }

    public void Break()
    {
        GetComponent<Animator>().SetTrigger("break");
        if (brokenParticleSystem!= null)
        {
            brokenParticleSystem.Play();
        }
    }
}
