using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDB : MonoBehaviour
{
    int point { get; set; }
    int x { get; set; }
    int y { get; set; }

    public UserDB(int point, int x,int y)
    {
        this.point = point;
        this.x = x;
        this.y = y;
    }

}
