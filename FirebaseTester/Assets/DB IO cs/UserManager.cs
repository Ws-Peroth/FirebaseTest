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

    public DBUserData CreatUser(string name, int point, int uid)
    {
        DBUserData user = new DBUserData(name, point, uid);
        return user;
    }

    public void InputUserToDB(DBUserData user, int key)
    {
        DBUsersSet.userSetter.SetUser(user, key);
    }

    public bool UserDatabaseState()
    {
        int state = UIDGet.uidGetter.UidDbExistsCheck();

        switch (state)
        {
            case 1:
                Debug.Log("User DB is Exists");
                return true;
            case -2:
                Debug.Log("ERROR : flag is not changed");
                return false;
            case -1:
                Debug.Log("ERROR : Get User Database Failed");
                return false;
            case 0:
                Debug.Log("User DB is Not Exixts");
                return false;
            default:
                Debug.Log("Flag Value Error");
                return false;
        }
    }

    public List<DBUserData> GetUserList()
    {
        if (!UserDatabaseState())
        {
            Debug.Log("User DB is Null");
        }
        else
        {
            Debug.Log("User DB call Complete");
            return DBUsersGet.userGetter.GetUsers();
        }

        return null;
    }

}
