using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceUnitData : SubActionData.TableData
{
    private int _index;
    public UnitType unitType;
    private string _name;
    private int _hp;
    private int _armor;
    private int _damage;
    private float _attackRange;
    private float _moveSpeed;
    private int _useActionButtonCount;
    private int _skillCount;
    private int _cost;
    public UnitSize unitSize;
    public UnitState unitState;
    public Coroutine coUnitstate = null;
    public Coroutine coUnitAction = null;
    EnemyWaveController waveController;

    [SerializeField] GameObject targetUnit;

    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Hp
    {
        get { return _hp; }
        set
        {
            if (value <= 0)
            {
                _hp = 0;
            }
            else
            {
                _hp = value;
            }
        }
    }

    public int Armor
    {
        get { return _armor; }
        set
        {
            if (value <= 0)
            {
                _armor = 0;
            }
            else
            {
                _armor = value;
            }
        }
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public float AttackRange
    {
        get { return _attackRange; }
        set { _attackRange = value; }
    }

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public int UseActionButtonCount
    {
        get { return _useActionButtonCount; }
        set { _useActionButtonCount = value; }
    }

    public int UseSkillCount
    {
        get { return _skillCount; }
        set { _skillCount = value; }
    }

    public int Cost
    {
        get { return _cost; }
        set { _cost = value; }
    }
}
