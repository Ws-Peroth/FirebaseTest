using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;

public class DBIO3 : MonoBehaviour
{
    public int sysflag = -2;
    UserDB user;
    DatabaseReference Reference;
    DataSnapshot getDatabase;

    public void Start()
    {
        Reference = FirebaseDatabase.DefaultInstance.RootReference.Reference;
    }

    public void OnSignal()
    {
        StartCoroutine(nameof(GetDBCouroutine));
    }

    IEnumerator GetDBCouroutine()
    {
        GetDB();
        while (sysflag == -1)
        {
            print("waiting... \n" + "sysflag == " + sysflag);
            yield return null;

        }
        print("Db Get Finish : " + sysflag);

        yield break;
    }

    public void GetDB()
    {
        print("call GetDB");
        Reference.Child("Users").GetValueAsync().ContinueWith(task =>
        {

            if (task.IsCompleted)
            {
                getDatabase = task.Result;

                if (getDatabase.ChildrenCount > 0)
                {
                    sysflag = 1;
                    print("flag = 1");
                }
                else
                {
                    sysflag = 0; print("flag = 0");
                }
            }
            else
            {
                sysflag = -1;
            }

            print(sysflag > 0 ? "Return True" : "Return False");
        });
    }
}
