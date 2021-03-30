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

public class DBGetter : MonoBehaviour
{
    DBUser user;
    DatabaseReference Reference;

    public delegate Task<DataSnapshot> FlagCheck();

    public void Start()
    {
        Reference = FirebaseDatabase.DefaultInstance.RootReference.Reference;
    }

    public void OnSignal()
    {
        Task<bool> task = new Task<bool>(() => Flag());

        task.Start();
        Debug.Log("Func Start");
        task.Wait();

        print(task.Result ? "Return True" : "Return False");

    }

    public bool Flag()
    {
        int flag = -2;

        //Task<DataSnapshot> callData = GetDataAsync();

        FlagCheck check = new FlagCheck(GetDataAsync);

        Task<DataSnapshot> callData = check();

        if (callData.IsCompleted)
        {
            if (callData.Result.ChildrenCount > 0)
            {
                flag = 1;
                print("flag = 1");
            }
            else
            {
                flag = 0; print("flag = 0");
            }
        }
        else
        {
            flag = -1;
        }

        Debug.Log("flag = " + flag);
        if (flag == 1) return true;
        return false;
    }

    private async Task<DataSnapshot> GetDataAsync()
    {
        DataSnapshot data = await Reference.Child("Users").GetValueAsync();
        Debug.Log(data.ChildrenCount);
        return data;
    }




}
