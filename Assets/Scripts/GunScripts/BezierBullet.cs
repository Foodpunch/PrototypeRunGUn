using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierBullet : BaseBullet
{
    //Works by plotting a mid point between player and AI at a set angle.
    //with a midpoint set, a bezier curve can be made for the projectile path.
    //once the projectile hits the endpoint, it explodes.

    Vector2 midPoint;
    Vector2 startPos;
    float t;
    bool set;
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
       
        bGravityInfluence = true;
        _rb.gravityScale = 1f;
    }
    protected override void Update()
    {
        base.Update();
        if(valueSet)
        {
            if(!set)
            {
                midPoint = CalculateMidPointByAngle(45f) + startPos;
                set = true;
            }
            float distance = (target - startPos).magnitude;
            t += Time.deltaTime /(distance/Stats.speed);
        }
        if (t >= 1f) Despawn();
        transform.position = DoBezier(startPos, midPoint, target, t);
        _sr.transform.rotation = Quaternion.FromToRotation(transform.right, DoBezier(startPos, midPoint, target, t)*2f);
    }
    public void SetBulletDestination(Vector3 point)
    {
        target = point;
        valueSet = true;
    }
    protected Vector2 CalculateMidPointByAngle(float angle)
    {
        Vector2 dir = target - (Vector2)startPos;
        float a = angle * Mathf.Deg2Rad;
        float distance = dir.magnitude;

        dir.y =distance* Mathf.Tan(a);
        dir.Normalize();
        dir *= distance / 2f;
        return dir;
    }
    protected Vector2 DoBezier(Vector2 start, Vector2 mid, Vector2 end,float t)
    {
        Vector2 Line1 = Vector2.Lerp(start, mid, t);
        Vector2 Line2 = Vector2.Lerp(mid, end, t);
        Vector2 Final = Vector2.Lerp(Line1, Line2, t);
        return Final;
    }
   
    protected override void Despawn()
    {
        base.Despawn();
        VisualFXManager.i.SpawnFXType(Effects.EffectType.EXPLOSION, transform.position);
        //probably might wanna do some addforce logic over here
        //loop through all objects, get their pos, minus this pos to get dir
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach(Collider2D col in hitColliders)
        {
            Vector2 forceDirection = col.transform.position - this.transform.position+ Vector3.up;
           // forceDirection.Normalize();
            if(col.GetComponent<Rigidbody2D>()!=null)
            {
                col.GetComponent<Rigidbody2D>().AddForce(forceDirection * 5f, ForceMode2D.Impulse);
            }
        }

    }
}
