using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public bool autoFire = true;    //Toggles autofire
    public float firerate = 3f;     //shots per second
    float nextTimeToFire = 0f;

    Vector3 shootDirection = Vector3.right;
    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetShootDirection();
        if (autoFire)
        {
            Autofire();
        }
        else
        {
            SemiAuto();
        }

    }
    void Autofire()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            //set timer for how long gun has been firing?
            //Spawn bullets here
            GameObject bullet =  ObjectPoolingScript.current.SpawnFromPool("bullet", transform.position, rot);
            Debug.Log(shootDirection + "shootdir");
            bullet.GetComponent<BulletScript>().SetValue(shootDirection, 5f, 4f);
            nextTimeToFire = Time.time + (1f / firerate);
        }
        
    }
    void SemiAuto()
    {
        if (Input.GetButtonDown("Fire1"))
        {

        }
    }
    void SetShootDirection()
    {
        if (Input.GetButton("Right")) shootDirection = Vector2.right;
        if (Input.GetButton("Left")) shootDirection = Vector2.left;
        if (Input.GetButton("Up")) shootDirection = Vector2.up;
        if (Input.GetButton("Down")) shootDirection = Vector2.down;

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg-90f;
        rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rot;
    }
   
}
