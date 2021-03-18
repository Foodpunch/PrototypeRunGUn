using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnTakeDamage(float _damage);
}
public interface IBullet
{
    void SetValue(BulletStats bulletStats, GunStats gunStats);
}