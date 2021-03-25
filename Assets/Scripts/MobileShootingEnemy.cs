using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileShootingEnemy : ShootingEnemy    
{
    float stopRadiusSqrd = 100f;
    protected override void DoBehaviour()   //arrive behavior
    {
        float mappedSpeed = Map(distToPlayerSquared, 0, speed, 0, stopRadiusSqrd);
        _rb.velocity = new Vector2(DirectionToPlayer.x, DirectionToPlayer.y) * mappedSpeed;

        if (mappedSpeed <= 0.1f)
        {
            _rb.velocity = Vector2.zero;
            ShootBehaviour();
        }

    }
}
