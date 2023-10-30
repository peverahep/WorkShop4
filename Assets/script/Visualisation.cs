using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualisation : MonoBehaviour
{
    Color one = new Color(1f, 1f, 1f);
    Color zero = new Color(0f, 0f, 0f);
    [SerializeField]
    Material material;
    [SerializeField]
    Perceptron[] perceptron;
    [SerializeField]
    private UI UIInfo;

    public void VisualizeOutput(double a, double b)
    {
        material.color = perceptron[UIInfo.Mode].CalcOutput(a, b) == 1?one:zero;
    }
    void Start()
    {
        material.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
