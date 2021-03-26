using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticBullet : BaseBullet
{
    //tries to calculate velocity based on player's position. 
    //Doesn't really work too well though, do not use.
    bool forceAdded;
    float distance;
    protected override void Start()
    {
        base.Start();
        bGravityInfluence = true;
        _rb.gravityScale = 1f;
    }
    protected override void Update()
    {
        base.Update();
        distance = ((Vector3)target - transform.position).magnitude;
        if (valueSet && !forceAdded)
        {
            BallisticTravelToPoint(target);
            forceAdded = true;
        }
        if (distance <= 1f) Despawn();
        //sprite rotation logic
        _sr.transform.rotation = Quaternion.FromToRotation(transform.up, _rb.velocity);
    }
    protected void BallisticTravelToPoint(Vector3 point)
    {
        Vector3 velocity = BallisticVelocity(PlayerScript.instance.transform.position, 45f);
        _rb.velocity = velocity;
    }

    protected Vector3 BallisticVelocity(Vector3 destination, float angle)
    {
        Vector3 dir = destination - transform.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist =dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height/Mathf.Tan(a); // Correction for small height differences
        float Abs = Mathf.Abs(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(Abs);
        return velocity * dir.normalized; // Return a normalized vector.
    }
    protected override void Despawn()
    {
        base.Despawn();
        VisualFXManager.i.SpawnFXType(Effects.EffectType.EXPLOSION, transform.position);
    }
}
