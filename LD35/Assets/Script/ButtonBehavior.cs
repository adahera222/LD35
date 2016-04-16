using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour {

    Vector3 unpressedPosition;
    public bool onePressButton;
    public int timeToPress = 1;
    public DoorBehavior[] attachedDoors;

    [HideInInspector]
    public bool pressed = false;

	// Use this for initialization
	void Start () {
        unpressedPosition = transform.GetChild(0).transform.localPosition;
	}

    void OnTriggerEnter()
    {
        transform.GetChild(0).transform.localPosition = Vector3.zero;
        timeToPress -= 1;

        if (timeToPress == 0)
        {
            pressed = true;
            foreach (var door in attachedDoors)
            {
                door.GetComponent<Animator>().SetTrigger("open");
            }
        }
    }

    void OnTriggerExit()
    {
        transform.GetChild(0).transform.localPosition = unpressedPosition;
        if (timeToPress == 0)
        {
            if (onePressButton == false)
            {
                pressed = false;
                foreach (var door in attachedDoors)
                {
                    door.GetComponent<Animator>().SetTrigger("close");
                }
            }
        }
    }
}
