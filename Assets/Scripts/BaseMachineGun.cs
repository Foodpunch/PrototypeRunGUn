﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMachineGun : BaseGun
{    
    float magnitude = 0.3f;

    protected override void SpawnBullet()
    {
        //Vector2 bulletOffset = transform.up*bulletPattern.Evaluate(gunTime);
        Vector2 bulletOffset = transform.up * Mathf.Sin(gunTime*fireRate) * magnitude;      //fire rate = frequency of curve, magnitude = min/max value of curve
        BaseBullet bulletClone = Instantiate(bullet, transform.position + (Vector3)bulletOffset, transform.rotation);
        bulletClone.GetComponent<BaseBullet>().SetValue(damage, projectileSpeed);
        player.GunRecoilVert(recoil-(player.hoverTime*fireRate));
    }
}