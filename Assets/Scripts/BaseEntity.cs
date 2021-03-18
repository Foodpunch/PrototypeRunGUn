using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour,IDamageable
{
    public float maxHealth;
    float currentHealth;

    public void OnTakeDamage(float _damage)
    {
        currentHealth -= _damage;
        if(currentHealth <=0f)
        {
            OnDeath();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDeath()
    {
        //play whatever anim needs to be played
    }
    

}
