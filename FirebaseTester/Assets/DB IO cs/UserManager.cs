using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager userManager;

    void Start()
    {
        if(userManager == null)
        {
            userManager = this;
        }
    }

    public DBUserData CreatUser(string name, int point)
    {
        DBUserData user = new DBUserData(name, point);
        return user;
    }

    public void InputUserToDB(DBUserData user, int key)
    {
        DBUsersSet.userSetter.SetUser(user, key);
    }
}
