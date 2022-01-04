using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnTakeDamage(float damage,ContactPoint2D _contact);
}
public interface IBullet
{
    void SetValue(GunStat entityStats);
}