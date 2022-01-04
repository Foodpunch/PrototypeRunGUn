using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : BaseEntity
{
    //The most basic implementation of an enemy. 
    //HP system, touch dmg to player, immobile.
    protected float distToPlayerSquared;
    protected Vector3 targetPos;
    protected Vector3 DirectionToPlayer;
    Vector3 spawnPos;           //position where AI spawned

    protected override void Start()
    {
        base.Start();       //get component calls and init stuff
        spawnPos = transform.position;
    }
    protected override void DoBehaviour()
    {
        TrackPlayer();
        ReturnToSpawnPoint();
    }
    protected void TrackPlayer()
    {
        targetPos = PlayerEntity.instance.transform.position;       //tracking the player pos
        DirectionToPlayer = (targetPos - transform.position);
        distToPlayerSquared = DirectionToPlayer.sqrMagnitude;   //sqr root is costly
    }
    protected void ReturnToSpawnPoint()
    {
        float distToSpawnSquared = (transform.position - spawnPos).sqrMagnitude;
        if (distToPlayerSquared > 10f)
        {
            _rb.velocity = (spawnPos - transform.position) * entityStat.speed;
        }

    }
}
