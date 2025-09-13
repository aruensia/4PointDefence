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
    public GameObject ExitBtn;

    public GameObject DbManager;

    public List<TextMeshProUGUI> enhanceInfoList;

    FirebaseDbMgr firebaseDbMgr;

    public float MainVolumValue;
    public float BGMVolumValue;
    public float SFXVolumValue;

    public static bool playerData = false;

    private void Start()
    {
        firebaseDbMgr = GameObject.Find("Manager").transform.GetChild(0).GetComponent<FirebaseDbMgr>();
    }


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
        StartCoroutine(WaitPlayerData());
    }

    IEnumerator WaitPlayerData()
    {
        firebaseDbMgr.LoadToDb();
        yield return new WaitUntil(() => playerData);
        EnhancePanel.SetActive(true);
        playerData = false;
    }

    public void EnhancePanelOff()
    {
        EnhancePanel.SetActive(false);
    }

    public void LoginSuccess()
    {
        Debug.Log("·Î±×ÀÎ ");
        LoginPanel.SetActive(false);
    }

    public void LoginButtonOn()
    {
        startBtn.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void SetOnRegisterUI()
    {
        nickField.gameObject.SetActive(true);
        ExitBtn.gameObject.SetActive(true);
        RegisterBtn.gameObject.SetActive(true);
        loginBtn.gameObject.SetActive(false);
        SetRegisterBtn.gameObject.SetActive(false);
    }

    public void ExitButton()
    {
        nickField.gameObject.SetActive(false);
        ExitBtn.gameObject.SetActive(false);
        RegisterBtn.gameObject.SetActive(false);
        loginBtn.gameObject.SetActive(true);
        SetRegisterBtn.gameObject.SetActive(true);
    }
}
