using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public int playerMoney;

    public int totalGoldEnhance;
    public int Unit_1_Enhance = 1;
    public int Unit_2_Enhance = 1;
    public int Unit_3_Enhance = 1;

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
