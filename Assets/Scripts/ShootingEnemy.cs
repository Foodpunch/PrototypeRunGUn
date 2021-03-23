using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : BaseEntity
{
    float stopRadiusSqrd = 10f;

    float shootTime;
    float nextTimeToFire;
    bool isFiring;
    protected override void DoBehaviour()
    {
        base.DoBehaviour();

        float mappedSpeed = Map(distSqrd, 0, speed, 0, stopRadiusSqrd);
        _rb.velocity = new Vector2(DirectionToPlayer.x, DirectionToPlayer.y) * mappedSpeed;

        if(mappedSpeed <= 0.1f)
        {
            _rb.velocity = Vector2.zero;
            ShootBehaviour();
        }

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

    private void ShootBehaviour()
    {
        shootTime += Time.deltaTime;
        if(Time.time >= nextTimeToFire)
        {
            SpawnBullet();
            nextTimeToFire = Time.time + (1f / entityStats.fireRate);
        }
       
    }

    private void SpawnBullet()
    {
        Vector2 predictedPos = PlayerScript.instance.playerDirection * 2f;
        Quaternion rot = Quaternion.FromToRotation(transform.right, DirectionToPlayer+(Vector3)predictedPos);
        GameObject bulletClone = Instantiate(projectile, transform.position,rot);
        bulletClone.GetComponent<IBullet>().SetValue(entityStats);

    }
}
