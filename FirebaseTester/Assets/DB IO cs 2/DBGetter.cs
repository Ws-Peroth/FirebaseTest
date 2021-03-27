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
    public DBUser user;
    public DatabaseReference Reference;

    public void OnSignal()
    {
        Reference = FirebaseDatabase.DefaultInstance.RootReference.Reference;

        Task<bool> task = new Task<bool>( () => Flag());

        task.Start();
        Debug.Log("Func Start");
        task.Wait();

        print(task.Result ? "Return True" : "Return False");

    }

    /*public bool IsUserDbExist()
    {
        print("Call Function : " + nameof(IsUserDbExist));

        int flag = -2;

        lock (this)
        {
            Reference.Child("Users").GetValueAsync().ContinueWith(task =>
           {
               if (task.IsCompleted)
               {
                   if (task.Result.ChildrenCount > 0)
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
                   flag = -1; print("flag = -1");
               }
           });
        };

        Debug.Log("UID DB Is Exists flag = " + flag);

        if (flag == 1) return true;
        return false;

    }*/


    public bool Flag()
    {
        int flag = -2;

        Task<DataSnapshot> callData = GetDataAsync();

        Debug.Log("Wait . . .");
        callData.Wait();

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
        else{
            flag = -1;
        }

        Debug.Log("flag = " + flag);
        if (flag == 1) return true;
        return false;

    }

    private async Task<DataSnapshot> GetDataAsync()
    {
        DataSnapshot data = await Reference.Child("Users").GetValueAsync();
        
        return data;
    }

}
