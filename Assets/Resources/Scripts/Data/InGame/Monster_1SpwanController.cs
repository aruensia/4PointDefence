using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster_1SpwanController : MonoBehaviour
{
    public static Monster_1SpwanController moonsterPool;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyWaveGroup;
    [SerializeField] private GameObject enemyWaveGroupPos;
    [SerializeField] private List<GameObject> monsterSpwanPos;

    Queue<EnemyUnit> enemyUnitPool = new Queue<EnemyUnit>();
    Queue<GameObject> enemyWaveGroupPool = new Queue<GameObject>();

    bool enemyGroupPoolCheck = false;
    bool isSetMonster = true;
    int enemyGroupPoolCount;
    [SerializeField] int spwanTime = 5;

    int waveCount;
    int WaveTimer = 1;
    int checkWaveCount;

    private void Awake()
    {
        moonsterPool = this;
        InitEnemyPrefab(1000);
        InitEnemyGroup(500);
    }

    private void Start()
    {
        Manager.Instance.inGameManager.NextLevel += () => SetMonsterSpwanData();
        Manager.Instance.inGameManager.NextLevel += () => StartCoroutine(StartMonsterSetup());
    }

    void SetMonsterSpwanData()
    {
        waveCount = Manager.Instance.inGameManager.currentStageData.WaveCount;
        enemyGroupPoolCount = Manager.Instance.inGameManager.currentStageData.Monster_Count1;

        Debug.Log("waveCount는 : " + waveCount);
        Debug.Log("풀안의 생성 enemy개수는 : " + enemyGroupPoolCount);
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
        Debug.Log("몬스터 생성 시작 했음");
        while (isSetMonster)
        {
            if(WaveTimer == spwanTime)
            {
                if (checkWaveCount >= waveCount)
                {
                    //isSetMonster = false;
                    checkWaveCount = 0;
                    break;
                }

                GetEnemy(enemyGroupPoolCount);
                checkWaveCount++;
                WaveTimer = 1;
            }
            WaveTimer++;

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
        newmonster.enemyUnitData.UseSkillCount = tempEnemy.UseSkillCount;
        return newmonster;
    }

    private GameObject CreatWaveGruop()
    {
        var waveGruop = Instantiate(enemyWaveGroup);
        waveGruop.gameObject.SetActive(false);
        waveGruop.transform.SetParent(enemyWaveGroupPos.transform);
        return waveGruop;
    }

    public EnemyUnit GetEnemy(int _wavecount)
    {
        EnemyUnit tempenemy = null;
        for(int k  = 0; k < _wavecount; k++)
        {
            if (enemyUnitPool.Count > 0)
            {
                Manager.Instance.inGameManager.enemycount++;
                var enemy = enemyUnitPool.Dequeue();
                GameObject enemygroup;
                enemygroup = enemyWaveGroupPool.Dequeue();

                enemygroup = enemyWaveGroupPos.transform.GetChild(0).gameObject;
                int randomnum = UnityEngine.Random.Range(0, 4);
                enemy.transform.SetParent(enemygroup.transform);
                enemy.transform.position = enemygroup.transform.position;

                if (enemyWaveGroupPos.transform.GetChild(0).childCount == enemyGroupPoolCount)
                {
                    enemygroup.transform.SetParent(monsterSpwanPos[randomnum].transform);
                    enemygroup.transform.position = monsterSpwanPos[randomnum].transform.position;
                    enemygroup.gameObject.SetActive(true);

                    for (int i = 0; i < enemygroup.transform.childCount; i++)
                    {
                        enemygroup.transform.GetChild(i).gameObject.SetActive(true);
                        switch (randomnum)
                        {
                            case 0:
                                enemygroup.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(enemygroup.transform.localPosition.x + (i * 0.35f), transform.localPosition.y, 0);
                                break;

                            case 1:
                                enemygroup.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(enemygroup.transform.localPosition.x + (i * 0.35f), transform.localPosition.y, 0);
                                break;

                            case 2:
                                enemygroup.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(enemygroup.transform.localPosition.x + (i * 0.35f), transform.localPosition.y, 0);
                                break;

                            case 3:
                                enemygroup.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(enemygroup.transform.localPosition.x + (i * 0.35f), transform.localPosition.y, 0);
                                break;

                            case 4:
                                enemygroup.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(enemygroup.transform.localPosition.x + (i * 0.35f), transform.localPosition.y, 0);
                                break;
                        }
                    }
                }
                tempenemy = enemy;
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
                tempenemy = newenemy;
            }
        }
        Debug.Log("생성된 몬스터의 수 : " + Manager.Instance.inGameManager.enemycount);
        return tempenemy;
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
