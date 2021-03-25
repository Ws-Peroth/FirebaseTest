using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDManager : MonoBehaviour
{
    public static UIDManager uidManager;
    public int uid = 0;

    private void Start()
    {

        if (uidManager == null)
        {
            uidManager = this;
        }
    }

    public bool InitUidDB()
    {
        print(nameof(InitUidDB));

        if (!UidDatabaseState())
        {
            FixUid(100000000);
            return true; // Init 완료
        }

        return false; // 이미 Init되어있음
    }

    public bool UidDatabaseState()
    {
        int state = UIDGet.uidGetter.UidDbExistsCheck();

        switch (state)
        {
            case 1:
                Debug.Log("Uid DB is Exists");
                return true;
            case -2:
                Debug.Log("ERROR : flag is not changed");
                return false;
            case -1:
                Debug.Log("ERROR : Get Uid Database Failed");
                return false;
            case 0:
                Debug.Log("Uid DB is Not Exixts");
                return false;
            default:
                Debug.Log("Flag Value Error");
                return false;
        }
    }

    public int FixUid(int uid)
    {
        this.uid = uid;
        return this.uid;
    }

    public int GetNextUid()
    {
        uid = UIDGet.uidGetter.GetUid();
        return uid + 1;
    }

    public int GetNowUid()
    {
        return UIDGet.uidGetter.GetUid();
    }

    public void SetNextUid()
    {
        uid = UIDGet.uidGetter.GetUid() + 1;
    }

    public void UpdateNowUid()
    {
        UIDSet.uidSetter.SetUid(uid);
    }

    public void UpdateNextUid()
    {
        UIDSet.uidSetter.SetUid(uid + 1);
    }

}
