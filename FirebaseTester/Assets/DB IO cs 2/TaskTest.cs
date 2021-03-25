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
    public void Start()
    {
        Debug.Log("Call Start");
        var task1 = new Task<BigInteger>(() => this.Calculate(5));

        task1.Start();  // 다른 쓰레드에서 task1 실행

        print("대기중");

        task1.Wait();   // task1이 종료될때까지 대기

        Debug.Log("Task1 : " + task1.Result);
    }


    public BigInteger Calculate(int p)
    {
        print("call Calculate");
        if (p <= 0)
        {
            throw new InvalidOperationException("Can not calculate by input data.");
        }

        BigInteger n = 1;

        for (var i = 1; i <= p; i++)
        {
            n *= i;
            print("Calc n = " + n);
            Thread.Sleep(500);
        }
        print("Calc End");
        return n;
    }

}

