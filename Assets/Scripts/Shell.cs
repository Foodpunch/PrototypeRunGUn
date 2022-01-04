using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float timeToInActive = 4f;
    float deltaTime = 0f;
    Rigidbody2D _rb;
    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        deltaTime += Time.deltaTime;
        if (_rb.velocity.x <= 0.1f && _rb.velocity.y <= 0.1f && deltaTime >= timeToInActive)
            _rb.Sleep();
    }

}
