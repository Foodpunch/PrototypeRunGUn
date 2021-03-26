using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float floors;
    public GameObject _platform;
    public GameObject[] _rooms;
    int platformPerFloor;

    // Start is called before the first frame update
    void Start()
    {
        //generate random platforms
        while(floors > 0)
        {
            //Lets try with 2 different types of level generation, room variant and random middle platform spawn with pre-determined side platforms

            //pre-determined side platforms
            if (floors % 2 == 0)
            {
                //left side platform
                GameObject PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-9f, -7f), (Random.Range(floors * 4 - 1, floors * 4)) - 2), transform.rotation);
                if (Random.Range(0, 2) == 0)
                {
                    PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-2f, 2f), (Random.Range(floors * 4, floors * 4)) - 1), transform.rotation);
                }
            }

            else
            {
                //right side platform
                GameObject PlatformClone = Instantiate(_platform, new Vector2(Random.Range(7f, 9f), (Random.Range(floors * 4 - 1, floors * 4)) - 2), transform.rotation);
                if (Random.Range(0, 2) == 0)
                {
                    PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-2f, 2f), (Random.Range(floors * 4, floors * 4)) - 1), transform.rotation);
                }
            }

            //Room variance


            //platformPerFloor = Random.Range(0, 3);
            //switch(platformPerFloor)
            //{
            //    case 1:
            //        GameObject PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-1f, 1f), (Random.Range(floors * 2 - 1, floors * 2)) - 2), transform.rotation);
            //        break;
            //    case 2:
            //        PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-4.5f, -2.5f), (Random.Range(floors * 2 - 1, floors * 2)) - 2), transform.rotation);
            //        PlatformClone = Instantiate(_platform, new Vector2(Random.Range(2.5f, 4.5f), (Random.Range(floors * 2 - 1, floors * 2)) - 2), transform.rotation);
            //        break;
            //    case 3:
            //        PlatformClone = Instantiate(_platform, new Vector2(Random.Range(6f, 8f), (Random.Range(floors * 2 - 1, floors * 2)) - 2), transform.rotation);
            //        PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-1f, 1f), (Random.Range(floors * 2 - 1, floors * 2)) - 2), transform.rotation);
            //        PlatformClone = Instantiate(_platform, new Vector2(Random.Range(-8f, -6f), (Random.Range(floors * 2 - 1, floors * 2)) - 2) , transform.rotation);
            //        break;
            //}

            //for (var i = 0; i < platformPerFloor; i++)
            //{
            //    GameObject PlatformClone = Instantiate(_platform, new Vector2(i*5,floors - (Random.Range(-1f, 1f))), transform.rotation);
            //}
            floors--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
