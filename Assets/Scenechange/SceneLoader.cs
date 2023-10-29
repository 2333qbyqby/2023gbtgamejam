using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    public Animator animator;
    public Image image;
    private const string APPEAR = "Appear";
    private const string TARGETSCENE = "Scene2";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        
    }

    public IEnumerator LoadSceneAsync(int index)
    {
        animator.SetBool(APPEAR, true);//显示加载界面
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        asyncLoad.completed += AsyncLoad_completed;

        //进度条
        //asyncLoad.allowSceneActivation = false;

        //while(!asyncLoad.isDone)//当场景还未加载结束时
        //{
        //    float progress = Mathf.Clamp01(asyncLoad.progress/0.9f);//获取到真正的进度
        //    if(progress >=1)
        //    {
        //        animator.SetBool(APPEAR, false);
        //        yield return new WaitForSeconds(1f);
        //        asyncLoad.allowSceneActivation = true;
        //    }
        //    yield return null;
        //}
    }

    private void AsyncLoad_completed(AsyncOperation obj)
    {
        if(LevelManager.instance!=null)
        {
            Color color = LevelManager.instance.targetColor;
            image.color = new Color(color.r, color.g, color.b, 1);
        }
        animator.SetBool(APPEAR, false);
    }
}
