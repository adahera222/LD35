using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsBehavior : MonoBehaviour
{

    public string nextScene;
    public Image fadeImage;
    float currentFade = 0f;
    ActionAfterAnimation? actionAfter;
    bool showMenu = true;

    float fadeDuration = 1f;

    // Use this for initialization
    void Start()
    {
        currentFade = 0f;
        fadeImage.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (showMenu)
        {
            currentFade += Time.deltaTime / fadeDuration;

            fadeImage.color = new Color(0, 0, 0, 1f - Mathf.Clamp01(currentFade));

            if (currentFade >= 1)
            {
                showMenu = false;
                fadeImage.enabled = false;
            }
        }

        if (actionAfter != null)
        {
            currentFade += Time.deltaTime;

            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(currentFade));
            if (currentFade >= 1)
            {
                var action = actionAfter.Value;
                switch (action)
                {
                    case ActionAfterAnimation.menu:
                        Application.LoadLevel(nextScene);
                        break;
                }
            }
        }
    }

    public void BackToMenu()
    {
        actionAfter = ActionAfterAnimation.menu;
        currentFade = 0f;
        fadeImage.enabled = true;
    }
}
