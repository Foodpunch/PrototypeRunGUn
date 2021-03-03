using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour,IPooledObj
{
     float speed = 5f;
    public float airTime = 4f; //how long bullet stays in the air (also works as its range)
    Vector3 bulletDir;

    Rigidbody2D _rb;
    public void OnObjSpawn()
    {
        _rb = GetComponent<Rigidbody2D>();
      
      
    }

    public void SetValue(Vector3 _dir, float _speed, float _airtime)
    {
        bulletDir = _dir;
        speed = _speed;
        airTime = _airtime;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.AddForce(bulletDir * speed, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision!=null)
        {
            _rb.Sleep();
            gameObject.SetActive(false);
            ObjectPoolingScript.current.poolDictionary["bullet"].Enqueue(gameObject);
        }
    }
}
