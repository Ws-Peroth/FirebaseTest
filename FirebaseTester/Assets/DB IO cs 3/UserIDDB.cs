using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using System.Threading.Tasks;

public class UserIDDB : MonoBehaviour
{
    public static UserIDDB userUidManager = null;
    DatabaseReference Reference;
    DataSnapshot getDatabase;
    BigInteger uid;
    int uidFlag;

    private void Start()
    {
        if (userUidManager == null)
        {
            userUidManager = this;
        }

        Reference = FirebaseDatabase.DefaultInstance.RootReference.Reference.Child("Uid");

        uidFlag = -2;

        InitializeUid();
    }

    public void InitializeUid()
    {
        StartCoroutine(nameof(InitializeUidCouroutine));
    }

    IEnumerator InitializeUidCouroutine()
    {
        UidGetter();

        while (uidFlag == -2)
        {
            yield return null;
        }

        if (uidFlag == -1)
        {
            print("Uid getter task is not complete");
        }
        else if (uidFlag == 0)
        {
            print("uid is null");
            StartCoroutine(nameof(UidReSettingCouroutine));
            uid = 100000000;
        }
        else if (uidFlag == 1)
        {
            print("uid is exixts");
            uid = BigInteger.Parse(getDatabase.Value.ToString());
        }
        else
        {
            print("::ERROR::\n" +
                "uidFlag is out of range " +
                "( -2 <= uidFlag <= 1 ) : " + uidFlag + "\n\n" +
                "UserIDDB.cs : " + nameof(InitializeUidCouroutine)
                );
        }

        yield break;
    }

    public void UidGetter()
    {
        print("call GetDB");
        Reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                getDatabase = task.Result;
                print("Uid Count : " + getDatabase.Value);

                if (getDatabase.Value != null)
                    uidFlag = 1;

                else
                    uidFlag = 0;
            }
            else
            {
                uidFlag = -1;
            }

            print("flag = " + uidFlag);
            print(uidFlag > 0 ? "Return True" : "Return False");
        });
    }

    public void UidReSetting()
    {
        uidFlag = -2;
        uid = 100000000;
        Reference.SetValueAsync(uid.ToString()).ContinueWith(task =>
        {
            if (task.IsCompleted)
                uidFlag = 1;
            else
                uidFlag = -1;
        });
    }

    IEnumerator UidReSettingCouroutine()
    {
        UidReSetting();

        while (uidFlag == -2)
            yield return null;

        if (uidFlag == 1)
            print("Init Uid Complete");

        else if (uidFlag == -1)
            print("Uid Init task is not complete");

        else if(uidFlag != 0)
            print("::ERROR::\n" +
                "uidFlag is out of range " +
                "( -2 <= uidFlag <= 1 ) : " + uidFlag + "\n\n" +
                "UserIDDB.cs : " + nameof(UidReSettingCouroutine)
                );

        yield break;
    }
}

