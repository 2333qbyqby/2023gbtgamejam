using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorSingle : MonoBehaviour
{
    public Image colorImage;
    public TextMeshProUGUI numText;

    public void UpdateVisual(Color color,int num)
    {
        colorImage.color = new Color(color.r,color.g,color.b,1);
        numText.text = $"*{num}";
    }
    
}
