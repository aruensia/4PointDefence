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

    public GameObject UserEnhanceItemList;
    public GameObject UnitEnhanceItemList;

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
    public GameObject EnhanceManager;

    public List<TextMeshProUGUI> enhanceInfoList;

    public FirebaseDbMgr firebaseDbMgr;

    public float MainVolumValue;
    public float BGMVolumValue;
    public float SFXVolumValue;

    public static bool playerData = false;
    public bool DbMangerOn = false;

    private void OnEnable()
    {
        if(Manager.Instance.IsGameOn == true)
        {
            LoginPanel.SetActive(false);
            firebaseDbMgr = GameObject.Find("Manager").transform.GetChild(0).GetComponent<FirebaseDbMgr>();
            StartCoroutine(WaitPlayerData());
        }
    }

    private void Start()
    {
        firebaseDbMgr = GameObject.Find("Manager").transform.GetChild(0).GetComponent<FirebaseDbMgr>();
    }

    public void TestInitMoney()
    {
        Manager.Instance.player.playerMoney = 10000;
        firebaseDbMgr.SaveToDb();
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
        EnhancePanel.SetActive(true);
    }

    IEnumerator WaitPlayerData()
    {
        firebaseDbMgr.LoadToDb();
        yield return new WaitUntil(() => playerData);
        Debug.Log("Firebase에서 유저 id를 비교하여 유저 계정 정보를 불러왔음");
        playerData = false;
    }

    public void EnhancePanelOff()
    {
        EnhancePanel.GetComponent<EnhanceData>().ResetUpgradeValue();
        EnhancePanel.SetActive(false);
    }

    public void UserEnhaceItemOn()
    {
        UserEnhanceItemList.gameObject.SetActive(true);
        UnitEnhanceItemList.gameObject.SetActive(false);

        EnhanceManager.GetComponent<EnhanceData>().ShowBaseDataInfo();
    }

    public void UnitEnhaceItemOn()
    {
        UserEnhanceItemList.gameObject.SetActive(false);
        UnitEnhanceItemList.gameObject.SetActive(true);

        EnhanceManager.GetComponent<EnhanceData>().ShowUnitDataInfo();
    }

    public void LoginSuccess()
    {
        Debug.Log("로그인 ");
        StartCoroutine(WaitPlayerData());
        Manager.Instance.IsGameOn = true;
        LoginPanel.SetActive(false);
    }

    public void LoginButtonOn()
    {
        startBtn.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void UserRegister()
    {
        FirebaseAuthMgr tempAuth = GameObject.Find("AuthManager").gameObject.GetComponent<FirebaseAuthMgr>();
        tempAuth.Register();

        StartCoroutine(WaitDbMgr());
    }

    IEnumerator WaitDbMgr()
    {
        yield return new WaitUntil(() => DbMangerOn && firebaseDbMgr.DbDataLoadOn);
        firebaseDbMgr.SaveToDb();
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
