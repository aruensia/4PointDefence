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

   

    // Start is called before the first frame update
    void Start()
    {
        this.dbRef = FirebaseAuthMgr.dbRef;
        this.user = FirebaseAuthMgr.user;

        if(dbRef != null)
        {
            Debug.Log("데이터 옴");
        }
        else
        {
            Debug.Log("데이터 안옴");
        }
    }

    public void SaveToDb()
    {
        var _initMoney = 5;

        StartCoroutine(UpdataMoney(_initMoney));
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

            Debug.Log("데이터 로드 성공");
            Debug.Log(Manager.Instance.player.playerMoney);

        }
    }
}
