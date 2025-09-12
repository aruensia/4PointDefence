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
            Debug.Log("µ¥ÀÌÅÍ ¿È");
        }
        else
        {
            Debug.Log("µ¥ÀÌÅÍ ¾È¿È");
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
            Debug.LogWarning(message: $"µ· ¾÷µ« ½ÇÆÐ! »çÀ¯ : {DbTask.Exception}");
        }
        else
        {
            Debug.Log("µ· ¾÷µ¥ÀÌÆ® ¿Ï·á");
        }
    }


}
