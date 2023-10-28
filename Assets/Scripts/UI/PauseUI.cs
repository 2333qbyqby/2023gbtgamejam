using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        [Tooltip("暂停界面")] public GameObject pauseUI;
        // Start is called before the first frame update
        void Start()
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
            
            GameInput.Instance.escEvent.AddListener(PauseOrRestart);
        }

        public void PauseOrRestart()
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            Time.timeScale = (pauseUI.activeSelf) ? 0f : 1f;
        }

        public void Restart()
        {
            PauseOrRestart();
            StartCoroutine(SceneLoader.Instance.LoadSceneAsync(LevelManager.instance.curlevelIndex));
        }

        public void BackToMenu()
        {
            PauseOrRestart();
            StartCoroutine(SceneLoader.Instance.LoadSceneAsync(0));
        }
    }
}
