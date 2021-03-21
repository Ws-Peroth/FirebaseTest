using System;
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

    void Start()
    {
        Uri uri = new Uri("https://fireabsetest-5e0f0-default-rtdb.firebaseio.com/");
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = uri;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        // 데이터베이스 경로를 설정해 인스턴스를 초기화
        // Database의 특정지점을 가리킬 수 있는데, 그 중 RootReference를 가 리킴

        User rank2 = new User("TestUser2");

        string json2 = JsonUtility.ToJson(rank2);
        // 데이터를 json형태로 반환

        string key2 = "uid";    //reference.Child("rank").Push().Key;

        // firebase에 key값은 0 신인용, 1 황제리, 2 김젤리.
        // root의 자식 rank에 key 값을 추가해주는 것임

        //reference.Child("rank").Child(key).SetRawJsonValueAsync(json);
        reference.Child("rank").Child(key2).SetRawJsonValueAsync(json2);
        // 생성된 키의 자식으로 json데이터를 삽입
    } 
}

public class User
{
    public string name;

    public User(string name)
    {
        this.name = name;
    }
}