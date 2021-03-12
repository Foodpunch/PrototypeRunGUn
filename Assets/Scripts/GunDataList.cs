using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunDataList : ScriptableObject
{
    private static GunDataList instance;
    public static GunDataList Instance { get => instance; set => instance = value; }

    public List<GunData> gunDataList;

    public GunData GetGunByName(string name)
    {
        for(int i=0; i<gunDataList.Count;i++)
        {
            if(gunDataList[i].name == name)
            {
                return gunDataList[i];
            }
        }
        throw new System.Exception("No such gun exists");
    }


}
