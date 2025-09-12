using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject EnhancePanel;
    public GameObject LoginPanel;

    public InputField emailField; 
    public InputField pwField; 
    public InputField nickField; 

    public TextMeshProUGUI warningText;
    public TextMeshProUGUI confirmText;

    public Button startBtn;
    public GameObject loginBtn;
    public GameObject SetRegisterBtn;
    public GameObject RegisterBtn;
    public GameObject DbManager;

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

    public void LoginSuccess()
    {
        LoginPanel.SetActive(false);
    }

    public void LoginButtonOn()
    {
        startBtn.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void SetOnRegisterUI()
    {

    }
}
