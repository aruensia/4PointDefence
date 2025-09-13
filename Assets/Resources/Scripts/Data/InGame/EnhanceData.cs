using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnhanceData : MonoBehaviour
{
    public GameObject text_currentPlayerMoney;
    public GameObject text_currentPlayerunit1;
    public GameObject text_currentPlayerUnit2;
    public GameObject text_currentPlayerUnit3;

    //강화화면에 들어왔을 때, 현재 유저 강화 수치를 임시로 보관함.
    int originPlayerMoney;
    int originPlayerunit1UpgradeCount;
    int originPlayerunit2UpgradeCount;
    int originPlayerunit3UpgradeCount;

    //유저가 강화를 누를 경우 해당 수치를 임시로 보관함.
    int upgradePendingPlayerMoney;
    int upgradePendingPlayerUnit1Count;
    int upgradePendingPlayerUnit2Count;
    int upgradePendingPlayerUnit3Count;

    //현재 유저의 강화 상태를 나타냄
    int currentPlayerMoney;
    int currentPlayerunit1;
    int currentPlayerunit2;
    int currentPlayerunit3;

    private void Start()
    {
        SetOriginData();
        SetCurrentData();
        ShowDataInfo();
    }

    void SetOriginData()
    {
        int originPlayerMoney = Manager.Instance.player.playerMoney;
        int originPlayerunit1UpgradeCount = Manager.Instance.player.Unit_1_Enhance;
        int originPlayerunit2UpgradeCount = Manager.Instance.player.Unit_2_Enhance;
        int originPlayerunit3UpgradeCount = Manager.Instance.player.Unit_3_Enhance;
    }

    void SetCurrentData()
    {
        int currentPlayerMoney = Manager.Instance.player.playerMoney;
        int currentPlayerunit1 = Manager.Instance.player.Unit_1_Enhance;
        int currentPlayerunit2 = Manager.Instance.player.Unit_2_Enhance;
        int currentPlayerunit3 = Manager.Instance.player.Unit_3_Enhance;
    }

    void ResetUpgradeValue()
    {
        int currentPlayerMoney = originPlayerMoney;
        int currentPlayerunit1 = originPlayerunit1UpgradeCount;
        int currentPlayerunit2 = originPlayerunit2UpgradeCount;
        int currentPlayerunit3 = originPlayerunit3UpgradeCount;
    }

    void ShowDataInfo()
    {
        text_currentPlayerMoney.GetComponent<TextMeshProUGUI>().text = "보유 재화 : " + currentPlayerMoney;
        text_currentPlayerunit1.GetComponent<TextMeshProUGUI>().text = "군인 Lv : " + currentPlayerunit1;
        text_currentPlayerUnit2.GetComponent<TextMeshProUGUI>().text = "강화 군인 Lv : " + currentPlayerunit2;
        text_currentPlayerUnit3.GetComponent<TextMeshProUGUI>().text = "정예 군인 Lv : " + currentPlayerunit3;
    }

    void UpgradeUserUnit1()
    {
        
    }
}
