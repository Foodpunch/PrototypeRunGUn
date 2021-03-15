using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunStats :ScriptableObject
{
    public new string name;
    public float firerate;
    public float damage;
    public float projectileSpeed = 25f;
    public float recoil= 10f;
    public enum GunFiringType   //For searching other gun types in the future?
    {
        DEFAULT,        //single shot, linear
        SHOTGUN,        //spread on angle
        BURST,          //Sine curve
        PROJECTILE      //RB Projectiles
    }
    public GunFiringType GunType = GunFiringType.DEFAULT;
    public BulletData bullet;
}
