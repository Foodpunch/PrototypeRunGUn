using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseBullet : MonoBehaviour
{
    public BulletData bulletData;

    float bulletDamage;
    float bulletSpeed;
    float timeToDisappear; //time in seconds that the bullet should be in the air for

    bool bGravityInfluence = false;       //if the bullet can be influenced by gravity

    bool valueSet;

    float bulletAirTime;            //time bullet has been in the air for
    Rigidbody2D _rb;                //bullet's rigidbody

    //particle fx for the bullet?
    public ParticleSystem bulletSparks;

   
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
            //_rb.AddForce(transform.right * bulletSpeed*Time.deltaTime, ForceMode2D.Impulse);
            //_rb.MovePosition(transform.position + transform.right * bulletSpeed*Time.deltaTime);
        }
    }
    public virtual void SetValue(float _dmg,float _speed, float _time = 5f)
    {
        bulletDamage = _dmg;
        bulletSpeed = _speed;
        timeToDisappear = _time;
        valueSet = true;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector3 normal = contact.normal;        //getting the normal for look rot
                Vector3 point = contact.point;          //point at which contact happens\
                Quaternion rot = Quaternion.FromToRotation(Vector3.right,normal); //so that particle looks at the opposite direction
                ParticleSystem sparksClone = Instantiate(bulletSparks, point, rot);
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
    
    
}
[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class BulletData : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public float damage;
    public float speed;
    public float time;

    public bool gravityInfluence = false;

}
