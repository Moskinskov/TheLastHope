﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMenuController : MonoBehaviour
{
    public void Continue()
    {

    }

    public void NewGame()
    {
		Application.LoadLevel("Trit");
    }

    public void Setting()
    {

    }


    public void Exit()
    {
		Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
