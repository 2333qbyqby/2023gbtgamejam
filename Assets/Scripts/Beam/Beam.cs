using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public Color currentBeamColor;
    private Material material;
    private void Start()
    {
        material = GetComponent<Renderer>().material;
        material.color = currentBeamColor;
    }
}
