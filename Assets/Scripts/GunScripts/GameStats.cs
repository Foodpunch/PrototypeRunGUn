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
    public float damage;
    public float shotSpeed;
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

