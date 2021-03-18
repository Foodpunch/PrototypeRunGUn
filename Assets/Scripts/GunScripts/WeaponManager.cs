using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static WeaponManager instance;
    //public BaseGun gun;
    public GunStats gun;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
          //  gun.SpawnBullet(transform.position, transform.rotation);
        }
    }
}
