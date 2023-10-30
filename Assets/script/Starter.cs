using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    BitMeaning selfMeaning;
    [SerializeField]
    Visualisation output;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor") return;
        var meaning = collision.gameObject.GetComponent<BitMeaning>();
        output.VisualizeOutput(selfMeaning.Number, meaning.Number);
    }
    void Start()
    {
        selfMeaning = gameObject.GetComponent<BitMeaning>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
