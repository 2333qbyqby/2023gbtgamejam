using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public static ColorController instance { get; private set; }
    [Header("记录当前最终混合颜色")]
    public Color currentColor;
    public List<Color> colors;
    [Header("当前关卡最高颜色数量")]
    public int maxColorNum;
    
    public GameObject playerVisual;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        maxColorNum =LevelManager.instance.maxColorNum;//设置最大可混合颜色数量

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
            LevelUI.instance.SetRemainText(maxColorNum-colors.Count);//设置剩余吸收数
            //判断是否通关
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
        int count =colors.Count;//取得当前有多少颜色
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
