using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [Header("����UI")]
    public TextMeshProUGUI R;
    public TextMeshProUGUI G;
    public TextMeshProUGUI B;
    [Header("Ŀ��Ԥ��ͼ")]
    public Image colorImage;
    public TextMeshProUGUI colorText;
    [Header("����ʧ��UI")]
    public TextMeshProUGUI passText;
    [Header("ʣ���ϴ���")]
    public TextMeshProUGUI blendText;
    [Header("�洢��ǰ��ɫ��Ϣ")]
    public Transform colorContainer;
    [Header("��ɫ��ʾ����Ԥ����")]
    public GameObject colorNumItem;
    public static LevelUI instance { get; private set; }
    public Dictionary<Color, int> keyValuePairs; 
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
    public void SetCurRGB()//���õ�ǰ��ɫ
    {
        R.text = "R:"+(ColorController.instance.currentColor.r * 255).ToString("000.0");
        G.text = "G:"+(ColorController.instance.currentColor.g * 255).ToString("000.0");
        B.text = "B:"+(ColorController.instance.currentColor.b * 255).ToString("000.0");
    }

    public void SetTargetUI()
    {
        Color color = LevelManager.instance.targetColor;
        colorImage.color = color;

        colorText.text = $"R:{color.r * 255}    G:{color.g * 255}    B:{color.b * 255}";
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


    public void UpdateAllVisual()
    {
        List<Color> colorList = ColorController.instance.colors;//��ȡ��ҵ�ǰ���ϻ����ɫ�б�
        Dictionary<Color, int> keyValuePairs=new Dictionary<Color, int>(); ;
        for (int i = 0; i < colorContainer.childCount; i++)
        {
            Destroy(colorContainer.GetChild(i).gameObject);
        }
        for(int i =0; i<ColorController.instance.colors.Count;i++)
        {
            if (!keyValuePairs.ContainsKey(colorList[i]))
            {
                keyValuePairs.Add(colorList[i], 1);
            }
            else
            {
                keyValuePairs[colorList[i]]++;
            }
            Debug.Log(colorList[i]+""+keyValuePairs[colorList[i]]);
        }
        foreach (var item in keyValuePairs)
        {
            ColorSingle colorSingle = Instantiate(colorNumItem, colorContainer).GetComponent<ColorSingle>();
            colorSingle.UpdateVisual(item.Key, item.Value);
        }
        
    }
}
