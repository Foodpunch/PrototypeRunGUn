using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public bool autoFire = true;    //Toggles autofire
    public float firerate = 3f;     //shots per second


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(autoFire)
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
        if(Input.GetButton("Fire1"))
        {

        }
    }
    void SemiAuto()
    {
        if(Input.GetButtonDown("Fire1"))
        {

        }
    }
}
