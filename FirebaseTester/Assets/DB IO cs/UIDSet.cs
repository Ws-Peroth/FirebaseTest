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

public class UIDSet : MonoBehaviour
{
    public DatabaseReference Reference = FirebaseDatabase.DefaultInstance.RootReference.Child("UID").Reference;
    public static UIDSet uidSetter;

    void Start()
    {
        if (uidSetter == null)
        {
            uidSetter = this;
        }
    }

    public void SetUid(int uid)
    {
        lock (uidSetter)
        {
            Reference.SetValueAsync(uid).Wait();
        }
        {
            Debug.Log("Data Input End");
        }
    }

}
