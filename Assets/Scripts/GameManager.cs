using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public SceneData SceneData
    {
        get => default;
        set
        {
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        SceneData = new SceneData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
