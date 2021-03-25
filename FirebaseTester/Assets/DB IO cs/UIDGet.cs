using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using System.Threading;
using System.Threading.Tasks;

public class UIDGet : MonoBehaviour
{
    public DatabaseReference Reference;
    public static UIDGet uidGetter;
    // Start is called before the first frame update

    void Start()
    {
        Reference = FirebaseDatabase.DefaultInstance.RootReference.Child("UID").Reference;
        if (uidGetter == null)
        {
            uidGetter = this;
        }
    }

    public int UidDbExistsCheck()
    {
        print(nameof(UidDbExistsCheck));
        int flag = -2;

        lock (uidGetter)
        {
            Reference.GetValueAsync().ContinueWith(task => {
                if (task.IsCompleted)
                {
                    DataSnapshot result = task.Result;
                    if (result.ChildrenCount <= 0) flag = 0;
                    else flag = 1;
                }
                else
                {
                    flag = -1;
                }
            });
        }
        {
            Debug.Log("lock On : UID DB Is Exists");
        }
        Debug.Log("UID DB Is Exists flag = " + flag);
        return flag;
    }

    public int GetUid()
    {
        int getUid = 0;

        lock (uidGetter)
        {
            Reference.GetValueAsync().ContinueWith(task =>
            {

                if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    getUid = (int)snapshot.Value;
                    Debug.Log("Get uid Completed");
                }
                else
                    Debug.Log("Get uid Fail");
            });
        }
        { Debug.Log("lock On : GetUid"); }

        Debug.Log("UID Get End : " + getUid);
        return getUid;
    }
}
