using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public static bool isFiring;
    protected float fireRate;          //rate at which gun fires; number of shots persecond
    protected float gunDamageMult;            //damage the bullet does
    protected float gunShotSpeedMult =1f;        //force at which bullet flies out at
    protected float recoil =10f;             //change to curve in the future?

    public float currentAmmo;

    protected float shootBuffer = 0.2f;     //buffered time for shooting
    float shootDuration;                    //=guntime + buffer time
    protected float gunTime;            //potentially anim curve related stuff
    public GunStat gunStat;
    //change to take gun data scriptable obj in the future
    //BulletDataScrObj bulletData;
    //public GunStats gunStats;         //gun stats
    //protected BulletStats bulletStats;
    [SerializeField]
    protected GameObject bullet;       //Bullet that the gun uses    


    float nextTimeToFire;           
    protected Vector3 shootDirection = Vector3.right;     //default shoot direction faces right
   
  //  protected Quaternion gunRotation;                             //rotation for gun direction

    protected PlayerEntity player;


    //public BaseGun(GunStats gunStats)
    //{
    //    fireRate = gunStats.firerate;
    //    damage = gunStats.damage;
    //    projectileSpeed = gunStats.projectileSpeed;
    //    recoil = gunStats.recoil;
    //}
    //Vector3 lastShootPos = Vector3.zero;                       //last direction gun was facing
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        player = PlayerEntity.instance.GetComponent<PlayerEntity>();
        //bullet = gunStats.bulletStats.bulletPrefab;
        currentAmmo = gunStat.maxAmmo;
       // SetGunStats(gunStats); 
    }
    protected virtual void OnEnable()   //small hack for testing
    {
        currentAmmo = gunStat.maxAmmo;
    }
    //public void SetGunStats(GunStats _gunStat)
    //{
    //    fireRate = _gunStat.firerate;
    //    gunDamageMult = _gunStat.damageMult;
    //    gunShotSpeedMult = _gunStat.shotSpeedMult;
    //    recoil = _gunStat.recoil;
    //}
    // Update is called once per frame
    protected virtual void Update()
    {
       BufferedFiring();
    }
    protected virtual void BufferedFiring()
    {
        if(Input.GetButton("Fire1"))
        {       
            shootDuration = gunTime + shootBuffer;
            isFiring = true;
        }
        if(isFiring)
        {
            gunTime += Time.deltaTime;
            if (Time.time >= nextTimeToFire)
            {
                //   CameraManager.instance.Shake(0.3f);
                DeductAmmo();
                SpawnBullet();
                SpawnShells();
                nextTimeToFire = Time.time + (1f / gunStat.fireRate);
            }

        }
        if (!Input.GetButton("Fire1") && gunTime >= shootDuration)
        {
            isFiring = false;
            gunTime = 0;
        }
    }
    protected virtual void DeductAmmo()
    {
        if (currentAmmo > 0) currentAmmo--;
        else WeaponManager.instance.ChangeWeapon();
    }
    protected virtual void SpawnBullet()
    {
        GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.GetComponent<IBullet>().SetValue(gunStat);
        //player.GunRecoilVert(gunStat.recoil - (player.hoverTime * gunStat.fireRate));
        //player.GunRecoil(gunStat.recoil - (player.hoverTime * gunStat.fireRate));
    }
    protected virtual void SpawnShells()  //arbitrary implementation for the time being
    {
        GameObject shell = bullet.GetComponent<BaseBullet>().bulletShell;
        GameObject shellClone = Instantiate(shell, transform.position, transform.rotation);
        Vector3 shellDirection = Vector3.up - transform.right;
        shellClone.GetComponent<Rigidbody2D>().AddForce(shellDirection*4f, ForceMode2D.Impulse);
        //float dotProduct = Vector3.Dot(shootDirection, -shellDirection);
        //positive value is anti clockwise direction, negative is clockwise
        //shellClone.GetComponent<Rigidbody2D>().AddTorque(Random.value*dotProduct,ForceMode2D.Impulse);
    }


    #region unusedcode

    //void SetShootDirection()            //shoot direction based off key input/move dir
    //{
    //    float x = Input.GetAxisRaw("Horizontal");
    //    float y = Input.GetAxisRaw("Vertical"); ;
    //    shootDirection = (y < 0) ? new Vector2(0, y) : new Vector2(x, y);   //disables diagonal shooting downwards
    //    if (lastShootPos != shootDirection && shootDirection != Vector3.zero)
    //    {
    //        lastShootPos = shootDirection;
    //        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
    //        gunRotation = Quaternion.AngleAxis(angle, Vector3.forward);

    //        transform.rotation = gunRotation;
    //    }       
    //}
    void SemiAuto()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            isFiring = true;
            gunTime += Time.deltaTime;
            SpawnBullet();
            nextTimeToFire = Time.time + (1f / fireRate);
        }
    }
    protected virtual void AutoFire()
    {
        if (Input.GetButton("Fire1"))
        {
            gunTime += Time.deltaTime;
            isFiring = true;
            if (Time.time >= nextTimeToFire)
            {
                SpawnBullet();
                nextTimeToFire = Time.time + (1f / fireRate);
            }
        }
        else
        {
            gunTime = 0;
            isFiring = false;
        }
    }
    #endregion

}
