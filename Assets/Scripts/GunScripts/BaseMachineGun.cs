using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMachineGun : BaseGun
{    
    float magnitude = 0.3f;

    //public BaseMachineGun(GunStats gunStats):base(gunStats)       //inherit base constructor stuff
    //{

    //}
    protected override void Start()
    {
        base.Start();
    }
    protected override void SpawnBullet()
    {
        //Vector2 bulletOffset = transform.up*bulletPattern.Evaluate(gunTime);
        Vector2 bulletOffset = transform.up * Mathf.Sin(gunTime*gunStat.fireRate) * magnitude;      //fire rate = frequency of curve, magnitude = min/max value of curve
        GameObject bulletClone = Instantiate(bullet, transform.position+(Vector3)bulletOffset, transform.rotation);
        bulletClone.GetComponent<IBullet>().SetValue(gunStat);
        if (gunStat.isPlayerControlled)
            player.GunRecoilVert(gunStat.recoil-(player.hoverTime*gunStat.fireRate/2));
        //player.GunRecoilVert(gunStat.recoil);
    }
}
