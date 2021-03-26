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


public class TaskTest : MonoBehaviour
{
    public void TaskStart()
    {
        Debug.Log("Call Start");
        Task<int> task = new Task<int>(() => this.TaskProcess(5));

        task.Start();  // 다른 스레드에서 task 실행

        print("대기중");

        task.Wait();   // task가 종료될때까지 대기]

        Debug.Log("task Result = " + task.Result);
    }

    public int TaskProcess(int countNumer)
    {
        print("start Process");

        for(int i = 0; i <= countNumer; i++)
        {
            print("Count : " + i);  // 0 부터 countNumer까지 수를 출력하는 프로그램
        }

        print("End Process");
        return countNumer;
    }
}

