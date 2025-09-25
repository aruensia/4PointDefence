using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine.UI;

public class FirebaseDbMgr : MonoBehaviour
{
    DatabaseReference dbRef;
    FirebaseUser user;
    public bool DbDataLoadOn = false;

    // Start is called before the first frame update
    void Start()
    {
        this.dbRef = FirebaseAuthMgr.dbRef;
        this.user = FirebaseAuthMgr.user;

        if(dbRef != null)
        {
            Debug.Log("데이터 옴");
            DbDataLoadOn = true;
        }
        else
        {
            Debug.Log("데이터 안옴");
        }
    }

    public void SaveToDb()
    {
        var _initMoney = Manager.Instance.player.playerMoney;
        var _initGold = Manager.Instance.player.totalGoldEnhance;
        var _initBaseHp = Manager.Instance.player.BaseHpEnhance;
        var _initUnit1 = Manager.Instance.player.Unit_1_Enhance;
        var _initUnit2 = Manager.Instance.player.Unit_2_Enhance;
        var _initUnit3 = Manager.Instance.player.Unit_3_Enhance;

        StartCoroutine(UpdataMoney(_initMoney));
        StartCoroutine(UpdataTotalGoldCount(_initGold));
        StartCoroutine(UpdataBaesHpCount(_initBaseHp));
        StartCoroutine(UpdataUnit1Count(_initUnit1));
        StartCoroutine(UpdataUnit2Count(_initUnit2));
        StartCoroutine(UpdataUnit3Count(_initUnit3));
    }

    IEnumerator UpdataMoney(int money)
    {
        var DbTask = dbRef.Child("users").Child(user.UserId).Child("money").SetValueAsync(money);

        yield return new WaitUntil(predicate: () => DbTask.IsCompleted);

        if(DbTask.Exception != null)
        {
            Debug.LogWarning(message: $"돈 업뎃 실패! 사유 : {DbTask.Exception}");
        }
        else
        {
            Debug.Log("돈 업데이트 완료");
        }
    }

    IEnumerator UpdataTotalGoldCount(float totalgold)
    {
        var DbTask = dbRef.Child("users").Child(user.UserId).Child("totalgold").SetValueAsync(totalgold);

        yield return new WaitUntil(predicate: () => DbTask.IsCompleted);

        if (DbTask.Exception != null)
        {
            Debug.LogWarning(message: $"돈 업뎃 실패! 사유 : {DbTask.Exception}");
        }
        else
        {
            Debug.Log("골드 강화 업데이트 완료");
        }
    }

    IEnumerator UpdataBaesHpCount(float baseHp)
    {
        var DbTask = dbRef.Child("users").Child(user.UserId).Child("baseHp").SetValueAsync(baseHp);

        yield return new WaitUntil(predicate: () => DbTask.IsCompleted);

        if (DbTask.Exception != null)
        {
            Debug.LogWarning(message: $"돈 업뎃 실패! 사유 : {DbTask.Exception}");
        }
        else
        {
            Debug.Log("골드 강화 업데이트 완료");
        }
    }

    IEnumerator UpdataUnit1Count(int unit1)
    {
        var DbTask = dbRef.Child("users").Child(user.UserId).Child("unit1").SetValueAsync(unit1);

        yield return new WaitUntil(predicate: () => DbTask.IsCompleted);

        if (DbTask.Exception != null)
        {
            Debug.LogWarning(message: $"돈 업뎃 실패! 사유 : {DbTask.Exception}");
        }
        else
        {
            Debug.Log("유닛1 업데이트 완료");
        }
    }

    IEnumerator UpdataUnit2Count(int unit2)
    {
        var DbTask = dbRef.Child("users").Child(user.UserId).Child("unit2").SetValueAsync(unit2);

        yield return new WaitUntil(predicate: () => DbTask.IsCompleted);

        if (DbTask.Exception != null)
        {
            Debug.LogWarning(message: $"돈 업뎃 실패! 사유 : {DbTask.Exception}");
        }
        else
        {
            Debug.Log("유닛2 업데이트 완료");
        }
    }

    IEnumerator UpdataUnit3Count(int unit3)
    {
        var DbTask = dbRef.Child("users").Child(user.UserId).Child("unit3").SetValueAsync(unit3);

        yield return new WaitUntil(predicate: () => DbTask.IsCompleted);

        if (DbTask.Exception != null)
        {
            Debug.LogWarning(message: $"돈 업뎃 실패! 사유 : {DbTask.Exception}");
        }
        else
        {
            Debug.Log("유닛3 업데이트 완료");
        }
    }

    public void LoadToDb()
    {
        StartCoroutine(LoadUserData());
    }

    public IEnumerator LoadUserData()
    {
        var DBTask = dbRef.Child("users").Child(user.UserId).GetValueAsync();

        yield return new WaitUntil(() => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning($"데이터 불러오기 실패! 사유: {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            Debug.LogWarning("저장된 데이터가 없습니다!");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            Manager.Instance.player.playerMoney = int.Parse(snapshot.Child("money").Exists ? snapshot.Child("money").Value.ToString() : "0");
            Manager.Instance.player.totalGoldEnhance = int.Parse(snapshot.Child("totalgold").Exists ? snapshot.Child("totalgold").Value.ToString() : "0");
            Manager.Instance.player.BaseHpEnhance = int.Parse(snapshot.Child("baseHp").Exists ? snapshot.Child("baseHp").Value.ToString() : "0");
            Manager.Instance.player.Unit_1_Enhance = int.Parse(snapshot.Child("unit1").Exists ? snapshot.Child("unit1").Value.ToString() : "0");
            Manager.Instance.player.Unit_2_Enhance = int.Parse(snapshot.Child("unit2").Exists ? snapshot.Child("unit2").Value.ToString() : "0");
            Manager.Instance.player.Unit_3_Enhance = int.Parse(snapshot.Child("unit3").Exists ? snapshot.Child("unit3").Value.ToString() : "0");

            TitleManager.playerData = true;

            Debug.Log("데이터 로드 성공");
            Debug.Log("데이터 로드 성공" + Manager.Instance.player.playerMoney);
        }
    }
}
