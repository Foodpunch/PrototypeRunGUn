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

    bool bGravityInfluence = false;       //if the bullet can be influenced by gravity

    bool valueSet;

    float bulletAirTime;            //time bullet has been in the air for
    Rigidbody2D _rb;                //bullet's rigidbody

    //particle fx for the bullet?
    //BulletStats _stats;

    //public ParticleSystem bulletSparks;
    //public GameObject dustFX;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
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
            _rb.velocity = transform.right * bulletSpeed;
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
    public virtual void SetValue(BulletStats _bulletStats,GunStats _gunStats)
    {
        bulletDamage = _bulletStats.damage * _gunStats.damageMult;
        timeToDisappear = _bulletStats.timeToDisappear;
        bulletSpeed = _bulletStats.speed * _gunStats.shotSpeedMult;
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
                if(contact.collider.gameObject.GetComponent<IDamageable>()!=null)
                contact.collider.gameObject.GetComponent<IDamageable>().OnTakeDamage(bulletDamage,contact);
                SpawnBulletEffects(contact);

                Despawn();
                break;
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
    
}

