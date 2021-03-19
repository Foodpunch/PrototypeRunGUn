using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseEntity : MonoBehaviour,IDamageable
{
    [SerializeField]
    protected float maxHealth;
    protected float speed =0.2f;
    protected float damage;
    float currentHealth;

    float stopRadiusSqrd = 16f;
    protected float distSqrd;
    

    protected Vector3 targetPos;
    protected Vector3 DirectionToPlayer;
    protected Rigidbody2D _rb;
    protected SpriteRenderer _sr;
    protected Collider2D _col;
    ContactPoint2D _contact;
    protected bool isDead = false;

    public event Action onDeathEvent;

    public void OnTakeDamage(float _damage,ContactPoint2D contact)
    {
        _contact = contact;
        if(!isDead)
        {
            currentHealth -= _damage;
            //if (currentHealth > 0) //play hurt sound && flashing effect
            HurtBehaviour();
            if (currentHealth <= 0f)
            {
                OnDeath();
            }
        }
        
    }
   
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        TrackPlayer();      //calculates direction and dist to player
        if (!isDead) DoBehaviour();
        
    }
    public virtual void OnDeath()
    {
        CameraManager.instance.Shake(0.2f);

        VisualFXManager.i.SpawnFXType(Effects.EffectType.EXPLOSION, transform.position);
        _col.enabled = false;
        _rb.gravityScale = 1f;
       // _rb.Sleep();
        isDead = true;
        //play whatever anim needs to be played

    }
    protected void TrackPlayer()
    {
        targetPos = PlayerScript.instance.transform.position;       //tracking the player pos
        DirectionToPlayer = (targetPos - transform.position);
        distSqrd = DirectionToPlayer.sqrMagnitude;   //sqr root is costly
    }
    protected virtual void HurtBehaviour()     
    {

    }
    protected virtual void DoBehaviour()
    {
        float f = Map(distSqrd, 0, speed, 0, stopRadiusSqrd);
        _rb.velocity = new Vector2(0, DirectionToPlayer.y)*f;
        //if(f <=0.015f)
        //{
        //    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stopRadiusSqrd / 2);
        //    foreach (Collider2D obj in colliders)
        //    {
        //        if (obj.GetComponent<IDamageable>() != null)
        //        {
        //            //bj.GetComponent<IDamageable>().OnTakeDamage(damage, 0, transform.position, false);
        //        }
        //        Rigidbody2D otherRBs = obj.GetComponent<Rigidbody2D>();
        //        if (otherRBs != null)
        //        {
        //            Vector3 dir = (obj.transform.position - transform.position).normalized;
        //            Vector2 dir2D = new Vector2(dir.x, dir.y);
        //            otherRBs.AddTorque(0.1f);
        //            otherRBs.AddForce(dir2D * damage * 10f,ForceMode2D.Impulse);
        //        }
        //    }
        //    if(currentHealth > 0)
        //    {
        //        OnDeath();
        //    }
        //}
    }

    #region Map explanation
    //how map works is that value will be "clamped" according to the speed and range.
    //so in this case, if the distance is more than the range, f = max speed;
    //if the distance is < 0, the speed will also be 0
    //but if not, (meaning it's > 0 but < range) f = slower speed the smaller the range is.
    #endregion
    public float Map(float value, float from, float to, float from2, float to2)
    {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        else
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }
  
}
