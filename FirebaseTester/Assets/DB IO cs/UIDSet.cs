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
    public DatabaseReference Reference;
    public static UIDSet uidSetter;

    void Start()
    {
        Reference = FirebaseDatabase.DefaultInstance.RootReference.Child("UID").Reference;
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
