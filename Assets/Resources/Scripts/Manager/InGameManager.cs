using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    RaycastHit mouseClick;
    public LayerMask rayMask;

    public PlayerInfo playerData;
    public DefaultUnitSetting unitSetting;
    Manager manager;

    event Action OnSelectUnitTraining;
    public Action EnemyActionData;
    public Action EnemyGroupActionData;
    public Action ForceUnitdie;
    public event Action DataChange;
    public event Action NextLevel;
    public event Action OnGameEnd;

    bool isGameOver = false;

    [Header("[생성하는 몬스터 프리팹]")]
    public GameObject monsterPrefab;
    public List<GameObject> StructbBuildingPrefab;

    [Header("[스폰 포지션]")]
    public List<GameObject> monsterSpwanPos;
    public GameObject unitFiled;
    public GameObject battleField;
    public List<GameObject> userUnit;
    public GameObject selectedUnit;
    public List<GameObject> unitInstantiatePos;

    public GameObject userLifeObject;
    public GameObject forceUnitPrefab;

    public GameObject baseCamp;
    public GameObject gameOverPopup;

    public LayerType isPlayerSelect;
    public bool isUnitSelect = false;
    public bool waitNextSetage = true;
    public int currentUnitNum;
    public int nextWaitStageTimer = 15;

    float tempradius = 20f;
    int currentTrainingLevel;
    int currentStageLevel = 0;
    public bool waitTime = false;
    public StageData currentStageData;
    public int enemycount;
    public int totalkillCount;

    public int getMoney;

    private void Awake()
    {
        Manager.Instance.inGameManager = this;
        playerData = Manager.Instance.player;
    }

    private void Start()
    {
        currentStageData = (StageData)Manager.Instance.dataloader.tableDatas["StageTable"][Manager.Instance.inGameManager.currentStageLevel];
        OnSelectUnitTraining += UnitTrainingBoxCheck;
        OnGameEnd += GameOverPopupOn;

        manager = GameObject.Find("Manager").GetComponent<Manager>();
        userLifeObject = GameObject.Find("SetUnitPos");

        unitSetting.SetupStartBuiding();
        StartGetGold();
        currentTrainingLevel = 3;
        StartCoroutine(StageCheck());
        StartCoroutine(GameStartSetup());
    }

    IEnumerator GameStartSetup()
    {
        yield return new WaitForSeconds(5);
        NextStageCall();
        DataChangeCall();
    }

    IEnumerator StageCheck()
    {
        while(true)
        {
            if (isGameOver == true)
            {
                InitMoneyToPlayer();
                break;
            }

            if(waitNextSetage == true)
            {
                StartCoroutine(WaitNextStage());
                waitNextSetage = false;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator WaitNextStage()
    {
        waitTime = true;
        while (true)
        {
            if (nextWaitStageTimer <= 0)
            {

                NextStageCall();
                waitTime = false;
                waitNextSetage = false;
                break;
            }

            nextWaitStageTimer -= 1;
            DataChange.Invoke();
             
            yield return new WaitForSeconds(1);
        }
    }

    void InitMoneyToPlayer()
    {
        playerData.playerMoney = getMoney;
    }

    public void CallDataChange()
    {
        DataChange.Invoke();
    }

    public void NextStageCall()
    {
        Manager.Instance.inGameUi.GameTime.gameObject.SetActive(false);
        currentStageLevel++;
        currentStageData = (StageData)Manager.Instance.dataloader.tableDatas["StageTable"][Manager.Instance.inGameManager.currentStageLevel];
        nextWaitStageTimer = 15;
        NextLevel.Invoke();
    }

    public void StartGetGold()
    {
        var tempgold = playerData._defaultGold + playerData.totalGoldEnhance;

        Debug.Log("시작 시 기본 재화 + 강화값 " + tempgold);

        StartCoroutine(UpdateGetGold(tempgold));
    }

    public IEnumerator UpdateGetGold(int getgoldvalue)
    {
        while (true)
        {
            if (isGameOver == true)
            {
                break;
            }

            yield return new WaitForSeconds(1);

            playerData._defaultGold += getgoldvalue;
            DataChange.Invoke();
        }
    }

    void UnitTrainingBoxCheck()
    {
        if(isUnitSelect == true)
        {
            switch(currentTrainingLevel)
            {
                case 1:
                    for (int i = 0; i < currentTrainingLevel; i++)
                    {
                        unitInstantiatePos[i].gameObject.SetActive(true);
                    }
                    break;

                case 2:
                    for (int i = 0; i < currentTrainingLevel; i++)
                    {
                        unitInstantiatePos[i].gameObject.SetActive(true);
                    }
                    break;

                case 3:
                    for (int i = 0; i < currentTrainingLevel; i++)
                    {
                        unitInstantiatePos[i].gameObject.SetActive(true);
                    }
                    break;
            }
        }
        else if (isUnitSelect == false)
        {
            switch (currentTrainingLevel)
            {
                case 1:
                    for (int i = 0; i < currentTrainingLevel; i++)
                    {
                        unitInstantiatePos[i].gameObject.SetActive(false);
                    }
                    break;

                case 2:
                    for (int i = 0; i < currentTrainingLevel; i++)
                    {
                        unitInstantiatePos[i].gameObject.SetActive(false);
                    }
                    break;

                case 3:
                    for (int i = 0; i < currentTrainingLevel; i++)
                    {
                        unitInstantiatePos[i].gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
    public void UnitInstantiate(RaycastHit _mouseClick)
    {
        Transform setpos = _mouseClick.transform.gameObject.transform.parent.GetChild(1).gameObject.transform;
        setpos.transform.localPosition = _mouseClick.transform.gameObject.transform.localPosition;
        var tempunit = Instantiate(Manager.Instance.inGameManager.selectedUnit, setpos);
        ForceUnitData tempdata = (ForceUnitData)Manager.Instance.dataloader.tableDatas["ForceUnitTable"][Manager.Instance.inGameManager.currentUnitNum];
        tempunit.transform.SetParent(Manager.Instance.inGameManager.battleField.transform);
        tempunit.GetComponent<ForceUnit>().forceUnitData.Name = tempdata.Name;
        tempunit.GetComponent<ForceUnit>().forceUnitData.Hp = tempdata.Hp;
        tempunit.GetComponent<ForceUnit>().forceUnitData.Armor = tempdata.Armor;
        tempunit.GetComponent<ForceUnit>().forceUnitData.Damage = tempdata.Damage;
        tempunit.GetComponent<ForceUnit>().forceUnitData.AttackRange = tempdata.AttackRange;
        tempunit.GetComponent<ForceUnit>().forceUnitData.MoveSpeed = tempdata.MoveSpeed;
        tempunit.GetComponent<ForceUnit>().forceUnitData.Cost = tempdata.Cost;
    }

    public void OnGameOver()
    {
        isGameOver = true;
        Manager.Instance.player = playerData;
        OnGameEnd.Invoke();
    }

    public void DataChangeCall()
    {
        DataChange.Invoke();
    }

    public void UnitSelcet(int unitselect)
    {
        selectedUnit = userUnit[unitselect].gameObject;
        OnSelectUnitTraining.Invoke();
    }    

    public void Offselecet()
    {
        OnSelectUnitTraining.Invoke();
    }

    public void GameOverPopupOn()
    {
        gameOverPopup.gameObject.SetActive(true);
    }

    public void GameOverPopupOff()
    {
        SceneManager.LoadScene("Title");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, tempradius);
    }
}
