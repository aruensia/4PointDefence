using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject EnhancePanel;

    public List<TextMeshProUGUI> enhanceInfoList;

    public float MainVolumValue;
    public float BGMVolumValue;
    public float SFXVolumValue;

    public void SaveOptionData()
    {
        PlayerPrefs.SetFloat("volumNum", MainVolumValue);
        PlayerPrefs.SetFloat("volumNum", BGMVolumValue);
        PlayerPrefs.SetFloat("volumNum", SFXVolumValue);
    }

    public void GoInGameScene()
    {
        SceneManager.LoadScene("InGame");
    }

    public void OptionPanelOn()
    {
        OptionPanel.SetActive(true);
    }

    public void OptionPanelOff()
    {
        OptionPanel.SetActive(false);
    }

    public void EnhancePanelOn()
    {
        EnhancePanel.SetActive(true);
    }

    public void EnhancePanelOff()
    {
        EnhancePanel.SetActive(false);
    }
}
