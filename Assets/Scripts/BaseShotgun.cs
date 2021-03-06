using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShotgun : BaseGun
{
    protected int pelletCount = 3; //number of bullets per shot
    protected float spreadAngle = 25f; //angle of shotgun shot


    protected override void SpawnBullet()
    {
        for(int i=0; i < pelletCount; i++)
        {
            float spreadRange = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Quaternion randomArc = Quaternion.Euler(0, 0, spreadRange);
            BaseBullet bulletClone = Instantiate(bullet, transform.position, transform.rotation*randomArc);
            bulletClone.GetComponent<BaseBullet>().SetValue(damage / pelletCount, projectileSpeed);
            player.GunRecoil(5f);
        }
    }


}
