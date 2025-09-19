using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ForceUnit : MonoBehaviour
{
    public ForceUnitData forceUnitData = new ForceUnitData();
    public Animator forceUnitAnimator;
    public GameObject targetUnit;
    public Transform muzzle;

    public Collider[] Engage;
    public List<GameObject> targetList;
    public List<GameObject> tmpeTargeList;
    public bool enemyUnitSearch = false;
    public bool isAttack = false;
    public bool isdie = false;
    public bool engage = false;

    int enemyUnitCount = 0;
    int tempEnemyUnitCount = 0;

    float tempradius;

    MeshRenderer muzzleFlash;

    [SerializeField] List<GameObject> enemyobj;

    private void Awake()
    {
        forceUnitAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        forceUnitData.unitState = UnitState.Start;
        tempradius = forceUnitData.AttackRange;
        targetUnit = null;
        muzzleFlash = muzzle.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
        StartForceUnit();
    }

    void ForceUnitSearch()
    {
        if (this.enemyUnitSearch == false)
        {
            this.enemyUnitCount = 0;
            this.tempEnemyUnitCount = 0;

            this.Engage = Physics.OverlapSphere(this.transform.position, tempradius);
            foreach (var item in this.Engage)
            {
                if (item.gameObject.activeSelf)
                {
                    if (item.CompareTag("enemy"))
                    {
                        this.enemyUnitCount++;
                        this.tmpeTargeList.Add(item.gameObject);
                        this.enemyUnitSearch = true;
                    }
                }
            }

            if (this.enemyUnitCount >= this.tempEnemyUnitCount)
            {
                for (var i = 0; i < this.enemyUnitCount; i++)
                {
                    this.targetList.Add(tmpeTargeList[i]);
                    this.tempEnemyUnitCount++;
                }
            }
        }
    }

    IEnumerator SetMuzzleFlash()
    {
        muzzleFlash.enabled = true;

        yield return new WaitForSeconds(0.1f);
        muzzleFlash.enabled = false;
    }

    void GoPlinState()
    {
        if (targetList.Count >= 1)
        {
            this.forceUnitData.unitState = UnitState.PileIn;

            int randomnum = UnityEngine.Random.Range(0, targetList.Count - 1);
            targetUnit = targetList[randomnum].gameObject;
        }
    }

    public void TakeDamage(int damage, GameObject takedamageunit)
    {
        this.enemyobj.Add(takedamageunit);
        this.forceUnitData.Hp = this.forceUnitData.Hp - damage;
        if (this.forceUnitData.Hp <= 0)
        {
            Manager.Instance.inGameManager.ForceUnitdie.Invoke();
            isdie = true;

            takedamageunit.gameObject.GetComponent<EnemyUnit>().enemyUnitData.unitState = UnitState.Idle;
            takedamageunit.gameObject.GetComponent<EnemyUnit>().tempUnitState = takedamageunit.gameObject.GetComponent<EnemyUnit>().enemyUnitData.unitState;
            takedamageunit.gameObject.GetComponent<EnemyUnit>().attackTargetUnit = Manager.Instance.inGameManager.baseCamp;

            DestroyUnit();
        }
        Manager.Instance.inGameManager.CallDataChange();
    }

    void SetupAttackRange()
    {
        var distance = Vector3.Distance(transform.position, targetUnit.transform.position);
        if( distance <= tempradius)
        {
            this.forceUnitData.unitState = UnitState.Fight;
        }
    }

    void CheckAttack()
    {
        forceUnitAnimator.SetBool("fightUnit",true);

        if (!this.isAttack)
        {
            this.isAttack = true;
            StartCoroutine(UnitIsAttack());
        }
    }

    IEnumerator UnitIsAttack()
    {
        while (this.forceUnitData.unitState == UnitState.Fight)
        {
            StartCoroutine(SetMuzzleFlash());
            UnitAttack();
            forceUnitAnimator.SetBool("fightUnit", false);
            yield return new WaitForSeconds(forceUnitData.AttackSpeed);
        }
    }

    public void UnitAttack()
    {
        if (targetUnit.gameObject.activeSelf)
        {
            if (targetUnit.CompareTag("enemy"))
            {
                this.targetUnit.GetComponent<EnemyUnit>().TakeDamage(this.forceUnitData.Damage, this.gameObject);
            }
        }
    }

    public void DestroyUnit()
    {
        for(int i = 0; i < enemyobj.Count-1; i++)
        {
            enemyobj[i].GetComponent<EnemyUnit>().attackTargetUnit = null;
            enemyobj[i].GetComponent<EnemyUnit>().moveTargetUnit = Manager.Instance.inGameManager.baseCamp;

            enemyobj[i].GetComponent<EnemyUnit>().forceUnitSearch = false;
            enemyobj[i].GetComponent<EnemyUnit>().onEngage = false;
            enemyobj[i].GetComponent<EnemyUnit>().isAttack = false;

            enemyobj[i].GetComponent<EnemyUnit>().targetList.Remove(this.gameObject);
            enemyobj[i].GetComponent<EnemyUnit>().tempTargetList.Remove(this.gameObject);
            enemyobj[i].GetComponent<EnemyUnit>().forceobj.Clear();

            enemyobj[i].GetComponent<EnemyUnit>().enemyUnitAnimator.SetBool("OnFight", false);
            enemyobj[i].GetComponent<EnemyUnit>().enemyUnitData.unitState = UnitState.Idle;
        }
        Manager.Instance.inGameManager.ForceUnitdie.Invoke();

        StartCoroutine(DestroyModel());
    }

    IEnumerator DestroyModel()
    {
        forceUnitAnimator.SetBool("dieUnit", true);

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void StartForceUnit()
    {
        if (forceUnitData.coUnitstate == null)
        {
            this.forceUnitData.coUnitstate = StartCoroutine(ForceState());
        }
        if (forceUnitData.coUnitAction == null)
        {
            this.forceUnitData.coUnitAction = StartCoroutine(ForceAction());
        }
    }

    IEnumerator ForceState()
    {
        while (isdie == false)
        {
            yield return new WaitForSeconds(0.07f);

            if (forceUnitData.unitState == UnitState.Start)
            {
                this.forceUnitData.unitState = UnitState.Idle;
            }
        }
    }

    IEnumerator ForceAction()
    {
        while (true)
        {
            switch (forceUnitData.unitState)
            {
                case UnitState.Start:
                    break;

                case UnitState.Idle:
                    ForceUnitSearch();
                    GoPlinState();
                    break;

                case UnitState.PileIn:
                    SetupAttackRange();
                    break;

                case UnitState.Fight:
                    CheckAttack();
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}
