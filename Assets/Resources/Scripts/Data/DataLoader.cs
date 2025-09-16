using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataLoader
{
    public Dictionary<string, List<SubActionData.TableData>> tableDatas { get; private set; } = new Dictionary<string, List<SubActionData.TableData>>()
    {
        {"ForceUnitTable", new List<SubActionData.TableData>()},
        {"EnemyUnitTable", new List<SubActionData.TableData>()},
        {"StructUnitTable", new List<SubActionData.TableData>()},
        {"StageTable", new List<SubActionData.TableData>()},
    };

    public void DataLoad()
    {
        foreach (var item in tableDatas)
        {
            TextAsset csvFiles = Resources.Load<TextAsset>($"Tables/{item.Key}");

            if (csvFiles == null)
            {
                return;
            }

            string[] lines = csvFiles.text.Split('\n');
            switch (csvFiles.name)
            {
                case "ForceUnitTable":
                    for (int i = 2; i < lines.Length - 1; i++)
                    {
                        string[] values = lines[i].Split(',');
                        ForceUnitData tempForceUnit = new ForceUnitData();
                        tempForceUnit.Index = int.Parse(values[0]);
                        tempForceUnit.unitType = (UnitType)int.Parse(values[1]);
                        tempForceUnit.Name = values[2].ToString();
                        tempForceUnit.Hp = int.Parse(values[3]);
                        tempForceUnit.Armor = int.Parse(values[4]);
                        tempForceUnit.Damage = int.Parse(values[5]);
                        tempForceUnit.AttackRange = float.Parse(values[6]);
                        tempForceUnit.AttackSpeed = float.Parse(values[7]);
                        tempForceUnit.unitSize = (UnitSize)int.Parse(values[8]);
                        tempForceUnit.unitState = (UnitState)int.Parse(values[9]);
                        tempForceUnit.UseSkillCount = int.Parse(values[11]);
                        tempForceUnit.Cost = int.Parse(values[12]);

                        item.Value.Add(tempForceUnit);
                    }
                    break;

                case "EnemyUnitTable":
                    for (int i = 2; i < lines.Length - 1; i++)
                    {
                        string[] values = lines[i].Split(',');
                        EnemyUnitData tempEnemyUnit = new EnemyUnitData();
                        tempEnemyUnit.Index = int.Parse(values[0]);
                        tempEnemyUnit.unitType = (UnitType)int.Parse(values[1]);
                        tempEnemyUnit.Name = values[2].ToString();
                        tempEnemyUnit.Hp = int.Parse(values[3]);
                        tempEnemyUnit.Armor = int.Parse(values[4]);
                        tempEnemyUnit.Damage = int.Parse(values[5]);
                        tempEnemyUnit.AttackRange = float.Parse(values[6]);
                        tempEnemyUnit.MoveSpeed = float.Parse(values[7]);
                        tempEnemyUnit.unitSize = (UnitSize)int.Parse(values[8]);
                        tempEnemyUnit.unitState = (UnitState)int.Parse(values[9]);
                        tempEnemyUnit.UseSkillCount = int.Parse(values[11]);

                        item.Value.Add(tempEnemyUnit);
                    }
                    break;

                case "StructUnitTable":
                    for (int i = 2; i < lines.Length - 1; i++)
                    {
                        string[] values = lines[i].Split(',');
                        StructUnitData tempStructUnit = new StructUnitData();
                        tempStructUnit.Index = int.Parse(values[0]);
                        tempStructUnit.unitType = (UnitType)int.Parse(values[1]);
                        tempStructUnit.Name = values[2].ToString();
                        tempStructUnit.Hp = int.Parse(values[3]);
                        tempStructUnit.Armor = int.Parse(values[4]);
                        tempStructUnit.Damage = int.Parse(values[5]);
                        tempStructUnit.AttackRange = float.Parse(values[6]);
                        tempStructUnit.MoveSpeed = float.Parse(values[7]);
                        tempStructUnit.unitSize = (UnitSize)int.Parse(values[8]);
                        tempStructUnit.unitState = (UnitState)int.Parse(values[9]);

                        item.Value.Add(tempStructUnit);
                    }
                    break;

                case "StageTable":
                    for (int i = 2; i < lines.Length - 1; i++)
                    {
                        string[] values = lines[i].Split(',');
                        StageData tempStage = new StageData();
                        tempStage.Index = int.Parse(values[0]);
                        tempStage.Name = values[1].ToString();
                        tempStage.Monster_Count1 = int.Parse(values[2]);
                        tempStage.Monster_Count2 = int.Parse(values[4]);
                        tempStage.Monster_Count3 = int.Parse(values[6]);
                        tempStage.WaveCount = int.Parse(values[7]);

                        item.Value.Add(tempStage);
                    }
                    break;
            }
        }
    }
}
