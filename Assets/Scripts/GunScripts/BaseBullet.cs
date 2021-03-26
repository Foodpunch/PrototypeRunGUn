using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseBullet : MonoBehaviour,IBullet
{
    // public BulletDataScrObj bulletData;

    float bulletDamage;
    float bulletSpeed;
    float timeToDisappear; //time in seconds that the bullet should be in the air for

    protected bool bGravityInfluence = false;       //if the bullet can be influenced by gravity

    protected bool valueSet;

    float bulletAirTime;            //time bullet has been in the air for
    protected Rigidbody2D _rb;                //bullet's rigidbody
    protected SpriteRenderer _sr;

    public ProjectileStats projectileStats;
    protected Vector2 target;
    protected StatWrapper Stats;
    public GameObject bulletShell;

    //particle fx for the bullet?
    //BulletStats _stats;

    //public ParticleSystem bulletSparks;
    //public GameObject dustFX;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        timeToDisappear = projectileStats.timeToDisappear;
        _sr = this.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(valueSet)
        {
            bulletAirTime += Time.deltaTime;
            if (bulletAirTime >= timeToDisappear)
            {
                Despawn();
            }
            if(!bGravityInfluence) _rb.velocity = transform.right * Stats.speed;

            // _rb.velocity = transform.right * bulletSpeed;
            //_rb.AddForce(transform.right * bulletSpeed*Time.deltaTime, ForceMode2D.Impulse);
            //_rb.MovePosition(transform.position + transform.right * bulletSpeed*Time.deltaTime);
        }
    }
    //public virtual void SetValue(float _dmg,BulletStats _data)
    //{
    //    bulletDamage = _dmg;
    //    bulletSpeed = _data.Speed;
    //    timeToDisappear = _data.TimeToDisappear;
    //    valueSet = true;
    //}
    public virtual void SetValue(EntityStats eStats)
    {
        Stats = new StatWrapper(projectileStats, eStats);
        if (!eStats.isPlayerControlled) target = PlayerScript.instance.transform.position;
        valueSet = true;
    }
 

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Vector3 normal = contact.normal;        //getting the normal for look rot
                // Vector3 point = contact.point;          //point at which contact happens
                // Quaternion rot = Quaternion.FromToRotation(Vector3.right,normal); //so that particle looks at the opposite direction
                // ParticleSystem sparksClone = Instantiate(bulletSparks, point, rot);
                // Quaternion rot2 = Quaternion.FromToRotation(Vector3.up, normal);
                // GameObject dustClone = Instantiate(dustFX, point, rot2);
                if (contact.collider.gameObject.GetComponent<IBullet>()!= null)
                    SpawnNullifyEffect();
                if(contact.collider.gameObject.GetComponent<IDamageable>()!=null)
                {
                    contact.collider.gameObject.GetComponent<IDamageable>().OnTakeDamage(Stats, contact);
                    SpawnBulletEffects(contact);
                }
                SpawnEnvirontmentEffect(contact);
                  

                Despawn();
                return;
            }
        }   
    }

    protected virtual void Despawn()
    {
        valueSet = false;
        _rb.Sleep();
        gameObject.SetActive(false);
    }
    protected virtual void SpawnBulletEffects(ContactPoint2D contact)
    {
        VisualFXManager.i.SpawnFXType(Effects.EffectType.BULLET, contact);
      //  GameManager.instance.BulletEffectsData.SpawnAllFX(contact);
      //  bulletEffects.SpawnAllFX(contact);
    }
    protected virtual void SpawnNullifyEffect()
    {
        VisualFXManager.i.SpawnFXType(Effects.EffectType.GENERIC, transform.position);
    }
    protected virtual void SpawnEnvirontmentEffect(ContactPoint2D contact)
    {
        VisualFXManager.i.SpawnFXType(Effects.EffectType.GENERIC, contact);
    }
    
}

