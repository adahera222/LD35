using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

    public ParticleSystem brokenParticleSystem;
    public bool openBrokes = false;
    public AudioClip doorOpen;
    public AudioClip doorBroken;

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
            var audioSrc = GetComponent<AudioSource>();
            audioSrc.Stop();
            audioSrc.clip = doorOpen;
            audioSrc.Play();
        }
    }

    public void Close()
    {
        GetComponent<Animator>().SetTrigger("close");
        var audioSrc = GetComponent<AudioSource>();
        audioSrc.Stop();
        audioSrc.clip = doorOpen;
        audioSrc.Play();
    }

    public void Break()
    {
        GetComponent<Animator>().SetTrigger("break");
        var audioSrc = GetComponent<AudioSource>();
        audioSrc.Stop();
        audioSrc.clip = doorBroken;
        audioSrc.Play();
        if (brokenParticleSystem!= null)
        {
            brokenParticleSystem.Play();
        }
    }
}
