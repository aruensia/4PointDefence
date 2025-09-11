using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSetting : MonoBehaviour
{
    List<GameObject> currentMonster;
    int currentMonsterSpwanCount;
    StageData currentStageData;

    void Start()
    {
        
    }

    void InitStageData()
    {
        //currentStageData = (StageData)Manager.Instance.dataloader.tableDatas["StageTable"][Manager.Instance.inGameManager.currentStageLevel];
    }

    void SetStageLevel()
    {
        currentMonsterSpwanCount = currentStageData.Monster_Count1;
    }

}
