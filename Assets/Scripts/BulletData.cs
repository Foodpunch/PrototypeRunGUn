using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletData : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public float damage;
    public float speed;
    public float timeToDisappear;
    public float airTime;
    public Rigidbody2D rigidbody;
    public GameObject shell;

    public bool gravityInfluence = false;

    public BulletData(string name, Sprite sprite, float damage, float speed, float timeToDisappear, float airTime, Rigidbody2D rigidbody, GameObject shell, bool gravityInfluence)
    {
        this.name = name;
        this.sprite = sprite;
        this.damage = damage;
        this.speed = speed;
        this.timeToDisappear = timeToDisappear;
        this.airTime = airTime;
        this.rigidbody = rigidbody;
        this.shell = shell;
        this.gravityInfluence = gravityInfluence;
    }
}
