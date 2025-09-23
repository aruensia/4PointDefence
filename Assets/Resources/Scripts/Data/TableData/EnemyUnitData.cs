using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitData : SubActionData.TableData
{
    [SerializeField] private int _index;
    public UnitType unitType;
    [SerializeField] private string _name;
    [SerializeField] private int _hp;
    [SerializeField] private int _armor;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _useActionButtonCount;
    [SerializeField] private int _skillCount;
    public UnitSize unitSize;
    public UnitState unitState;
    public Coroutine coUnitstate = null;
    public Coroutine coUnitAction = null;


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
}
