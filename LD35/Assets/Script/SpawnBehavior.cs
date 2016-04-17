using UnityEngine;
using System.Collections;

public class SpawnBehavior : MonoBehaviour {

    public ShapeBehavior shapeBehavior;
    public GameObject lastCheckpoint;

    public AudioClip killSound;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    [ContextMenu("Kill")]
    void Kill()
    {
        GetComponent<ParticleSystem>().Play();
        var audioSrc = GetComponent<AudioSource>();
        audioSrc.Stop();
        audioSrc.clip = killSound;
        audioSrc.Play();
        transform.localScale = Vector3.one;
        shapeBehavior.enabled = false;
        shapeBehavior.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        shapeBehavior.gameObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        shapeBehavior.enabled = true;
        var rigidBody = shapeBehavior.gameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        shapeBehavior.transform.position = lastCheckpoint.transform.position;
        shapeBehavior.gameObject.GetComponent<MeshRenderer>().enabled = true;
        shapeBehavior.Reinit();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Checkpoint")
        {
            if (lastCheckpoint != null && lastCheckpoint.GetComponent<MeshRenderer>() != null)
            {
                lastCheckpoint.GetComponent<MeshRenderer>().enabled = false;
            }
            collider.GetComponent<MeshRenderer>().enabled = true;
            lastCheckpoint = collider.gameObject;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            Kill();
        }
    }
}
