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

public class DBUsersSet : MonoBehaviour
{
    public static DBUsersSet userSetter;

    public DatabaseReference Reference = FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Reference;
    // Start is called before the first frame update
    void Start()
    {
        if(userSetter == null)
        {
            userSetter = this;
        }
    }

    public void SetUser(DBUserData userData, int key)
    {
        string userJson = JsonUtility.ToJson(userData);

        lock (userSetter)
        {
            Reference.Child(key.ToString()).SetRawJsonValueAsync(userJson).Wait();
        }
        {
            Debug.Log("Data Input End");
        }
    }
    
}
