using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [Header("文字UI")]
    public TextMeshProUGUI R;
    public TextMeshProUGUI G;
    public TextMeshProUGUI B;
    [Header("目标预览图")]
    public Image colorImage;
    public TextMeshProUGUI colorText;
    [Header("过关失败UI")]
    public TextMeshProUGUI passText;
    [Header("剩余混合次数")]
    public TextMeshProUGUI blendText;
    public static LevelUI instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SetTargetUI();
        passText.gameObject.SetActive(false);
        SetRemainText(LevelManager.instance.maxColorNum);
    }
    public void SetCurRGB()//设置当前颜色
    {
        R.text = "R:"+(ColorController.instance.currentColor.r * 255).ToString("000.0");
        G.text = "G:"+(ColorController.instance.currentColor.g * 255).ToString("000.0");
        B.text = "B:"+(ColorController.instance.currentColor.b * 255).ToString("000.0");
    }

    public void SetTargetUI()
    {
        Color color = LevelManager.instance.targetColor;
        colorImage.color = color;

        colorText.text = $"R:{color.r * 255}\tG:{color.g * 255}\tB:{color.b * 255}";
    }

    public void Pass()
    {
        passText.color = LevelManager.instance.targetColor;
        passText.text = "Absorbed!";
        passText.gameObject.SetActive(true);
    }

    public void Failed()
    {
        passText.color = ColorController.instance.currentColor;
        passText.text = "Unabsorbed!";
        passText.gameObject.SetActive(true);
    }

    public void SetRemainText(int num)
    {
        blendText.text = $"{num} absorb times remain";
    }
}
