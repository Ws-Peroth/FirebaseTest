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


public class DBUsersGet : MonoBehaviour
{
    public static DBUsersGet userGetter;

    public List<DBUserData> users;
    public DatabaseReference Reference;

    private void Start()
    {
        if (userGetter == null)
        {
            userGetter = this;
        }
        Reference = FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Reference;
    }

    public int UserDbExistsCheck()
    {
        int flag = -2;

        lock (userGetter)
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
            Debug.Log("lock On : User DB Is Exists");
        }
        Debug.Log("User DB Is Exists flag = " + flag);
        return flag;
    }


    void AddUserData(DataSnapshot snapshot)
    {
        foreach (DataSnapshot userDataSnapshot in snapshot.Children)
        {
            DBUserData userData = (DBUserData)userDataSnapshot.Value;
            users.Add(userData);
        }
    }

    public List<DBUserData> GetUsers()
    {
        users = new List<DBUserData>();

        lock (userGetter)
        {
            Reference.GetValueAsync().ContinueWith(task =>
            {

                if (task.IsCompleted)
                { // 성공적으로 데이터를 가져왔으면
                    DataSnapshot snapshot = task.Result;
                    AddUserData(snapshot);
                    Debug.Log("Add Process End");
                }
            });
        }
        { Debug.Log("lock On"); }

        Debug.Log("Get Data Return");
        
        return users;
    }
}

