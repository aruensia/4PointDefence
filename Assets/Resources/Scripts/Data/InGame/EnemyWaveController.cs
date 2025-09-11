using SubActionData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour, IWaveMove
{
    [SerializeField] float moveSpeed = 2.0f;
    Coroutine unitstate = null;
    float tempradius = 4f;
    public Collider[] Engage;
    public List<GameObject> EnemyList;
    public bool engageEnemySight = false;
    public int _moveCount = 0;
    GameObject moveLocation;
    

    void Start()
    {
        Manager.Instance.inGameManager.EnemyGroupActionData += StartEnemyWave;
        OnEnemyAction();
    }

    void OnEnemyAction()
    {
        Manager.Instance.inGameManager.EnemyGroupActionData.Invoke();
    }

    void StartEnemyWave()
    {
        if (unitstate == null)
        {
            unitstate = StartCoroutine(OnWaveMove());
        }
    }

    void PlayerContectisOn()
    {
        Engage = Physics.OverlapSphere(this.transform.position, tempradius);

        foreach(var enemy in Engage)
        {
            if(enemy.CompareTag("EnterTriger"))
            {
                StopCoroutine(OnWaveMove());

                for (int i = 0; i < Manager.Instance.inGameManager.currentStageData.Monster_Count1; i++)
                {
                    this.transform.GetChild(0).GetComponent<EnemyUnit>().StartEnemyUnit();
                    this.transform.GetChild(0).SetParent(Manager.Instance.inGameManager.battleField.transform);

                    engageEnemySight = true;
                }
                Monster_1SpwanController.moonsterPool.ReturnWaveGroup(this.gameObject);
            }
        }
    }

    IEnumerator OnWaveMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.07f);
            PlayerContectisOn();
            if (engageEnemySight == true)
            {
                break;
            }
            WaveMove();
        }
    }

    public void WaveMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, Manager.Instance.inGameManager.baseCamp.transform.position, moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, tempradius);
    }

    private void OnDisable()
    {
        Engage = null;
    }
}
