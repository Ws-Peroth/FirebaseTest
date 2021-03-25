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

public class DBClassManager : MonoBehaviour
{
    public static DBClassManager classManager;

    private void Start()
    {
        if(classManager == null)
        {
            classManager = this;
        }
    }

    public void MakeUserButtonDown()
    {
        print(nameof(MakeUserButtonDown));

        int uid = SetUid();

        System.Random random = new System.Random();

        DBUserData user = UserManager.userManager.CreatUser("TestUser", random.Next(0, 101), uid);

        UserManager.userManager.InputUserToDB(user, uid);
    }

    private int SetUid()
    {
        print(nameof(SetUid));

        if (!UIDManager.uidManager.InitUidDB())
        {
            return UIDManager.uidManager.GetNowUid();
        }
        else
        {
            return UIDManager.uidManager.GetNextUid();
        }
    }

    public void GetUserButtonDown()
    {
        if (UserManager.userManager.UserDatabaseState())
        {
            List<DBUserData> users = UserManager.userManager.GetUserList();

            for (int i = 0; i < users.Count; i++)
            {
                Debug.Log("UID : " + users[i].uid + "\nName : " + users[i].name + "\nPoint : " + users[i].point);
            }
        }
    }

}