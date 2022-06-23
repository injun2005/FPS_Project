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

    [SerializeField] private GameObject ObtionPanel;

    private void Start()
    {
        startButton?.onClick.AddListener(OnStartButtonClick);
        optionButton?.onClick.AddListener(OnObtionButtonClick);
        exitButton?.onClick.AddListener(OnExitButton);
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
    public void OnObtionButtonClick()
    {
        Time.timeScale = 0f;
        ObtionPanel.SetActive(true);
    }

    public void OnOffPanelButton()
    {
        ObtionPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
