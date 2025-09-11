using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LayerType
{
    ForceUnit = 6, EnemyUnit = 7 , Building = 8, BackGround = 9, SetPos = 10
}

public class Manager : MonoBehaviour
{
    static Manager instance;
    public Dictionary<string, List<SubActionData.TableData>> usebleData;

    public static Manager Instance
    {
        get { return instance; }
    }

    public PlayerInfo player = new PlayerInfo();
    public DataLoader dataloader =  new DataLoader();
    public InGameManager inGameManager;
    public GetUIDataInfo inGameUi;

    void Awake()
    {
        if( instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if ( instance != null)
        {
            Destroy(gameObject);
        }

        dataloader.DataLoad();
    }

    private void Start()
    {
        SetUsebleData();
    }

    void SetUsebleData()
    {
        usebleData = dataloader.tableDatas;
    }

    void InitPrefsData()
    {

    }
}
