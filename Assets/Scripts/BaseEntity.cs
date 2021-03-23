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
    [SerializeField]
    protected GameObject projectile;

    [SerializeField]
    protected EntityStats entityStats;

    protected float distSqrd;
    

    protected Vector3 targetPos;
    protected Vector3 DirectionToPlayer;
    protected Rigidbody2D _rb;
    protected SpriteRenderer _sr;
    protected Collider2D _col;
    ContactPoint2D _contact;
    protected bool isDead = false;

    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
        GameManager.instance.onEnemyDeathEvent += OnDeath;
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
        CameraManager.instance.ripple.Emit(transform.position);
        _rb.gravityScale = 1f;
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
        //logic when AI gets hurt

    }
    protected virtual void DoBehaviour()
    {
        //logic for AI to execute
    }


    public void OnTakeDamage(StatWrapper Stats, ContactPoint2D contact)
    {
        _contact = contact;
        if (!isDead)
        {
            currentHealth -= Stats.damage;
            HurtBehaviour();
            if (currentHealth <= 0f)
            {
                OnDeath();
            }
        }
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
