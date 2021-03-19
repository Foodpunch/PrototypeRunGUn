using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunStats :ScriptableObject
{
    public new string name;
    public int ammo;
    public float firerate;
    public float damageMult;
    public float shotSpeedMult;
    public int pelletCount;
    public float spreadAngle;
    public float recoil;
    public enum GunFiringType   //For searching other gun types in the future?
    {
        DEFAULT,        //single shot, linear
        SHOTGUN,        //spread on angle
        BURST,          //Sine curve
        PROJECTILE      //RB Projectiles
    }
    public GunFiringType GunType = GunFiringType.DEFAULT;
    public BulletStats bulletStats;
    public string Debug()
    {
        return "Name: " + name + "\n" +
            "Firerate: " + firerate + "\n" +
            "Damage: " + damageMult + "\n" +
            "ProjectileSpeed: " + shotSpeedMult + "\n" +
            "PelletCount: " + pelletCount + "\n" +
            "Spread Angle: " + spreadAngle + "\n" +
            "Recoil" + recoil + "";
    }
}
