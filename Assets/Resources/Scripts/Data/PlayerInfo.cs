using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public int playerMoney;

    public int totalGoldEnhance;
    public int totalAttackEnhance;
    public int totalDefenceEnhance;
    public int totalHpEnhance;

    public int _defaultGold = 1;

    public int DefaultGold
    {
        get { return _defaultGold; }
        set
        {
            if (_defaultGold >= 1000)
            {
               _defaultGold = 1000;
            }
            else
            {
                _defaultGold = value;
            }
        }
    }

}
