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
public class EntityStats
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

public class StatWrapper
{
    public ProjectileStats projectileData;
    public EntityStats entityData;

    public float damage;
    public float speed;
    public enum Entity
    {
        PLAYER,
        ENEMY
    };
    public Entity entity;
    public StatWrapper(ProjectileStats projectileStat, EntityStats entityStats)
    {
        projectileData = projectileStat;
        entityData = entityStats;
        damage = projectileStat.damage * entityStats.dmgMult;
        speed = projectileStat.speed * entityStats.shotSpeedMult;
        if(entityStats.isPlayerControlled)
        {
            entity = Entity.PLAYER;
        }
        else
        {
            entity = Entity.ENEMY;
        }
    }
}