using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    //ObjectPoolingScript current;
    //public GameObject ObjToClone;
    float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > 0.3f)
        {
            
            GameObject clone = ObjectPoolingScript.current.SpawnFromPool("Cube", transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
            timer = 0;
        }
	}
}
