using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolingScript : MonoBehaviour
{
    /* ======= How to use =============
     * 1. Create empty game obj and put in the script. Change size accordingly, put in tags you want to use and prefab & how big list should be
     * 2. Make sure you always call the singleton, so you can access the "SpawnFromPool" function (make sure you have the correct params).
     * 3. For the obj that is to be pooled, you need to have a script in it that has the interface "IpooledObj" and it must implement the "OnObjSpawned" function.
     * 4. Implement whatever stuff that needs to be run before the obj hides itself with SetActive(false).
     * 5. Make sure you remember to enqueue the obj again!!! e.g -> ObjectPoolingScript.current.poolDictionary[myName].Enqueue(gameObject); 
     */


    public Dictionary<string, Queue<GameObject>> poolDictionary; //makes a dictionary pool with string as tag and "list" of Game OBj

  //  public int pooledAmount = 5;
    // public bool willGrow; // allows the list to grow

    #region Singleton
    public static ObjectPoolingScript current;
    void Awake()
    {
        current = this; //to make sure the static class that is instantiated is this.
    }

    #endregion

    // Use this for initialization
    [System.Serializable]
    public class Pool           //class for the pool, items should contain this info. Maybe use struct?
    {
        public string tag;                  //string for item tag
        public GameObject prefab;           //To store prefab
        public int size;                    //size to spawn
     // public bool willGrow;               //if list is allowed to grow (for future implementation)
    }
    public List<Pool> pools;                    //makes a list of the class pool
    GameObject _obj;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>(); //instantiates a new pool dictionary
        foreach (Pool pool in pools)                                  //runs through list of pools
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();     //new "list" in this sense a queue is like a list for the dictionary
            for (short v = 0; v < pool.size; v++)       //forloop to spawn pooled Objs
            {
                _obj = Instantiate(pool.prefab);
                _obj.transform.SetParent(gameObject.transform);      
                _obj.SetActive(false);
                objectPool.Enqueue(_obj);    //adds to queue
            }
            poolDictionary.Add(pool.tag, objectPool); //adds pool to dictionary
        }
       

    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)  //call objPool.SpawnFromPool(name of obj, pos, rot) to take from pool.
    {
        
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        GameObject objToSpawn = poolDictionary[tag].Dequeue(); //pulls out first element from pool and spawns
        if (!objToSpawn.activeInHierarchy && poolDictionary[tag].Count > 1) //checks the list if there are still objs to spawn
        {
            objToSpawn.SetActive(true); //if have then choose one and "spawn" it
            objToSpawn.transform.position = pos;
            objToSpawn.transform.rotation = rot;

            IPooledObj pooledObj = objToSpawn.GetComponent<IPooledObj>(); //grabs the component for any obj with this interface implemented.
            if (pooledObj != null)
            {
                pooledObj.OnObjSpawn();
            }    
            return objToSpawn;
        }
        else //so usually when you dequeue, the queue gets empty so you need to spawn more.
        {
            GameObject _clone = Instantiate(objToSpawn); //makes a clone to spawn (meaning you need to instantiate EXTRA)
            _clone.name = tag + "(CLONE)"; //keeps it neat
            _clone.transform.SetParent(gameObject.transform);
            poolDictionary[tag].Enqueue(objToSpawn); //adds clone to queue. Note that we don't add the clone, but add the existing obj to spawn.
            _clone.SetActive(true);
            _clone.transform.position = pos;
            _clone.transform.rotation = rot;
            IPooledObj pooledObj = _clone.GetComponent<IPooledObj>(); //grabs the component for any obj with this interface implemented.
            if (pooledObj != null)
            {
                pooledObj.OnObjSpawn();
            }
            return _clone;
        }
        
    }


}


//Interface for Obj that can be pooled. Implement for them to call the OnObjSpawn instead of using OnEnable.

public interface IPooledObj  //interface to tag any obj that needs to be pooled OnObjSpawn should contain logic of when to hide the obj.
{
    void OnObjSpawn();
}