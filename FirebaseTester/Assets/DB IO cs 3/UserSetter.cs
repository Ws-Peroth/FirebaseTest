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
        Reference = FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Reference;

    }

    public void SetterOn()
    {
        UserInputDB();
    }
    string MakeUser()
    {
        int point, x, y;
        System.Random random = new System.Random();
        point = random.Next(0, 101);
        x = random.Next(0, 101);
        y = random.Next(0, 101);
        print("point : " + point);
        print("x : " + x);
        print("y : " + y);



        UserDB user = new UserDB(point, x, y);
        string userJson = JsonUtility.ToJson(user);
        return userJson;
    }

    public void UserInputDB()
    {
        BigInteger uid = UserIDDB.userUidManager.RequestRecentUid();
        if (uid == 0) { print("Uid Get Error"); return; }
        else { print("Get Uid Complete"); }

        string userJson = MakeUser();
        print("uid = " + uid);
        print("user data" + userJson);
        Reference.Child(uid.ToString()).SetRawJsonValueAsync(userJson);
    }
}
