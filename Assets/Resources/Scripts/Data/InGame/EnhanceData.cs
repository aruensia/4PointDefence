using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnhanceData : MonoBehaviour
{
    event Action EnhanceUI;

    public GameObject text_currentPlayerMoney;
    public GameObject text_currentGoldEnhance;
    public GameObject text_currentBaseHpEnhance;
    public GameObject text_currentPlayerunit1;
    public GameObject text_currentPlayerUnit2;
    public GameObject text_currentPlayerUnit3;
    public List<GameObject> text_Cost;
    public TextMeshProUGUI text_info;

    public TitleManager titleManager;

    //강화화면에 들어왔을 때, 현재 유저 강화 수치를 임시로 보관함.
    int originPlayerMoney;
    float originGoldEnhance;
    int originBaseHpEnhance;
    int originPlayerunit1UpgradeCount;
    int originPlayerunit2UpgradeCount;
    int originPlayerunit3UpgradeCount;

    //현재 유저의 강화 상태를 나타냄
    int currentPlayerMoney;
    float currentGoldEnhance;
    int currentBaseHpEnhance;
    int currentPlayerunit1;
    int currentPlayerunit2;
    int currentPlayerunit3;

    int cost = 10;

    private void OnEnable()
    {
        SetOriginData();
        SetCurrentData();
    }

    private void Start()
    {
        EnhanceUI += ShowUnitDataInfo;
        EnhanceUI += ShowBaseDataInfo;

        ShowBaseDataInfo();
    }

    void SetOriginData()
    {
        originPlayerMoney = Manager.Instance.player.playerMoney;
        originGoldEnhance = Manager.Instance.player.totalGoldEnhance;
        originBaseHpEnhance = Manager.Instance.player.BaseHpEnhance;
        originPlayerunit1UpgradeCount = Manager.Instance.player.Unit_1_Enhance;
        originPlayerunit2UpgradeCount = Manager.Instance.player.Unit_2_Enhance;
        originPlayerunit3UpgradeCount = Manager.Instance.player.Unit_3_Enhance;
    }

    void SetCurrentData()
    {
        currentPlayerMoney = Manager.Instance.player.playerMoney;
        currentGoldEnhance = Manager.Instance.player.totalGoldEnhance;
        currentBaseHpEnhance = Manager.Instance.player.BaseHpEnhance;
        currentPlayerunit1 = Manager.Instance.player.Unit_1_Enhance;
        currentPlayerunit2 = Manager.Instance.player.Unit_2_Enhance;
        currentPlayerunit3 = Manager.Instance.player.Unit_3_Enhance;

        Debug.Log("커런트에 값 넣었음.");
        Debug.Log("커런트 재화 돈 : " + currentPlayerMoney + " 유닛1 lv : " + currentPlayerunit1 + " 유닛2 lv : " + currentPlayerunit2 + " 유닛3 lv : " + currentPlayerunit3);
        Debug.Log("오리진 돈 : " + Manager.Instance.player.playerMoney );
    }

    public void ResetUpgradeValue()
    {
        currentPlayerMoney = originPlayerMoney;
        currentGoldEnhance = originGoldEnhance;
        currentBaseHpEnhance = originBaseHpEnhance;
        currentPlayerunit1 = originPlayerunit1UpgradeCount;
        currentPlayerunit2 = originPlayerunit2UpgradeCount;
        currentPlayerunit3 = originPlayerunit3UpgradeCount;
        //EnhanceUI.Invoke();

    }

    public void ShowBaseDataInfo()
    {
        text_currentPlayerMoney.GetComponent<TextMeshProUGUI>().text = "보유 재화 : " + currentPlayerMoney;
        text_currentGoldEnhance.GetComponent<TextMeshProUGUI>().text = "골드 획득 Lv : " + currentGoldEnhance;
        text_currentBaseHpEnhance.GetComponent<TextMeshProUGUI>().text = "본부 체력 Lv : " + currentBaseHpEnhance;
    }


    public void ShowUnitDataInfo()
    {
        text_currentPlayerMoney.GetComponent<TextMeshProUGUI>().text = "보유 재화 : " + currentPlayerMoney;
        text_currentPlayerunit1.GetComponent<TextMeshProUGUI>().text = "군인 Lv : " + currentPlayerunit1;
        text_currentPlayerUnit2.GetComponent<TextMeshProUGUI>().text = "강화 군인 Lv : " + currentPlayerunit2;
        text_currentPlayerUnit3.GetComponent<TextMeshProUGUI>().text = "정예 군인 Lv : " + currentPlayerunit3;
    }

    public void UpgradeBase(int initbutton)
    {
        switch(initbutton)
        {
            case 1:
                if (currentPlayerMoney >= (currentGoldEnhance * cost))
                {
                    currentGoldEnhance = currentGoldEnhance + 0.5f;
                    currentPlayerMoney = currentPlayerMoney - (int)(currentGoldEnhance * cost);
                    EnhanceUI.Invoke();
                }
                else
                {
                    text_info.text = "보유한 돈이 모자랍니다.";
                }
                break;

            case 2:
                if (currentPlayerMoney >= (currentBaseHpEnhance * cost))
                {
                    currentBaseHpEnhance++;
                    currentPlayerMoney = currentPlayerMoney - (currentBaseHpEnhance * cost);
                    EnhanceUI.Invoke();
                }
                else
                {
                    text_info.text = "보유한 돈이 모자랍니다.";
                }
                break;
        }
    }


    public void UpgradeUserUnit(int initbutton)
    {
        switch(initbutton)
        {
            case 1:
                if (currentPlayerMoney >= (currentPlayerunit1 * cost))
                {
                    currentPlayerunit1++;
                    currentPlayerMoney = currentPlayerMoney - (currentPlayerunit1 * cost);
                    EnhanceUI.Invoke();
                }
                else
                {
                    text_info.text = "보유한 돈이 모자랍니다.";
                }
                break;

            case 2:
                if (currentPlayerMoney >= (currentPlayerunit2 * cost))
                {
                    currentPlayerunit2++;
                    currentPlayerMoney = currentPlayerMoney - (currentPlayerunit2 * cost);
                    EnhanceUI.Invoke();
                }
                else
                {
                    text_info.text = "보유한 돈이 모자랍니다.";
                }
                break;

            case 3:
                if (currentPlayerMoney >= (currentPlayerunit3 * cost))
                {
                    currentPlayerunit3++;
                    currentPlayerMoney = currentPlayerMoney - (currentPlayerunit3 * cost);
                    EnhanceUI.Invoke();
                }
                else
                {
                    text_info.text = "보유한 돈이 모자랍니다.";
                }
                break;
        }
    }

    public void SaveUpGrade()
    {
        Manager.Instance.player.playerMoney = currentPlayerMoney;
        Manager.Instance.player.totalGoldEnhance = currentGoldEnhance;
        Manager.Instance.player.BaseHpEnhance = currentBaseHpEnhance;
        Manager.Instance.player.Unit_1_Enhance = currentPlayerunit1;
        Manager.Instance.player.Unit_2_Enhance = currentPlayerunit2;
        Manager.Instance.player.Unit_3_Enhance = currentPlayerunit3;
        SetOriginData();
        titleManager.firebaseDbMgr.SaveToDb();
    }
}
