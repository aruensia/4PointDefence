using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructUnit : MonoBehaviour
{
    public StructUnitData unitData = new StructUnitData();
    public bool isdie = false;

    public void TakeDamage(int damage, GameObject takedamageunit)
    {
        this.unitData.Hp = this.unitData.Hp - damage;
        if (this.unitData.Hp <= 0)
        {
            Manager.Instance.inGameManager.isGameOver = true;

            Debug.Log(this.gameObject.name + " 가 죽었습니다.");
            Manager.Instance.inGameManager.OnGameOver();
        }
        Manager.Instance.inGameManager.CallDataChange();
    }
}
