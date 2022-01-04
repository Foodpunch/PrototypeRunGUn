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
  //  public List<Room> CurrentRoomList = new List<Room>();       //list of the rooms currently spawned in the level

    public Queue<Room> CurrentRoomQueue = new Queue<Room>();      //queue of rooms
    Room lastSpawnedRoom;

    Vector2 playerPos;
    float levelMaxY;                                    //max Y value of the current level
    public GameSprites levelSprites;

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
        NextRoomSpawnLogic();
    }

    //Pick random room from list (will later change to picking randomly from room type
    Room  GetRandomRoom()
    {
        int i = Random.Range(0, Rooms.Count);
        return Rooms[i];
    }

    void SetRoomPosAndSpawn(Room roomToSpawn)
    {
        if (CurrentRoomQueue.Count <= 0)
        {
            SpawnRoom(GetRandomRoom(),Vector2.zero);
            return;
        }
            
        //Get previous room Y pos and size, spawn on top of it.
        int offsetY = lastSpawnedRoom.height;
        Vector2 roomPos = (Vector2)lastSpawnedRoom.transform.position + (Vector2.up* offsetY);

        SpawnRoom(roomToSpawn,roomPos);
    }
    void SpawnRoom(Room roomToSpawn, Vector2 roomPos)
    {
        Room room = Instantiate(roomToSpawn, transform);
        lastSpawnedRoom = room;
        room.transform.localPosition = roomPos;
        CurrentRoomQueue.Enqueue(room);
        levelMaxY = roomPos.y + room.height;
        
    }
   void Init()
    {
        //First room
        SpawnRoom(Rooms[2],Vector2.zero);
        //spawn base number of rooms
        for (int i = 0; i < 6; i++)
        {
            SetRoomPosAndSpawn(GetRandomRoom());
        }
    }
    void NextRoomSpawnLogic()
    {
        //track player Y, compare to max Y, if value difference is more than amt, spawn next level
        
        playerPos = PlayerEntity.instance.transform.position;
       // Debug.Log("Player pos + 9 = " + (playerPos.y + 9f) + " , Level Max " + levelMaxY + " = " + ((playerPos.y + 9f) >= levelMaxY));
        if ((playerPos.y + 12f)>=levelMaxY)
        {
            SetRoomPosAndSpawn(GetRandomRoom());        //in the future level manager will determine what room
            //pool the previous rooms here (might need to set a limit for when to dequeue
            Room _room = CurrentRoomQueue.Dequeue();
            //UNCOMMENT TO HIDE ROOMS HERE!! Currently showing all rooms for testing.
            //_room.gameObject.SetActive(false);
        }
    }
}
