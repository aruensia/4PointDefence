using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUnitSetting : MonoBehaviour
{
    private void Awake()
    {
        Manager.Instance.inGameManager.unitSetting = this;
    }

    public void SetupStartBuiding()
    {
        var tempbuild = Instantiate(Manager.Instance.inGameManager.StructbBuildingPrefab[0].transform.GetChild(0).GetComponent<StructUnit>(), transform);
        StructUnitData setbuild = (StructUnitData)Manager.Instance.dataloader.tableDatas["StructUnitTable"][0];

        tempbuild.unitData.Name = setbuild.Name;
        tempbuild.unitData.Hp = setbuild.Hp;
        tempbuild.unitData.Armor = setbuild.Armor;
        tempbuild.unitData.Damage = setbuild.Damage;
        tempbuild.unitData.AttackRange = setbuild.AttackRange;
        tempbuild.unitData.MoveSpeed = setbuild.MoveSpeed;
        Manager.Instance.inGameManager.baseCamp = tempbuild.gameObject;

    }
}
