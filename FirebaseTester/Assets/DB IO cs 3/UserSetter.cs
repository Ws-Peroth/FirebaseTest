using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;

public class UserSetter : MonoBehaviour
{

    DatabaseReference Reference;

    private void Start()
    {
        Reference = FirebaseDatabase.DefaultInstance.RootReference.Reference;

    }

    public void SetterOn()
    {
        MakeUser();
    }


    UserDB MakeUser()
    {
        int point, x, y;
        System.Random random = new System.Random();
        point = random.Next(0, 101);
        x = random.Next(0, 101);
        y = random.Next(0, 101);
        UserDB user = new UserDB(point, x, y);
        return user;
    }
    
}
