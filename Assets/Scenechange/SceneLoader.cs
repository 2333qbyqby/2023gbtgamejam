using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    public Animator animator;
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
        animator.SetBool(APPEAR, true);//��ʾ���ؽ���
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        asyncLoad.completed += AsyncLoad_completed;

        //������
        //asyncLoad.allowSceneActivation = false;

        //while(!asyncLoad.isDone)//��������δ���ؽ���ʱ
        //{
        //    float progress = Mathf.Clamp01(asyncLoad.progress/0.9f);//��ȡ�������Ľ���
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
        animator.SetBool(APPEAR, false);
    }
}
