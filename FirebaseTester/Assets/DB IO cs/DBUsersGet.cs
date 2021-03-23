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

    public List<User> users;
    public DatabaseReference Reference = FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Reference;

    private void Start()
    {
        if (userGetter == null)
        {
            userGetter = this;
        }
    }

    void AddUserData(DataSnapshot snapshot)
    {
        foreach (DataSnapshot userDataSnapshot in snapshot.Children)
        {
            User userData = (User)userDataSnapshot.Value;
            users.Add(userData);
        }
    }

    public List<User> GetUsers()
    {
        users = new List<User>();

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

