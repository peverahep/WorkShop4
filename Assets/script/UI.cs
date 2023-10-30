using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    private string[] modes = { "and", "or", "nand", "xor" };
    [SerializeField]
    private TextMeshProUGUI modeOut;
    [SerializeField]
    SceneManager manager;
    public int Mode { get; private set; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Mode = 0;
            modeOut.text = $"Current mode: {modes[Mode]}";
            manager.RestartInput();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Mode = 1;
            modeOut.text = $"Current mode: {modes[Mode]}";
            manager.RestartInput();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Mode = 2;
            modeOut.text = $"Current mode: {modes[Mode]}";
            manager.RestartInput();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Mode = 3;
            modeOut.text = $"Current mode: {modes[Mode]}";
            manager.RestartInput();
        }
    }
}
