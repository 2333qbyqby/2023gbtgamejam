using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartButtonEvent);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void StartButtonEvent()
    {
        StartCoroutine( SceneLoader.Instance.LoadSceneAsync(1));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
