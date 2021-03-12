using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BulletDataList",menuName ="List of Bullet Data")]
public class BulletDataList : ScriptableObject
{
    private static BulletDataList instance;
    public static BulletDataList Instance { get => instance; set => instance = value; }


    public List<BulletData> bulletDataList;

   
}
