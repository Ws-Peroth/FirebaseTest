using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
// using Firebase.Unity.Editor;
// Firebase 불러오기

public class GameManager : MonoBehaviour
{


    public DatabaseReference reference { get; set; }
    // 라이브러리를 통해 불러온 FirebaseDatabase 관련객체를 선언해서 사용

    public void Printlog(string log)
    {
        Debug.Log(log);
    }

    void Start()
    {
        Printlog(nameof(Start));

        Uri uri = new Uri("https://fireabsetest-5e0f0-default-rtdb.firebaseio.com/");
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = uri;

        InitKeyCode();
    }

    public BigInteger InitKeyCode()
    {
        Printlog(nameof(InitKeyCode));
        BigInteger startKeyCode = 0;
        reference = FirebaseDatabase.DefaultInstance.GetReference("UID");
        UidKey defaultkey = new UidKey("99999999");
        string defaultkeyjson = JsonUtility.ToJson(defaultkey);

        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.HasChildren == true)
                {
                    Debug.Log("Defualt DB is Exists");
                    InputData(
                        MakeUserData(), 
                        UpdateKeyCode( GetRecentKeycode(snapshot) )
                    );
                }
                else
                {
                    Debug.Log("Defualt DB is Not Exists");
                    startKeyCode = BigInteger.Parse("99999999");
                    reference.SetRawJsonValueAsync(defaultkeyjson);
                    InputData(MakeUserData(), UpdateKeyCode(startKeyCode));

                }
            }
            else
                Debug.Log("Get Value ERROR");
        });

        return startKeyCode;

    }

    public User MakeUserData()
    {
        Printlog(nameof(MakeUserData));
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        System.Random rand = new System.Random();
        User rank = new User("TestUser", rand.Next(0, 101));

        return rank;
    }

    public BigInteger UpdateKeyCode(BigInteger bigInteger)
    {
        Printlog(nameof(UpdateKeyCode));
        return bigInteger + 1;
    }

    public BigInteger GetRecentKeycode(DataSnapshot snapshot)
    {
        Printlog(nameof(GetRecentKeycode));
        BigInteger startKeyCode;

        foreach (DataSnapshot uidkeycode in snapshot.Children)
        {
            UidKey dataDictionary = (UidKey)uidkeycode.Value;
            startKeyCode = BigInteger.Parse(dataDictionary.uidKey);
            Debug.Log("Keycode Setting Finish\nKeycode : " + startKeyCode);
        }

        return startKeyCode;

    }

    public void InputData(User rank, BigInteger setKeyCode)
    {
        Printlog(nameof(InputData));
        UidKey updateKey = new UidKey(setKeyCode.ToString());
        string updateKeyJson = JsonUtility.ToJson(updateKey);
        string json = JsonUtility.ToJson(rank);
        // 데이터를 json형태로 반환

        reference.Child("rank").Child(setKeyCode.ToString()).SetRawJsonValueAsync(json);
        reference.Child("UID").SetRawJsonValueAsync(updateKeyJson);
        // 생성된 키의 자식으로 json데이터를 삽입

        PrintData();
    }

    public void PrintData()
    {
        Printlog(nameof(PrintData));
        reference = FirebaseDatabase.DefaultInstance.GetReference("rank");

        reference.GetValueAsync().ContinueWith(task => {

            if (task.IsCompleted)
            { // 성공적으로 데이터를 가져왔으면
                DataSnapshot snapshot = task.Result;

                // 데이터를 출력하고자 할때는 Snapshot 객체 사용함

                foreach (DataSnapshot users in snapshot.Children)
                {
                    IDictionary dataDictionary = (IDictionary)users.Value;
                    Debug.Log("UID = " + users.Key + "\n이름 : " + dataDictionary["name"] + ", 점수 : " + dataDictionary["point"]);
                }

                Debug.Log("Precess End");

            }
        });

    }





}

public class UidKey
{
    public string uidKey;

    public UidKey(string uid)
    {
        uidKey = uid;
    }
}
public class User
{
    public string name;
    public int point;
    public User(string name, int point)
    {
        this.name = name;
        this.point = point;
    }
}