using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] inputCubes;
    [SerializeField]
    private Material[] outMaterials;
    void Start()
    {
        
    }

    public void RestartInput()
    {
        foreach (var cube in inputCubes)
        {
            cube.transform.position = new Vector3(cube.transform.position.x, 3, cube.transform.position.z);
        }

        foreach (var material in outMaterials)
        {
            material.color = Color.gray;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
