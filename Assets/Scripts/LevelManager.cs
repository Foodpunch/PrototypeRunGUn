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

    public static LevelManager instance;
    public int maxRoomCount;
    [SerializeField]
    List<Room> Rooms;
    public List<Room> CurrentRoomList = new List<Room>();       //list of the rooms currently spawned in the level

    private void Awake()
    {
        instance = this;        
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
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

    //Pick random room from list (will later change to picking randomly from room type
    Room  GetRandomRoom()
    {
        int i = Random.Range(0, Rooms.Count);
        return Rooms[i];
    }

    void SetRoomPosAndSpawn(Room roomToSpawn)
    {
        if (CurrentRoomList.Count <= 0)
        {
            SpawnRoom(GetRandomRoom(),Vector2.zero);
            return;
        }
            
        //Get previous room Y pos and size, spawn on top of it.
        Room previousRoom = CurrentRoomList[CurrentRoomList.Count - 1];
        Vector2 previousRoomPos = previousRoom.gameObject.transform.position;
        int offsetY = previousRoom.height;
        Vector2 roomPos = previousRoomPos + (Vector2.up* offsetY);

        //roomClone.transform.position = roomPos;
        SpawnRoom(roomToSpawn,roomPos);
    }
    void SpawnRoom(Room roomToSpawn, Vector2 roomPos)
    {
        Room room = Instantiate(roomToSpawn, transform);
        room.transform.localPosition = roomPos;
        CurrentRoomList.Add(room);
    }
   void Init()
    {
        //spawn base number of rooms
        for (int i = 0; i < 16; i++)
        {
            SetRoomPosAndSpawn(GetRandomRoom());
        }
    }

}
