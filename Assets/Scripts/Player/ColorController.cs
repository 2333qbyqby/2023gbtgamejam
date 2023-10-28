using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public static ColorController instance { get; private set; }
    [Header("��¼��ǰ���ջ����ɫ")]
    public Color currentColor;
    public List<Color> colors;
    [Header("��ǰ�ؿ������ɫ����")]
    public int maxColorNum;
    
    public GameObject playerVisual;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        maxColorNum =LevelManager.instance.maxColorNum;//�������ɻ����ɫ����

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            Color addedColor = other.gameObject.GetComponent<Renderer>().material.color;
            colors.Add(addedColor);
            LevelManager.instance.curColorNum++;
            //Debug.Log(colors.Count);
            currentColor=BlendColor(currentColor,addedColor);
            playerVisual.GetComponent<Renderer>().material.color=currentColor;
            LevelUI.instance.SetCurRGB();
            LevelUI.instance.SetRemainText(maxColorNum-colors.Count);//����ʣ��������
            //�ж��Ƿ�ͨ��
            LevelManager.instance.CheckifPassed();
            LevelManager.instance.CheckifFailed();
        }
    }

    public Color BlendColor(Color currentColor,Color addedColor)
    {
        if (colors.Count == 1)
        {
            return addedColor;
        }
        int count =colors.Count;//ȡ�õ�ǰ�ж�����ɫ
        float cweight = 1f - 1f / count;
        float aweight=1f/ count;
        float newR =currentColor.r * cweight + addedColor.r * aweight;
        float newG =currentColor.g * cweight + addedColor.g * aweight;
        float newB =currentColor.b * cweight + addedColor.b * aweight;
        Color blendedColor=new Color(newR,newG,newB);
        //Debug.Log(cweight);
        //Debug.Log(aweight);
        return blendedColor;
    }

}
