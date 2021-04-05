using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileStats
{
   //public enum User
   //{
   //     PLAYER,
   //     ENEMY
   //};
   // public User user;

    public float damage;
    public float speed;
    public float timeToDisappear;
    public bool gravityInfluence;
}

[System.Serializable]
public class GunStat
{
    public int maxAmmo;        
    public float fireRate;
    public float dmgMult;
    public float shotSpeedMult;
    public int pelletCount;
    public float spreadAngle;
    public float recoil;
    public bool isPlayerControlled;
}

[System.Serializable]
public class EntityStat
{
    public int maxHealth;
    public float speed;
    public bool isPlayer;
}

public class StatWrapper
{
    public ProjectileStats projectileData;
    public GunStat entityData;

    public float damage;
    public float speed;
    public StatWrapper(ProjectileStats _projectileStat, GunStat _gunStat)
    {
        projectileData = _projectileStat;
        entityData = _gunStat;
        damage = _projectileStat.damage * _gunStat.dmgMult;
        speed = _projectileStat.speed * _gunStat.shotSpeedMult;

    }
}