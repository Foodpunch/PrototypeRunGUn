using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShotgun : BaseGun
{
    //protected int pelletCount = 3; //number of bullets per shot
    //protected float spreadAngle = 25f; //angle of shotgun shot
    //float dmgPerPellet;
    protected override void Start()
    {  
        base.Start();
        //dmgPerPellet = gunStats.Damage / gunStats.pelletCount;
        //pelletCount = gunStats.pelletCount;
    }

    protected override void SpawnBullet()
    {
       // Debug.Log("BulletStats: " + gunStats.bulletStats.Debug());

        for (int i=0; i < gunStats.pelletCount; i++)
        {
            float spreadRange = Random.Range(-gunStats.spreadAngle / 2, gunStats.spreadAngle / 2);
            Quaternion randomArc = Quaternion.Euler(0, 0, spreadRange);
            GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation*randomArc);
            bulletClone.GetComponent<IBullet>().SetValue(gunStats.bulletStats,gunStats);
            player.GunRecoilVert(recoil - (player.hoverTime * fireRate));
        }
      
    }


}
