using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPoolController : MonoBehaviour
{
    public static UnitPoolController unitPoolController;

    [SerializeField] private GameObject ForceUnitPrefab;
    public GameObject unitSettingPos;
    [SerializeField] private int settingUnitCount;

    Queue<ForceUnit> forceUnitPool = new Queue<ForceUnit>();

    private void Awake()
    {
        unitPoolController = this;
        InitForcePrefab(300);
    }

    private void Start()
    {
        StartForceUnitSetup();
    }


    private void InitForcePrefab(int initcount)
    {
        for(int i = 0; i < initcount; i++)
        {
            forceUnitPool.Enqueue(CreatForceUnit());
        }
    }


    void StartForceUnitSetup()
    {
        for (int i = 0; i < settingUnitCount; i++)
        {
            var tempforceUnit = GetForce();
            tempforceUnit.transform.position = new Vector3(unitSettingPos.transform.position.x + (i * 0.2f), unitSettingPos.transform.position.y, unitSettingPos.transform.position.z);
        }
    }

    private ForceUnit CreatForceUnit()
    {
        var newForceUnit = Instantiate(ForceUnitPrefab).GetComponent<ForceUnit>();
        newForceUnit.gameObject.SetActive(false);
        newForceUnit.transform.SetParent(transform);
        ForceUnitData tempForce = (ForceUnitData)Manager.Instance.dataloader.tableDatas["ForceUnitTable"][0];
        newForceUnit.forceUnitData.Index = tempForce.Index;
        newForceUnit.forceUnitData.unitType = tempForce.unitType;
        newForceUnit.forceUnitData.Name = tempForce.Name;
        newForceUnit.forceUnitData.Hp = tempForce.Hp;
        newForceUnit.forceUnitData.Armor = tempForce.Armor;
        newForceUnit.forceUnitData.Damage = tempForce.Damage;
        newForceUnit.forceUnitData.AttackRange = tempForce.AttackRange;
        newForceUnit.forceUnitData.MoveSpeed = tempForce.MoveSpeed;
        newForceUnit.forceUnitData.unitSize = tempForce.unitSize;
        newForceUnit.forceUnitData.unitState = tempForce.unitState;
        newForceUnit.forceUnitData.UseSkillCount = tempForce.UseSkillCount;
        return newForceUnit;
    }

    public ForceUnit GetForce()
    {
        if (forceUnitPool.Count > 0)
        {
            var forceUnit = forceUnitPool.Dequeue();

            forceUnit.transform.SetParent(unitSettingPos.transform);
            forceUnit.gameObject.SetActive(true);

            return forceUnit;
        }
        else
        {
            var forceUnit = CreatForceUnit();
            forceUnit.gameObject.SetActive(true);
            forceUnit.transform.SetParent(unitSettingPos.transform);
            forceUnit.gameObject.SetActive(true);

            return forceUnit;
        }
    }

    public void ReturnForceUnit(ForceUnit _forceUnit)
    {
        _forceUnit.gameObject.SetActive(false);
        _forceUnit.transform.SetParent(transform);
        forceUnitPool.Enqueue(_forceUnit);
    }
}
