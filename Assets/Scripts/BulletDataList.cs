using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BulletDataList",menuName ="List of Bullet Data")]
public class BulletDataList : ScriptableObject
{
    private static BulletDataList instance;
    public static BulletDataList Instance { get => instance; set => instance = value; }


    public List<BulletStats> bulletDataList;

    public BulletStats GetByName(string name)
    {
        for(int i = 0; i< bulletDataList.Count; i++)
        {
            if(name == bulletDataList[i].name)
            {
                return bulletDataList[i];
            }
        }
        throw new System.Exception("No such bullet name exists!");
    }
   
    public BulletStats GetByType(BulletStats bullet)
    {
        for (int i = 0; i < bulletDataList.Count; i++)
        {
            if (bullet.Equals(bulletDataList[i]))
            {
                return bulletDataList[i];
            }
        }
        throw new System.Exception("No such bullet type exists!");
    }
}
