using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatform : MonoBehaviour
{
    public int appear;

    // Start is called before the first frame update
    void Start()
    {
        appear = Random.Range(0, 2);
        if(appear == 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
