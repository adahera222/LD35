using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ActionAfterAnimation
{
    menu,
    start,
    credits,
    quit
}

public class MenuBehavior : MonoBehaviour {

    public string gameSceneName;
    public string creditsSceneName;
    public Image fadeImage;
    float currentFade = 1f;
    ActionAfterAnimation? actionAfter;
    bool showMenu = true;

    float fadeDuration = 1f;

    // Use this for initialization
    void Start () {
        currentFade = 0f;
        fadeImage.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
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
                    case ActionAfterAnimation.quit:
                        Application.Quit();
                        break;

                    case ActionAfterAnimation.start:
                        SceneManager.LoadScene(gameSceneName);
                        break;

                    case ActionAfterAnimation.credits:
                        SceneManager.LoadScene(creditsSceneName);
                        break;
                }
            }
        }
	}

    public void Quit()
    {
        actionAfter = ActionAfterAnimation.quit;
        currentFade = 0f;
        fadeImage.enabled = true;
    }

    public void StartGame()
    {
        actionAfter = ActionAfterAnimation.start;
        currentFade = 0f;
        fadeImage.enabled = true;
    }

    public void ShowCredits()
    {
        actionAfter = ActionAfterAnimation.credits;
        currentFade = 0f;
        fadeImage.enabled = true;
    }
}
