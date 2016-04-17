using UnityEngine;
using System.Collections;

public class ButtonDelayBehavior : MonoBehaviour {

    Vector3 unpressedPosition;
    public bool onePressButton;
    public DoorBehavior[] attachedDoors;
    public float delay = 3f;

    public AudioClip soundOn;
    public AudioClip soundOff;

    public GaugeBehavior gauge;
    float _currentDelay = 0f;
    float currentDelay
    {
        get
        {
            return _currentDelay;
        }
        set
        {
            _currentDelay = value;
            gauge.progress = _currentDelay / delay;
        }
    }

    bool _pressed;
    public bool pressed
    {
        get
        {
            return _pressed;
        }
    }

    bool _presence;
    public bool presence
    {
        get
        {
            return _presence;
        }
    }

    // Use this for initialization
    void Start () {
        unpressedPosition = transform.GetChild(0).transform.localPosition;
	}

    void Update()
    {
        if (presence)
        {
            currentDelay += Time.deltaTime;
            if (currentDelay > delay && pressed == false)
            {
                _pressed = true;
                foreach (var door in attachedDoors)
                {
                    door.GetComponent<DoorBehavior>().Open();
                }
            }
        }
    }

    void OnTriggerEnter()
    {
        transform.GetChild(0).transform.localPosition = Vector3.zero;

        _presence = true;
        var audioSrc = GetComponent<AudioSource>();
        audioSrc.Stop();
        audioSrc.clip = soundOn;
        audioSrc.Play();
    }

    void OnTriggerExit()
    {
        transform.GetChild(0).transform.localPosition = unpressedPosition;

        _presence = false;
        var audioSrc = GetComponent<AudioSource>();
        audioSrc.Stop();
        audioSrc.clip = soundOff;
        audioSrc.Play();

        currentDelay = 0;
        if (pressed)
        {
            if (onePressButton == false)
            {
                _pressed = false;
                foreach (var door in attachedDoors)
                {
                    door.GetComponent<DoorBehavior>().Close();
                }
            }
        }
    }
}
