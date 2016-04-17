using UnityEngine;
using System.Collections;

public class GrillTextureChangerBehavior : MonoBehaviour {

    public Material grillMaterial;
    [Range(0f, 0.5f)]
    public float amplitude;
    [Range(0f, 20f)]
    public float frequence;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var intensity = 0.5f + Mathf.Sin(Time.time * frequence) * amplitude;
        grillMaterial.SetColor("_EmissionColor", new Color(intensity, intensity, intensity, 1));
	}
}
