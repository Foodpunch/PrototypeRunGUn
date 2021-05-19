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
        AudioManager.instance.PlaySound(AudioManager.SoundType.SHOTGUN_SHOOT,transform.position);
        // Debug.Log("BulletStats: " + gunStats.bulletStats.Debug());
        for (int i=0; i < gunStat.pelletCount; i++)
        {
            float spreadRange = Random.Range(-gunStat.spreadAngle / 2, gunStat.spreadAngle / 2);
            Quaternion randomArc = Quaternion.Euler(0, 0, spreadRange);
            GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation*randomArc);
            bulletClone.GetComponent<IBullet>().SetValue(gunStat);
            if(gunStat.isPlayerControlled)
                player.GunRecoil(gunStat.recoil - (player.hoverTime * gunStat.fireRate));
            //player.GunRecoilVert(gunStat.recoil - (player.hoverTime * gunStat.fireRate));
        }
      
    }


}
