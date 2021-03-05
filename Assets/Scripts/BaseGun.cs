using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    protected float fireRate =4f;          //rate at which gun fires; number of shots persecond
    protected bool bAutofire = true;           //true = automatic; false = semi-auto
    protected float damage;            //damage the bullet does
    protected float projectileSpeed = 25f;        //force at which bullet flies out at
    protected float recoil;             //change to curve in the future?

    protected float gunTime;            //potentially anim curve related stuff
    [SerializeField]
    AnimationCurve testCurve;

    //change to take gun data scriptable obj in the future

    public BaseBullet bullet;       //Bullet that the gun uses
    


    float nextTimeToFire;           
    Vector3 shootDirection = Vector3.right;     //default shoot direction faces right
    Vector3 lastShootPos = Vector3.zero;                       //last direction gun was facing
    Quaternion gunRotation;                             //rotation for gun direction
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        SetShootDirection();
        if (bAutofire)
        {
            AutoFire();
        }
        else SemiAuto();

    }


    protected virtual void AutoFire()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            gunTime += Time.deltaTime;
            SpawnBullet();
            nextTimeToFire = Time.time + (1f / fireRate);
        }
        gunTime = 0f;       //reset gunTime;
    }
    protected virtual void SemiAuto()
    {

    }
    protected virtual void SpawnBullet()
    {
        BaseBullet bulletClone = Instantiate(bullet, transform.position, gunRotation);
        bulletClone.GetComponent<BaseBullet>().SetValue(damage, projectileSpeed);
    }



    void SetShootDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical"); ;
        shootDirection = (y < 0) ? new Vector2(0, y) : new Vector2(x, y);   //disables diagonal shooting downwards
        if (lastShootPos != shootDirection && shootDirection != Vector3.zero)
        {
            lastShootPos = shootDirection;
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            gunRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = gunRotation;
        }       
    }
}
