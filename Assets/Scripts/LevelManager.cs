using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LevelManager : MonoBehaviour
{
    //public float floors;
    //[SerializeField]
    //GameObject levelObj;
    //public GameObject _platform;
    //public GameObject[] _rooms;
    //int platformFloor;

    public List<Obstacle> obstacleList;
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Init();
        #region KimHao's Stuff
        //generate random platforms
        //while(floors > 0)
        //{
        //    //Lets try with 2 different types of level generation, room variant and random middle platform spawn with pre-determined side platforms
        //    /*
        //    //pre-determined side platforms
        //    if (floors % 2 == 0)
        //    {
        //        //left side platform
        //        GameObject PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-9f, -7f), (Random.Range(floors * 4 - 1, floors * 4)) - 2), transform.rotation);
        //        PlatformClone.transform.SetParent(levelObj.transform);
        //        if (Random.Range(0, 2) == 0)
        //        {
        //            PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-2f, 2f), (Random.Range(floors * 4, floors * 4)) - 1), transform.rotation);
        //            PlatformClone.transform.SetParent(levelObj.transform);
        //        }
        //    }

        //    else
        //    {
        //        //right side platform
        //        GameObject PlatformClone = Instantiate(_platform, new Vector2(Random.Range(7f, 9f), (Random.Range(floors * 4 - 1, floors * 4)) - 2), transform.rotation);
        //        PlatformClone.transform.SetParent(levelObj.transform);
        //        if (Random.Range(0, 2) == 0)
        //        {
        //            PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-2f, 2f), (Random.Range(floors * 4, floors * 4)) - 1), transform.rotation);
        //            PlatformClone.transform.SetParent(levelObj.transform);
        //        }
        //    }
        //    */
        //    //Room variance

        //    platformFloor = Random.Range(0, _rooms.Length); //decide which floor preset to use
        //    GameObject RoomClone = Instantiate(_rooms[platformFloor], new Vector2(0f, (floors * 5f + 0.5f)), transform.rotation);
        //    RoomClone.transform.SetParent(gameObject.transform);
        //    //switch(platformPerFloor)
        //    //{
        //    //    case 1:
        //    //        GameObject PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-1f, 1f), (Random.Range(floors * 2 - 1, floors * 2)) - 2), transform.rotation);
        //    //        break;
        //    //    case 2:
        //    //        PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-4.5f, -2.5f), (Random.Range(floors * 2 - 1, floors * 2)) - 2), transform.rotation);
        //    //        PlatformClone = Instantiate(_platform, new Vector2(Random.Range(2.5f, 4.5f), (Random.Range(floors * 2 - 1, floors * 2)) - 2), transform.rotation);
        //    //        break;
        //    //    case 3:
        //    //        PlatformClone = Instantiate(_platform, new Vector2(Random.Range(6f, 8f), (Random.Range(floors * 2 - 1, floors * 2)) - 2), transform.rotation);
        //    //        PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-1f, 1f), (Random.Range(floors * 2 - 1, floors * 2)) - 2), transform.rotation);
        //    //        PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-8f, -6f), (Random.Range(floors * 2 - 1, floors * 2)) - 2) , transform.rotation);
        //    //        break;
        //    //}

        //    //for (var i = 0; i < platformPerFloor; i++)
        //    //{
        //    //    GameObject PlatformClone = Instantiate(_platform, new Vector2(i*5,floors - (Random.Range(-1f, 1f))), transform.rotation);
        //    //}
        //    floors--;
        //}
        #endregion
    }

    #region oldstuff
    //[Button]
    //private void Init()
    //{
    //    for(int i =0; i<floors; i++)
    //    {
    //        platformFloor = Random.Range(0, _rooms.Length); //decide which floor preset to use
    //        GameObject RoomClone = Instantiate(_rooms[platformFloor], new Vector2(0f, (i * 5f + 0.5f)), transform.rotation);
    //        RoomClone.transform.SetParent(gameObject.transform);
    //    }
    //}
    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }

    public Obstacle GetObstacle(Vector2 obstacleSize)       //might be slow? if you have to keep searching each time a room is generated.
    {
        List<Obstacle> _obs = new List<Obstacle>();
        for(int i =0; i<obstacleList.Count; i++)
        {
            if(obstacleList[i].Size.IsLesserThan(obstacleSize))     //as long as obstacle fits in allocated size
            {
                _obs.Add(obstacleList[i]);
            }
        }
        if (_obs == null) Debug.LogError("ERROR! No Obstacle of such size!");
        int rand = Random.Range(0, _obs.Count);
        return _obs[rand];
    }

}
