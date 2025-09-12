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

        if(this.dbRef == null)
        {
            Debug.Log("이거 널임");
        }
    }

    public void SaveToDb()
    {
        var _initMoney = Manager.Instance.player.playerMoney;

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


}
