using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour,IPooledObj {

    bool isObjSpawned;
    float timer;

    public void OnObjSpawn() 
    {
        isObjSpawned = true;
    }
    public void OnDespawnObj()              //this is the important bit!!
    {
        gameObject.GetComponent<Rigidbody>().Sleep();
        gameObject.SetActive(false);                //set active to 0
        gameObject.transform.position = Vector3.zero; //reset the pos
        gameObject.transform.rotation = Quaternion.identity; //reset rot
        ObjectPoolingScript.current.poolDictionary["Cube"].Enqueue(gameObject); //enqueue the damn thing again!!!
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isObjSpawned)
        {
            timer += Time.deltaTime;
            if(timer > 3)
            {
                isObjSpawned = false;
                timer = 0;
            }
        }
        if(!isObjSpawned)
        {
            OnDespawnObj();
        }
	}
}
