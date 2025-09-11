using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : SubActionData.TableData
{
    private int _index;
    private string _name;
    public List<GameObject> _monster;
    private int _monster_count1;
    private int _monster_count2;
    private int _monster_count3;
    private int _waveCount;

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

    public int Monster_Count1
    {
        get { return _monster_count1; }
        set { _monster_count1 = value; }
    }

    public int Monster_Count2
    {
        get { return _monster_count2; }
        set { _monster_count2 = value; }
    }

    public int Monster_Count3
    {
        get { return _monster_count3; }
        set { _monster_count3 = value; }
    }

    public int WaveCount
    {
        get { return _waveCount; }
        set { _waveCount = value; }
    }
}
