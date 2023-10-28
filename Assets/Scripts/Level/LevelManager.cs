using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("关卡配置信息")]
    public int maxColorNum;
    public Color targetColor;//目标颜色
    [Header("关卡当前信息")]
    public int curlevelIndex;
    public int curColorNum;
    public int nextLevelIndex;//下一个要跳转的场景id
    public static LevelManager instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    public void CheckifPassed()//检测成功
    {
        if (ColorController.instance.currentColor == targetColor)
        {
            StartCoroutine(PassCoroutine());
        }
    }
    public void CheckifFailed()
    {
        if(curColorNum==maxColorNum)
        {
            StartCoroutine(FailCoroutine());
        }
    }
    IEnumerator FailCoroutine()
    {
        LevelUI.instance.Failed();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SceneLoader.Instance.LoadSceneAsync(curlevelIndex));
    }
    IEnumerator PassCoroutine()
    {
        LevelUI.instance.Pass();
        //暂停

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(SceneLoader.Instance.LoadSceneAsync(nextLevelIndex));
    }
}
