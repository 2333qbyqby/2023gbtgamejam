using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("�ؿ�������Ϣ")]
    public int maxColorNum;
    public Color targetColor;//Ŀ����ɫ
    [Header("�ؿ���ǰ��Ϣ")]
    public int curlevelIndex;
    public int curColorNum;
    public int nextLevelIndex;//��һ��Ҫ��ת�ĳ���id
    public static LevelManager instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    public void CheckifPassed()//���ɹ�
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
        //��ͣ

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(SceneLoader.Instance.LoadSceneAsync(nextLevelIndex));
    }
}
