using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_2SpwanController : MonoBehaviour
{
    public static Monster_2SpwanController moonsterPool;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyWaveGroup;
    [SerializeField] private GameObject enemyWaveGroupPos;
    [SerializeField] private List<GameObject> monsterSpwanPos;

    Queue<EnemyUnit> enemyUnitPool = new Queue<EnemyUnit>();
    Queue<GameObject> enemyWaveGroupPool = new Queue<GameObject>();

    bool enemyGroupPoolCheck = false;
    int enemyGroupPoolCount = 10;

    private void Awake()
    {
        moonsterPool = this;
        InitEnemyPrefab(1000);
        InitEnemyGroup(500);
    }

    private void Start()
    {
        StartCoroutine(StartMonsterSetup());
    }


    private void InitEnemyPrefab(int initcount)
    {
        for(int i = 0; i < initcount; i++)
        {
            enemyUnitPool.Enqueue(CreatMonster());
        }
    }

    private void InitEnemyGroup(int initcount)
    {
        for (int i = 0; i < initcount; i++)
        {
            enemyWaveGroupPool.Enqueue(CreatWaveGruop());
        }
    }

    IEnumerator StartMonsterSetup()
    {
        while (true)
        {
            var monster = GetEnemy();

            yield return new WaitForSeconds(1);
        }
    }

    private EnemyUnit CreatMonster()
    {
        var newmonster = Instantiate(enemyPrefab).GetComponent<EnemyUnit>();
        newmonster.gameObject.SetActive(false);
        newmonster.transform.SetParent(transform);
        EnemyUnitData tempEnemy = (EnemyUnitData)Manager.Instance.dataloader.tableDatas["EnemyUnitTable"][0];
        newmonster.enemyUnitData.Index = tempEnemy.Index;
        newmonster.enemyUnitData.unitType = tempEnemy.unitType;
        newmonster.enemyUnitData.Name = tempEnemy.Name;
        newmonster.enemyUnitData.Hp = tempEnemy.Hp;
        newmonster.enemyUnitData.Armor = tempEnemy.Armor;
        newmonster.enemyUnitData.Damage = tempEnemy.Damage;
        newmonster.enemyUnitData.AttackRange = tempEnemy.AttackRange;
        newmonster.enemyUnitData.MoveSpeed = tempEnemy.MoveSpeed;
        newmonster.enemyUnitData.unitSize = tempEnemy.unitSize;
        newmonster.enemyUnitData.unitState = tempEnemy.unitState;
        return newmonster;
    }

    private GameObject CreatWaveGruop()
    {
        var waveGruop = Instantiate(enemyWaveGroup);
        waveGruop.gameObject.SetActive(false);
        waveGruop.transform.SetParent(enemyWaveGroupPos.transform);
        return waveGruop;
    }

    public EnemyUnit GetEnemy()
    {
        if ( enemyUnitPool.Count > 0)
        {
            var enemy = enemyUnitPool.Dequeue();
            GameObject enemygroup;
            if (enemyGroupPoolCheck == false)
            {
                enemygroup = enemyWaveGroupPool.Dequeue();
                enemyGroupPoolCheck = true;
            }

            enemygroup = enemyWaveGroupPos.transform.GetChild(0).gameObject;
            int randomnum = UnityEngine.Random.Range(0, 5);
            enemy.transform.SetParent(enemygroup.transform);
            enemy.transform.position = enemygroup.transform.position;

            if (enemyWaveGroupPos.transform.GetChild(0).childCount == enemyGroupPoolCount)
            {
                enemygroup.transform.SetParent(monsterSpwanPos[randomnum].transform);
                enemygroup.transform.position = monsterSpwanPos[randomnum].transform.position;
                enemygroup.gameObject.SetActive(true);

                for(int i = 0; i < enemygroup.transform.childCount; i++)
                {
                    enemygroup.transform.GetChild(i).gameObject.SetActive(true);
                    enemygroup.transform.GetChild(i).gameObject.transform.position = new Vector3(enemygroup.transform.position.x+(i*0.2f), enemygroup.transform.position.y, enemygroup.transform.position.z);
                    enemyGroupPoolCheck = false;
                }
            }
            return enemy;
        }
        else
        {
            var newenemy = CreatMonster();
            newenemy.gameObject.SetActive(true);
            int randomnum = UnityEngine.Random.Range(0, 5);
            newenemy.transform.SetParent(enemyWaveGroupPos.transform.GetChild(0).transform);
            newenemy.transform.position = enemyWaveGroupPos.transform.GetChild(0).position;
            if (enemyWaveGroupPos.transform.GetChild(0).childCount == enemyGroupPoolCount)
            {
                enemyWaveGroupPos.transform.GetChild(0).SetParent(monsterSpwanPos[randomnum].transform);
                newenemy.gameObject.SetActive(true);
            }
            return newenemy;
        }
    }

    public void ReturnEnemy(EnemyUnit _enemy)
    {
        _enemy.gameObject.SetActive(false);
        _enemy.transform.SetParent(transform);
        moonsterPool.enemyUnitPool.Enqueue(_enemy);
    }

    public void ReturnWaveGroup(GameObject _wavegruop)
    {
        _wavegruop.gameObject.SetActive(false);
        _wavegruop.transform.SetParent(enemyWaveGroupPos.transform);
        moonsterPool.enemyWaveGroupPool.Enqueue(_wavegruop);
    }
}
