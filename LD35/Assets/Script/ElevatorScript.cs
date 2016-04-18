using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ElevatorType
{
    start,
    end,
    intermediate
}

public class ElevatorScript : MonoBehaviour {

    public string nextLevelName;
    public DoorBehavior attachedDoor;
    public Image fadeScreen;
    public ElevatorType elevatorType;

    float cursorLerpColor;
    bool fadeInAction = false;
    bool fadeOutAction = false;

    void Update()
    {
        if (fadeInAction && elevatorType == ElevatorType.end)
        {
            if (cursorLerpColor < 1)
            {
                cursorLerpColor = Mathf.Min(cursorLerpColor + Time.deltaTime, 1);

                fadeScreen.color = new Color(0f, 0f, 0f, cursorLerpColor);

                if (cursorLerpColor >= 1)
                {
                    fadeInAction = false;
                    LoadNextLevel();
                }
            }
        }

        else if (fadeOutAction && elevatorType == ElevatorType.start)
        {
            if (cursorLerpColor < 1)
            {
                cursorLerpColor = Mathf.Min(cursorLerpColor + Time.deltaTime, 1);

                fadeScreen.color = new Color(0f, 0f, 0f, 1f -cursorLerpColor);

                if (cursorLerpColor >= 1)
                {
                    fadeOutAction = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (elevatorType == ElevatorType.end)
            {
                GetComponent<Animator>().SetTrigger("end level");
            }
        }
    }

    public void FadeIn()
    {
        cursorLerpColor = 0f;
        fadeInAction = true;
    }

    public void FadeOut()
    {
        cursorLerpColor = 0f;
        fadeOutAction = true;
    }

    public void CloseEndDoor()
    {
        if (elevatorType == ElevatorType.end)
        {
            if (attachedDoor)
            {
                attachedDoor.Close();
            }
        }
    }

    public void OpenStartDoor()
    {
        if (elevatorType == ElevatorType.start)
        {
            if (attachedDoor)
            {
                attachedDoor.Open();
            }
        }
    }

    public void LoadNextLevel()
    {
        if (nextLevelName != null)
        {
            Application.LoadLevel(nextLevelName);
        }
    }

}
