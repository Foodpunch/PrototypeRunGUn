using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletStats : ScriptableObject
{
    public new string name;
    //public Sprite sprite;
    [HideInInspector]
    public float damage;
    public float speed;
    public float timeToDisappear;
    //public float airTime;
    public GameObject bulletPrefab;
    public GameObject shell;
    public bool gravityInfluence = false;

  //  public float Damage { get => damage; set => damage = value; }
 //   public float Speed { get => speed; set => speed = value; }
  //  public float TimeToDisappear { get => timeToDisappear; set => timeToDisappear = value; }

    public string Debug()
    {
        return "Name: " + name + "\n" +
            "Damage: " + damage + "\n" +
            "Speed: " + speed + "\n" +
            "DisappearTime: " + timeToDisappear + "v ";
    }


    //public BulletData(string name, Sprite sprite, float damage, float speed, float timeToDisappear, float airTime, GameObject shell, bool gravityInfluence)
    //{
    //    this.name = name;
    //    this.sprite = sprite;
    //    this.damage = damage;
    //    this.speed = speed;
    //    this.timeToDisappear = timeToDisappear;
    //    this.airTime = airTime;
    //    this.shell = shell;
    //    this.gravityInfluence = gravityInfluence;
    //}
}
