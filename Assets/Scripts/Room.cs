using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Room : MonoBehaviour
{
    public enum RoomType
    {
        OBSTACLE,       //More probabilistic obstacles
        COMBAT,         //More static platforms
        SPECIAL,        //future implementation for shop
        MOBILITY        //Static platforms + stuff to aid player
    }
    RoomType roomType = RoomType.OBSTACLE;
    [BoxGroup("Room Size")]
    public int width = 6;      //set to 6 first
    [BoxGroup("Room Size")]
    public int height = 4;          //min 4, max 6?
    int floor;              
    GameObject[,] RoomArray;       //2D array of tiles in the room. (do we really need this)
    [SerializeField]
    Tile wallTile;

    [SerializeField]
    List<ObstacleTemplate> templateList;

    void Start()
    {
        ObstacleTemplate[] array = transform.GetComponentsInChildren<ObstacleTemplate>();
        templateList = new List<ObstacleTemplate>(array);
        SpawnObstacles();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    [Button]
    public void GenerateRoomWalls()
    {
        RoomArray = new GameObject[width, height];
        ClearRoomArray();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //bool isWithinYBounds = (y < height || y > 0);
                bool isLeftBound = (x == 0 );
                bool isRightBound = (x == width-1);

                if (isLeftBound)
                {
                    GameObject wallClone = Instantiate(wallTile.gameObject, new Vector2(x-1, y)+(Vector2)transform.position, Quaternion.identity);
                    wallClone.transform.SetParent(transform);
                    RoomArray[x, y] = wallClone;
                }
                if (isRightBound)
                {
                    GameObject wallClone = Instantiate(wallTile.gameObject, new Vector2(x+1, y)+(Vector2)transform.position, Quaternion.identity);
                    wallClone.transform.SetParent(transform);
                    RoomArray[x, y] = wallClone;
                  
                }
            }
        }
    }

    Vector2Int GetRandomSize()
    {
        int randWidth = Random.Range(4, width);
        int randHeight = Random.Range(1, height);
        return new Vector2Int(randWidth, randHeight);
    }

    Vector2 GetObstaclePosInRoom(Vector2Int size)      //obstacles origin is bottom right so calculate that.
    {
        int xRange = width - size.x;
        int yRange = height - size.y;
        int randX = Random.Range(0, xRange);
        int randY = Random.Range(0, yRange);
        return (new Vector2(randX, randY));
    }

    public void ClearRoomArray()
    {
        for(int i= gameObject.transform.childCount-1; i>0; i--)
        {
            if(gameObject.transform.GetChild(i).GetComponent<Tile>() !=null)
            {
                DestroyImmediate(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }

    public void SpawnObstacles()
    {
        foreach(ObstacleTemplate obt in templateList)
        {
            obt.TestObstacle();
        }
    }
    #region unused code for room to spawn obstacles randomly

    //void SetObstacleTemplate()
    //{
    //    RoomTiles = new Tile[width, height];
    //    switch (roomType)
    //    {
    //        case RoomType.OBSTACLE:
    //            //by right define an obstacle size? then grab one? 
    //            //then spawn? (by right should also add into the array)
    //            Vector2Int obstacleSize = GetRandomSize();
    //            Obstacle obstacle = ObstacleList.Instance.GetObstacle(obstacleSize);
    //            Instantiate(obstacle.gameObject, GetObstaclePosInRoom(obstacleSize), Quaternion.identity);
    //            break;
    //    }
    //}

    //unused for now, will implement on level manager or something
    //void SpriteLogic()
    //{
    //    //Arbitrary logic for spawning the nice walls.
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            //Spawn side walls
    //            bool topandbot = (y < height || y > 0);
    //            bool middleLeft = (x == 0 && topandbot);
    //            bool middleRight = (x == 5 && topandbot);

    //            if (middleLeft)
    //            {
    //                GameObject middleLeftWall = Instantiate(wallTile.gameObject, new Vector2(x, y), Quaternion.identity);
    //                middleLeftWall.GetComponent<SpriteRenderer>().sprite = tileSprites[1];
    //            }
    //            if (middleRight)
    //            {
    //                GameObject middleRightWall = Instantiate(wallTile.gameObject, new Vector2(x + 1, y), Quaternion.identity);
    //                middleRightWall.GetComponent<SpriteRenderer>().sprite = tileSprites[0];
    //            }
    //        }
    //    }
    //}
    #endregion
}
