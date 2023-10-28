using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Audio;
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

    private void Start()
    {
        Debug.Log("BGM ON");
        AudioManager.instance.PlayBgm();
    }

    public void CheckifPassed()//检测成功
    {
        Color color = ColorController.instance.currentColor;
        float curR=color.r*255;
        float curG=color.g*255;
        float curB=color.b*255;
        float tarR=targetColor.r*255;
        float tarG=targetColor.g*255;
        float tarB=targetColor.b*255;
        if (Mathf.Abs(tarR-curR)<=1&& Mathf.Abs(tarG-curG)<=1&& Mathf.Abs(tarB-curB)<=1)
        {
            StartCoroutine(PassCoroutine());
        }
        else
        {
            CheckifFailed();
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
        AudioManager.instance.StopBgm();//BGM停止
        AudioManager.instance.PlaySound(3);//播放失败音效
        yield return new WaitForSeconds(2f);
        StartCoroutine(SceneLoader.Instance.LoadSceneAsync(curlevelIndex));
    }
    IEnumerator PassCoroutine()
    {
        LevelUI.instance.Pass();
        AudioManager.instance.StopBgm();//BGM停止
        AudioManager.instance.PlaySound(1);//播放通关音效
        //暂停
        yield return new WaitForSeconds(2f);
        StartCoroutine(SceneLoader.Instance.LoadSceneAsync(nextLevelIndex));
    }
}
