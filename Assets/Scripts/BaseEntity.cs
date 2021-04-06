using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseEntity : MonoBehaviour,IDamageable
{
    [SerializeField]
    protected EntityStat entityStat;
    protected float currentHealth;

    protected Rigidbody2D _rb;
    protected SpriteRenderer _sr;
    protected Collider2D _col;
    ContactPoint2D _contact;
    [SerializeField]
    protected bool isDead = false;
    [SerializeField]
    protected bool isHurt;                  //AI will not have i-frames. This is for checking when to flash the sprite.
    [SerializeField]
    protected Material[] hurtMaterial;      //0 black, 1 white could probably move this else where for nicer reference
    [SerializeField]
    Material defaultMaterial;
    float flashTime;
    protected float hurtFlashDuration = .1f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
        currentHealth = entityStat.maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
      //  TrackPlayer();      //calculates direction and dist to player
        if (!isDead) DoBehaviour();
        if (isHurt &&!isDead) DamageFlash();      //Move this to visuals update later

    }
    public virtual void OnDeath()
    {
        _sr.material = defaultMaterial;
        CameraManager.instance.Shake(0.2f);
        VisualFXManager.i.SpawnFXType(Effects.EffectType.EXPLOSION, transform.position);
        _rb.constraints = RigidbodyConstraints2D.None;
        _col.enabled = false;
     //   CameraManager.instance.ripple.Emit(transform.position);
        _rb.gravityScale = 1f;
        isDead = true;
        //play whatever anim needs to be played

    }
 
    protected virtual void HurtBehaviour()     
    {
        //logic when AI gets hurt
       //Should set a bool to flash renderer here.
    }
    protected virtual void DoBehaviour()
    {
        //logic for AI to execute
    }
    protected virtual void DamageFlash()
    {
        flashTime += Time.deltaTime/2f;
        if (flashTime < hurtFlashDuration)
        {
            float x = flashTime * 10f;
            _sr.material = hurtMaterial[Mathf.RoundToInt(x) % 2];
        }
        else
        {
            flashTime = 0f;
            isHurt = false;
            _sr.material = defaultMaterial;
        }
        //else
        //isHurt = false;
       // _sr.material = defaultMaterial;
    }

    public void OnTakeDamage(StatWrapper Stats, ContactPoint2D contact)
    {
        _contact = contact;
        if (!isDead)
        {
            isHurt = true;
            currentHealth -= Stats.damage;
            HurtBehaviour();
            if (currentHealth <= 0f)
            {
                OnDeath();
            }
        }
    }


   
  
}
