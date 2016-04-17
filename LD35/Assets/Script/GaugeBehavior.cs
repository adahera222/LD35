using UnityEngine;
using System.Collections;

public class GaugeBehavior : MonoBehaviour {

    [Range(0f, 1f)]
    public float lerp;

    float _progress;
	public float progress
    {
        get
        {
            return _progress;
        }
        set
        {
            _progress = Mathf.Clamp01(value);
        }
    }
    float displayedProgress = 0f;

    void Update()
    {
        displayedProgress = Mathf.Lerp(_progress, displayedProgress, lerp);
        transform.localScale = new Vector3(displayedProgress, 1, 1);
    }
}
