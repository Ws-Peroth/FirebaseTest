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
// using Firebase.Unity.Editor;
// Firebase 불러오기

public class DBIO : MonoBehaviour
{
    public DatabaseReference reference { get; set; }
    // 라이브러리를 통해 불러온 FirebaseDatabase 관련객체를 선언해서 사용

    public void Printlog(string log) { Debug.Log(log); }

    void Start()
    {
        Uri uri = new Uri("https://fireabsetest-5e0f0-default-rtdb.firebaseio.com/");
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = uri;

        Printlog(nameof(Start));

        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // 1. Init DB

        reference.Child("Uid").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.HasChildren == true)
                {
                    Debug.Log("::Defualt DB is Exists::");
                    DoFlag(1);
                }
                else
                {
                    Debug.Log("::Defualt DB is Not Exists::");
                    DoFlag(0);
                }
            }
            else
            {
                Debug.Log("::Get Value ERROR::");
                DoFlag(-1);
            }
        });
    }

    public void DoFlag(int flag)
    {
        UidInit data = new UidInit(-1);

        if (flag < 0)
        {
            Debug.Log("::ERROR::");
            return;
        }

        if (flag == 0)
        {
            // 1-2. Defualt Setting
            Debug.Log("::Defualt Setting::");
            UidInit uidInit = new UidInit(BigInteger.Parse("99999999"));
            string uidjson = JsonUtility.ToJson(uidInit);

            reference.Child("UID").SetRawJsonValueAsync(uidjson).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("::Make UidInit Is Done");
                    Debug.Log("::Key Value = " + uidInit.key + "::");
                }
                else
                    Debug.Log("::Task Is Not Finish::");
            });
        }

        if (flag >= 0)
        {
            // 2. Make User Data
            Debug.Log("::Make User Data::");


            reference.Child("UID").GetValueAsync().ContinueWith(task =>
            {
                DataSnapshot readData = task.Result;
                data = new UidInit(((UidInit)task.Result.Value).key + 1);
                Debug.Log("::Update Kew Value = "+data.key+"::");
            });

        }
        // 3. Input Data

        System.Random random = new System.Random();
        Player player = new Player(random.Next(0, 101), "Test");

        string json = JsonUtility.ToJson(player);
        string newuid = JsonUtility.ToJson(data);


        reference.Child("rank").SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("::Input Player Data Is Done::");
                Debug.Log("::Player Name = " + player.name + ", Player Point = " + player.point + "::");
            }
            else
            {
                Debug.Log("::Input Player Data Failed::");
            }
        });


        reference.Child("UID").SetRawJsonValueAsync(newuid).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("::Input Uid Data Is Done::");
            }
            else
                Debug.Log("::Input Uid Data Failed::");
        });
    }


}



class Player
{

    public BigInteger point;
    public string name;
    public Player(BigInteger point,string name)
    {
        this.point = point;
        this.name = name;
    }
}

class UidInit
{
    public UidInit(BigInteger key)
    {
        this.key = key;
    }
    public BigInteger key;
}