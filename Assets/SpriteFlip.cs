using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlip : MonoBehaviour
{
    //Default direction to face is right
    SpriteRenderer _sr;
    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Right"))
        {
            _sr.flipX = false;
        }
        else
        {
            if (Input.GetButton("Left"))
            {
                _sr.flipX = true;
            }
            //else
            //{
            //    _sr.flipX = false;
            //}
        }
    }
}
