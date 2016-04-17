using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour {

    Vector3 unpressedPosition;
    public bool onePressButton;
    public int timeToPress = 1;
    public DoorBehavior[] attachedDoors;
    public ButtonIndicatorBehavior indicator;

    bool _pressed;
    public bool pressed
    {
        get
        {
            return _pressed;
        }
    }

    // Use this for initialization
    void Start () {
        unpressedPosition = transform.GetChild(0).transform.localPosition;
	}

    void OnTriggerEnter()
    {
        transform.GetChild(0).transform.localPosition = Vector3.zero;

        if (timeToPress > 0)
        {
            timeToPress -= 1;
            if (indicator)
            {
                indicator.TriggerIndicator(timeToPress);
            }
        }
        
        if (timeToPress == 0)
        {
            _pressed = true;
            foreach (var door in attachedDoors)
            {
                door.GetComponent<DoorBehavior>().Open();
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
                _pressed = false;
                foreach (var door in attachedDoors)
                {
                    door.GetComponent<DoorBehavior>().Close();
                }
            }
        }
    }
}
