using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GetUIDataInfo : MonoBehaviour
{
    RaycastHit mouseClick;
    public LayerMask rayMask;
    public TextMeshProUGUI[] GameInfoText;
    public List<Button> buttonList;
    public List<Sprite> buttonIconList;
    public List<GameObject> trainingUnitList;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI currentEnemyCount;
    public TextMeshProUGUI GameTime;
    public TextMeshProUGUI currentGetMoney;
    public TextMeshProUGUI totalKillCount;

    int actionButtonCount = 3;
    private EnemyUnit tempEnemyData;
    private ForceUnit tempForceData;
    private StructUnit tempStructData;
    bool clickBackground = false;

    InputActionMap mainActionMap;
    InputAction qInput;
    InputAction wInput;
    InputAction eInput;
    InputAction rInput;
    InputAction tInput;

    void Awake()
    {
        Manager.Instance.inGameUi = this;
        GameInfoText[6].text = " ";

    }

    private void Start()
    {
        Manager.Instance.inGameManager.DataChange += PrintGold;
        Manager.Instance.inGameManager.DataChange += PrintStageText;
        Manager.Instance.inGameManager.DataChange += PrintCurrentEnemyCount;
        Manager.Instance.inGameManager.DataChange += PrintWaitNextStegeTime;
        Manager.Instance.inGameManager.OnGameEnd += SetupGameOverInfo;
        GameTime.gameObject.SetActive(false);
        InputButtonData();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetMouseInputData();
        }
    }

    IEnumerator WaitClickAction()
    {
        float tempcolor = 1;

        while (true)
        {
            Color color = new Color(1, 1, 1, tempcolor);

            infoText.GetComponent<TextMeshProUGUI>().color = color;

            tempcolor -=0.01f;

            if(tempcolor <= 0)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    void PrintWaitNextStegeTime()
    {
        if(Manager.Instance.inGameManager.waitTime == true)
        {
            GameTime.text = "다음 스테이지 : " + Manager.Instance.inGameManager.nextWaitStageTimer.ToString() + "초";

        }
    }

    void SetupGameOverInfo()
    {
        totalKillCount.text = "총 처치 적 : " + Manager.Instance.inGameManager.totalkillCount.ToString();
        currentGetMoney.text = "획득 재화 : " + Manager.Instance.inGameManager.getMoney.ToString();
    }

    void GameTimeOn()
    {
        GameTime.gameObject.SetActive (true);
    }

    void PrintCurrentEnemyCount()
    {
        currentEnemyCount.text = "남은 적 : " + Manager.Instance.inGameManager.enemycount;
    }

    void PrintStageText()
    {
        stageText.text = "Stage : " + (Manager.Instance.inGameManager.currentStageData.Index - 2000000).ToString();
    }

    void PrintGold()
    {
        GameInfoText[6].text = Manager.Instance.inGameManager.playerData._defaultGold.ToString();
    }

    void SetMouseInputData()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray clickPos = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(clickPos, out mouseClick, rayMask))
        {
            switch (mouseClick.transform.gameObject.layer)
            {
                case (int)LayerType.EnemyUnit:
                    OffButtonListSetting();
                    tempEnemyData = mouseClick.transform.gameObject.GetComponent<EnemyUnit>();
                    GameInfoText[0].text = "이름 : " + tempEnemyData.enemyUnitData.Name;
                    GameInfoText[1].text = "체력 : " + tempEnemyData.enemyUnitData.Hp;
                    GameInfoText[2].text = "방어력 : " + tempEnemyData.enemyUnitData.Armor;
                    GameInfoText[3].text = "데미지 : " + tempEnemyData.enemyUnitData.Damage;
                    GameInfoText[4].text = "공격 거리 : " + tempEnemyData.enemyUnitData.AttackRange;
                    GameInfoText[5].text = "이동 속도 : " + tempEnemyData.enemyUnitData.MoveSpeed;
                    Manager.Instance.inGameManager.isPlayerSelect = LayerType.EnemyUnit;
                    Manager.Instance.inGameManager.isUnitSelect = false;
                    Manager.Instance.inGameManager.Offselecet();
                    break;

                case (int)LayerType.ForceUnit:
                    OffButtonListSetting();
                    tempForceData = mouseClick.transform.gameObject.GetComponent<ForceUnit>();
                    GameInfoText[0].text = "이름 : " + tempForceData.forceUnitData.Name;
                    GameInfoText[1].text = "체력 : " + tempForceData.forceUnitData.Hp;
                    GameInfoText[2].text = "방어력 : " + tempForceData.forceUnitData.Armor;
                    GameInfoText[3].text = "데미지 : " + tempForceData.forceUnitData.Damage;
                    GameInfoText[4].text = "공격 거리 : " + tempForceData.forceUnitData.AttackRange;
                    GameInfoText[5].text = "이동 속도 : " + tempForceData.forceUnitData.MoveSpeed;

                    tempForceData.forceUnitData.UseActionButtonCount = actionButtonCount;
                    var tempforcesubactioncount = tempForceData.forceUnitData.UseActionButtonCount + tempForceData.forceUnitData.UseSkillCount;
                    OnButtonListSetting(tempforcesubactioncount);
                    OffCostButton(tempforcesubactioncount);
                    Manager.Instance.inGameManager.isPlayerSelect = LayerType.ForceUnit;
                    Manager.Instance.inGameManager.isUnitSelect = false;
                    Manager.Instance.inGameManager.Offselecet();
                    break;

                case (int)LayerType.Building:
                    OffButtonListSetting();
                    tempStructData = mouseClick.transform.gameObject.GetComponent<StructUnit>();
                    GameInfoText[0].text = "이름 : " + tempStructData.unitData.Name;
                    GameInfoText[1].text = "체력 : " + tempStructData.unitData.Hp;
                    GameInfoText[2].text = "방어력 : " + tempStructData.unitData.Armor;
                    GameInfoText[3].text = "데미지 : " + tempStructData.unitData.Damage;
                    GameInfoText[4].text = "공격 거리 : " + tempStructData.unitData.AttackRange;
                    GameInfoText[5].text = "이동 속도 : " + tempStructData.unitData.MoveSpeed;
                    OnButtonListSetting(Manager.Instance.usebleData["ForceUnitTable"].Count);
                    InitButtonData(Manager.Instance.usebleData["ForceUnitTable"].Count);
                    Manager.Instance.inGameManager.isPlayerSelect = LayerType.Building;
                    Manager.Instance.inGameManager.isUnitSelect = false;
                    Manager.Instance.inGameManager.Offselecet();
                    break;

                case (int)LayerType.BackGround:
                    OffButtonListSetting();
                    Manager.Instance.inGameManager.isUnitSelect = false;
                    Manager.Instance.inGameManager.isPlayerSelect = LayerType.BackGround;
                    Manager.Instance.inGameManager.Offselecet();

                    GameInfoText[0].text = "이름 : ";
                    GameInfoText[1].text = "체력 : ";
                    GameInfoText[2].text = "방어력 : ";
                    GameInfoText[3].text = "데미지 : ";
                    GameInfoText[4].text = "공격 거리 : ";
                    GameInfoText[5].text = "이동 속도 : ";
                    break;

                case (int)LayerType.SetPos:
                    if(Manager.Instance.inGameManager.isUnitSelect == true)
                    {
                        if (Manager.Instance.inGameManager.playerData._defaultGold >= Manager.Instance.inGameManager.selectedUnit.GetComponent<ForceUnit>().forceUnitData.Cost)
                        {
                            Debug.Log(mouseClick.transform.gameObject.name);

                            if(mouseClick.transform.gameObject.GetComponent<PosData>().isSetUnit == false )
                            {
                                Manager.Instance.inGameManager.playerData._defaultGold = Manager.Instance.inGameManager.playerData._defaultGold - Manager.Instance.inGameManager.selectedUnit.GetComponent<ForceUnit>().forceUnitData.Cost;
                                Manager.Instance.inGameManager.UnitInstantiate(mouseClick);
                                mouseClick.transform.gameObject.GetComponent<PosData>().isSetUnit = true;
                                Manager.Instance.inGameManager.DataChangeCall();
                                Manager.Instance.inGameManager.Offselecet();
                            }
                            else
                            {
                                StartCoroutine(WaitClickAction());
                                infoText.text = "설치할 수 없습니다";
                            }

                        }
                        else
                        {
                            StartCoroutine(WaitClickAction());
                            infoText.text = "돈이 모자랍니다";
                        }
                    }
                    break;
            }
        }
    }

    public void SwitchButton(int i)
    {
        switch (Manager.Instance.inGameManager.isPlayerSelect)
        {
            case LayerType.EnemyUnit:
                break;

            case LayerType.ForceUnit:
                break;

            case LayerType.Building:
                if (buttonList[i].gameObject.activeSelf)
                {
                    Manager.Instance.inGameManager.isUnitSelect = true;
                    Manager.Instance.inGameManager.UnitSelcet(i);
                    Manager.Instance.inGameManager.currentUnitNum = i;
                }

                break;

            case LayerType.BackGround:
                break;
        }
    }

    void InputButtonData()
    {
        mainActionMap = GameObject.Find("Manager").GetComponent<PlayerInput>().actions.FindActionMap("UserInput");
        qInput = mainActionMap.FindAction("Qselect");
        wInput = mainActionMap.FindAction("Wselect");
        eInput = mainActionMap.FindAction("Eselect");
        rInput = mainActionMap.FindAction("Rselect");
        tInput = mainActionMap.FindAction("Tselect");

        qInput.performed += ctx =>
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                switch (Manager.Instance.inGameManager.isPlayerSelect)
                {
                    case LayerType.EnemyUnit:
                        break;

                    case LayerType.ForceUnit:
                        break;

                    case LayerType.Building:
                        if(buttonList[0].gameObject.activeSelf)
                        {
                            Manager.Instance.inGameManager.isUnitSelect = true;
                            Manager.Instance.inGameManager.UnitSelcet(0);
                            Manager.Instance.inGameManager.currentUnitNum = 0;
                        }

                        break;

                    case LayerType.BackGround:
                        break;
                }
            }
        };
        wInput.performed += ctx =>
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                switch (Manager.Instance.inGameManager.isPlayerSelect)
                {
                    case LayerType.EnemyUnit:
                        break;

                    case LayerType.ForceUnit:
                        break;

                    case LayerType.Building:
                        if (buttonList[1].gameObject.activeSelf)
                        {
                            Manager.Instance.inGameManager.isUnitSelect = true;
                            Manager.Instance.inGameManager.UnitSelcet(1);
                            Manager.Instance.inGameManager.currentUnitNum = 1;
                        }
                        break;

                    case LayerType.BackGround:
                        break;
                }
            }
        };
        eInput.performed += ctx =>
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                switch (Manager.Instance.inGameManager.isPlayerSelect)
                {
                    case LayerType.EnemyUnit:
                        break;

                    case LayerType.ForceUnit:
                        break;

                    case LayerType.Building:
                        if (buttonList[2].gameObject.activeSelf)
                        {
                            Manager.Instance.inGameManager.isUnitSelect = true;
                            Manager.Instance.inGameManager.UnitSelcet(2);
                            Manager.Instance.inGameManager.currentUnitNum = 2;
                        }
                        break;

                    case LayerType.BackGround:
                        break;
                }
            }
        };
        rInput.performed += ctx =>
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                switch (Manager.Instance.inGameManager.isPlayerSelect)
                {
                    case LayerType.EnemyUnit:
                        break;

                    case LayerType.ForceUnit:
                        break;

                    case LayerType.Building:
                        if (buttonList[3].gameObject.activeSelf)
                        {
                            Manager.Instance.inGameManager.isUnitSelect = true;
                            Manager.Instance.inGameManager.UnitSelcet(3);
                            Manager.Instance.inGameManager.currentUnitNum = 3;
                        }
                        break;

                    case LayerType.BackGround:
                        break;
                }
            }
        };
        tInput.performed += ctx =>
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                switch (Manager.Instance.inGameManager.isPlayerSelect)
                {
                    case LayerType.EnemyUnit:
                        break;

                    case LayerType.ForceUnit:
                        break;

                    case LayerType.Building:
                        if (buttonList[4].gameObject.activeSelf)
                        {
                            Manager.Instance.inGameManager.isUnitSelect = true;
                            Manager.Instance.inGameManager.UnitSelcet(4);
                            Manager.Instance.inGameManager.currentUnitNum = 4;
                        }
                        break;

                    case LayerType.BackGround:
                        break;
                }
            }
        };
    }

    void OnButtonListSetting(int tempcount)
    {
        for(int i = 0; i < tempcount;  i++)
        {
            buttonList[i].transform.gameObject.SetActive(true);
        }
    }

    void OffButtonListSetting()
    {
        for (int i = 0; i < 14; i++)
        {
            buttonList[i].transform.gameObject.SetActive(false);
        }
    }

    void OffCostButton(int initcount)
    {
        for (int i = 0; i < initcount; i++)
        {
            buttonList[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void InitButtonData(int initcount)
    {
        for( int i = 0; i < initcount; i++)
        {
            var tempUnitData = Manager.Instance.dataloader.tableDatas["ForceUnitTable"][i];
            trainingUnitList[i].GetComponent<ForceUnit>().forceUnitData = (ForceUnitData)tempUnitData;
            buttonList[i].transform.GetChild(0).gameObject.SetActive(true);
            buttonList[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Manager.Instance.inGameManager.userUnit[i].gameObject.GetComponent<ForceUnit>().forceUnitData.Cost.ToString();
        }
    }
}
