using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIMGR : MonoBehaviour
{
    public Button startButton;
    public Button optionButton;
    public Button exitButton;


    private void Start()
    {
        startButton?.onClick.AddListener(OnStartButtonClick);
        optionButton?.onClick.AddListener(()=> OnButtonClick(optionButton.name));
        exitButton?.onClick.AddListener(() => OnButtonClick(exitButton.name));
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnTitleButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    void OnButtonClick(string buttonName)
    {
        Debug.Log(buttonName);
    }
}
