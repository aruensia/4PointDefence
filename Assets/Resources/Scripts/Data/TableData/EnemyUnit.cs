using SubActionData;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyUnit : MonoBehaviour , SubActionData.IUnitAction
{
    public Animator enemyUnitAnimator;

    public EnemyUnitData enemyUnitData = new EnemyUnitData();
    public GameObject attackTargetUnit;
    public GameObject moveTargetUnit;
    EnemyWaveController enemyWaveController;
    public Collider[] Engage;
    bool isdie = false;
    public bool isAttack = false;
    public bool forceUnitSearch = false;
    public bool onEngage = false;

    float tempradius = 4f;

    int forceUnitCount;
    int tempforceUnitCount;

    public List<GameObject> targetList;
    public List<GameObject> tempTargetList;
    public UnitState tempUnitState = UnitState.Start;

    public List<GameObject> forceobj;

    private void Awake()
    {
        enemyUnitAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        Manager.Instance.inGameManager.ForceUnitdie += CheckFightState;
        Manager.Instance.inGameManager.ForceUnitdie += ForceUnitSearch;
        enemyWaveController = transform.parent.gameObject.GetComponent<EnemyWaveController>();
        enemyUnitData.unitState = UnitState.Start;
        moveTargetUnit = Manager.Instance.inGameManager.baseCamp;
    }

    void CheckAttack()
    {

        enemyUnitAnimator.SetBool("OnFight", true);

        if(!isAttack)
        {
            isAttack = true;

            this.transform.LookAt(attackTargetUnit.transform);
            StartCoroutine(UnitIsAttack());
        }
    }

    IEnumerator UnitIsAttack()
    {
        while(enemyUnitData.unitState == UnitState.Fight)
        {
            if(Manager.Instance.inGameManager.isGameOver == true)
            {
                break;
            }
            UnitAttack();
            enemyUnitAnimator.SetBool("OnFight", false);
            yield return new WaitForSeconds(5);
        }
    }

    public void UnitAttack()
    {
        if (attackTargetUnit.gameObject.activeInHierarchy || Manager.Instance.inGameManager.isGameOver == false)
        {
            if(attackTargetUnit.CompareTag("ForceUnit"))
            {
                this.attackTargetUnit.GetComponent<ForceUnit>().TakeDamage(this.enemyUnitData.Damage, this.gameObject);
            }
            else if (attackTargetUnit.CompareTag("StructUnit"))
            {
                this.attackTargetUnit.GetComponent<StructUnit>().TakeDamage(this.enemyUnitData.Damage, this.gameObject);
            }
        }
    }

    public void UnitHold()
    {
        throw new System.NotImplementedException();
    }

    public void UnitMove()
    {
        if(this.moveTargetUnit == null)
        {
            this.moveTargetUnit = Manager.Instance.inGameManager.baseCamp;
            this.transform.LookAt(Manager.Instance.inGameManager.baseCamp.transform);
        }

        Vector3 direction = (moveTargetUnit.transform.position - gameObject.GetComponent<Rigidbody>().position).normalized;
        this.transform.LookAt(moveTargetUnit.transform);
        gameObject.GetComponent<Rigidbody>().velocity = direction * (enemyUnitData.MoveSpeed * 0.6f);
    }

    public void UnitSearchTarget()
    {
        int randomnum = UnityEngine.Random.Range(0, targetList.Count-1);

        if(targetList.Count == 0)
        {
            moveTargetUnit = Manager.Instance.inGameManager.baseCamp;
        }
        else
        {
            moveTargetUnit = targetList[randomnum].gameObject;
        }
    }

    public void UnitPileIn()
    {
        SetupTarget();
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        if( moveTargetUnit != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackTargetUnit.transform.position, enemyUnitData.MoveSpeed * Time.deltaTime);
            this.transform.LookAt(attackTargetUnit.transform);

            var distance = Vector3.Distance(transform.position, attackTargetUnit.transform.position);
            if (distance <= 0.3f)
            {
                this.onEngage = true;
                this.enemyUnitData.unitState = UnitState.Fight;
                this.tempUnitState = UnitState.Fight;
            }
        }
        else
        {
            this.moveTargetUnit = null;
            this.attackTargetUnit = null;
            this.forceUnitSearch = false;

            this.enemyUnitData.unitState = UnitState.Idle;

            targetList.Clear();
            tempTargetList.Clear();
        }
    }

    public void UnitStop()
    {
        throw new System.NotImplementedException();
    }

    void SetupTarget()
    {
        attackTargetUnit = moveTargetUnit;
    }

    void ForceUnitSearch()
    {
        if(this.forceUnitSearch == false)
        {
            this.forceUnitCount = 0;
            this.tempforceUnitCount = 0;
       
            this.Engage = Physics.OverlapSphere(this.transform.position, tempradius);
            foreach (var item in this.Engage)
            {
                if (item.gameObject.activeSelf)
                {
                    if(item.CompareTag("ForceUnit") && item.GetComponent<ForceUnit>().isdie == false)
                    {
                        this.forceUnitCount++;
                        this.tempTargetList.Add(item.gameObject);
                        this.forceUnitSearch = true;
                    }
                    else if(item.CompareTag("StructUnit"))
                    {
                        this.forceUnitCount++;
                        this.tempTargetList.Add(item.gameObject);
                        this.forceUnitSearch = true;
                    }
                }
            }

            if (this.forceUnitCount >= this.tempforceUnitCount)
            {
                for (var i = 0; i < this.forceUnitCount; i++)
                {
                    this.targetList.Add(tempTargetList[i]);
                    this.tempforceUnitCount++;
                }
            }
        }
    }

    void CheckFightState()
    {
        if(this.attackTargetUnit != Manager.Instance.inGameManager.baseCamp.gameObject || this.attackTargetUnit == null)
        {
            this.enemyUnitData.unitState = UnitState.Idle;
            this.attackTargetUnit = null;
            this.moveTargetUnit = null;

            this.forceUnitSearch = false;
            this.onEngage = false;

            targetList.Clear();
            tempTargetList.Clear();
        }
    }

    void GoPlinState()
    {
        if(targetList.Count >= 1)
        {
            this.enemyUnitData.unitState = UnitState.PileIn;
            this.tempUnitState = UnitState.PileIn;

            int randomnum = UnityEngine.Random.Range(0, targetList.Count - 1);
            moveTargetUnit = targetList[randomnum].gameObject;
        }
    }

    public void TakeDamage(int damage, GameObject targetunit)
    {
        enemyUnitData.Hp = enemyUnitData.Hp - damage;
        this.forceobj.Add(targetunit);
        if (enemyUnitData.Hp <= 0)
        {
            enemyUnitData.unitState = UnitState.Death;
        }
        Manager.Instance.inGameManager.CallDataChange();
    }

    void ForceUnitObjStateChange()
    {
        for (int i = 0; i < forceobj.Count - 1; i++)
        {
            forceobj[i].GetComponent<ForceUnit>().targetUnit = null;

            forceobj[i].GetComponent<ForceUnit>().enemyUnitSearch = false;
            forceobj[i].GetComponent<ForceUnit>().isAttack = false;

            forceobj[i].GetComponent<ForceUnit>().targetList.Clear();
            forceobj[i].GetComponent<ForceUnit>().tmpeTargeList.Clear();

            forceobj[i].GetComponent<ForceUnit>().forceUnitAnimator.SetBool("fightUnit", false);
            forceobj[i].GetComponent<ForceUnit>().forceUnitData.unitState = UnitState.Idle;
        }
    }

    public void DestroyUnit()
    {
        Manager.Instance.inGameManager.enemycount--;
        Manager.Instance.inGameManager.getMoney += 10;
        Manager.Instance.inGameManager.totalkillCount ++;
        ForceUnitObjStateChange();
        Monster_1SpwanController.moonsterPool.ReturnEnemy(this);

        if (Manager.Instance.inGameManager.enemycount == 0)
        {
            Manager.Instance.inGameUi.GameTime.gameObject.SetActive(true);
            Manager.Instance.inGameManager.waitNextSetage = true;
        }
    }

    public void StartEnemyUnit()
    {
        if (enemyUnitData.coUnitstate == null)
        {
            this.enemyUnitData.coUnitstate = StartCoroutine(EnemyState());
        }
        if (enemyUnitData.coUnitAction == null)
        {
            this.enemyUnitData.coUnitAction = StartCoroutine(EnemyAction());
        }
    }

    IEnumerator EnemyState()
    {
        while (isdie == false)
        {
            yield return new WaitForSeconds(0.07f);

            if( enemyUnitData.unitState == UnitState.Start)
            {
                this.enemyUnitData.unitState = UnitState.Idle;
                this.tempUnitState = UnitState.Idle;
            }
        }
    }

    IEnumerator EnemyAction()
    {
        while (true)
        {
            switch (enemyUnitData.unitState)
            {
                case UnitState.Start:
                    break;

                case UnitState.Idle:
                    UnitMove();
                    ForceUnitSearch();
                    GoPlinState();
                    break;

                case UnitState.PileIn:
                    UnitPileIn();
                    break;

                case UnitState.Fight:
                    CheckAttack();
                    break;

                case UnitState.Death:
                    DestroyUnit();
                    break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.CompareTag("ForceUnit"))
    //    {
    //        Debug.Log("트리거로 아군 유닛 찾음");
    //        UnitSearchTarget();
    //        this.enemyUnitData.unitState = UnitState.PileIn;
    //        this.tempUnitState = UnitState.PileIn;
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, this.tempradius);
    }
}

