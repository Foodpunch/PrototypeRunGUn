using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : BaseEntity
{
  
    float entityRange = 900f;
    float maxTimeDelayOffset = 2f;
    float nextTimeToFire;
    Vector3 spawnPos;           //position where AI spawned
    protected override void Start()
    {
        base.Start();
        spawnPos = transform.position;
    }
    protected override void DoBehaviour()
    {
        if (distToPlayerSquared < entityRange)
            ShootBehaviour();
            //ShootGravityBullet();
        ReturnToSpawnPoint();
       
        //if(f <=0.015f)
        //{
        //    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stopRadiusSqrd / 2);
        //    foreach (Collider2D obj in colliders)
        //    {
        //        if (obj.GetComponent<IDamageable>() != null)
        //        {
        //            //bj.GetComponent<IDamageable>().OnTakeDamage(damage, 0, transform.position, false);
        //        }
        //        Rigidbody2D otherRBs = obj.GetComponent<Rigidbody2D>();
        //        if (otherRBs != null)
        //        {
        //            Vector3 dir = (obj.transform.position - transform.position).normalized;
        //            Vector2 dir2D = new Vector2(dir.x, dir.y);
        //            otherRBs.AddTorque(0.1f);
        //            otherRBs.AddForce(dir2D * damage * 10f,ForceMode2D.Impulse);
        //        }
        //    }
        //    if(currentHealth > 0)
        //    {
        //        OnDeath();
        //    }
        //}
    }
    protected void ReturnToSpawnPoint()
    {
        float distToSpawnSquared = (transform.position - spawnPos).sqrMagnitude;
        if (distToPlayerSquared > 10f)
        {
            _rb.velocity = (spawnPos - transform.position)*speed;
        }
   
    }

    protected void ShootBehaviour()
    {
        if(Time.time >= nextTimeToFire + UnityEngine.Random.Range(0, maxTimeDelayOffset))
        {
            float randomTimeOffset = UnityEngine.Random.Range(0, maxTimeDelayOffset);
            //SpawnBullet();
            ShootGravityBullet();
            nextTimeToFire = Time.time + (1f / entityStats.fireRate + randomTimeOffset);
        }
    }
    protected void ShootGravityBullet()
    {
        Vector2 predictedPos = PlayerScript.instance.playerDirection * UnityEngine.Random.Range(0, maxTimeDelayOffset);
        GameObject bulletClone = Instantiate(projectile, transform.position, Quaternion.identity);
        bulletClone.GetComponent<IBullet>().SetValue(entityStats);
    }

    private void SpawnBullet()
    {
        //AI tries to predict player's position randomly. In future, tweak to make it less accurate the higher the player velocity is.
        //and make it more accurate if the player is slow. (to encourage player movement)
        Vector2 predictedPos = PlayerScript.instance.playerDirection * UnityEngine.Random.Range(0, maxTimeDelayOffset);
        Quaternion rot = Quaternion.FromToRotation(transform.right, DirectionToPlayer+(Vector3)predictedPos);
        GameObject bulletClone = Instantiate(projectile, transform.position,rot);
        bulletClone.GetComponent<IBullet>().SetValue(entityStats);

    }
}
