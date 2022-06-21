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

    private UnityAction buttonAction;

    private void Start()
    {
        buttonAction = () => OnStartButtonClick();
        startButton.onClick.AddListener(buttonAction);

        optionButton.onClick.AddListener(()=> OnButtonClick(optionButton.name));

        exitButton.onClick.AddListener(() => OnButtonClick(exitButton.name));
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void OnButtonClick(string buttonName)
    {
        Debug.Log(buttonName);
    }
}
