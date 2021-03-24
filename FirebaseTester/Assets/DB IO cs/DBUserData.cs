using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBUserData
{
    public string name;
    public int point;
    public int uid;

    public DBUserData(string name, int point, int uid)
    {
        this.name = name;
        this.point = point;
        this.uid = uid;

    }
}

