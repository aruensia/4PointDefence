using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        bool isTriggerOn = false;

        if(other.tag == "EnemyGroup")
        {
            if(isTriggerOn == false)
            {
                Debug.Log("테그에 접촉해서 값을 올림");
                //other.transform.parent.gameObject.GetComponent<EnemyWaveController>()._moveCount++;
                other.transform.gameObject.GetComponent<EnemyWaveController>()._moveCount++;
                isTriggerOn = true;
            }
        }
    }
}
