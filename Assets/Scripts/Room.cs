using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int width = 6;      //set to 6 first
    int height = 4;          //min 4, max 6?
    int floor;              
    Tile[,] RoomTiles;       //2D array of tiles in the room. (do we really need this)
    [SerializeField]
    Sprite[] tileSprites;
    [SerializeField]
    Tile wallTile;
   


    void Start()
    {
        RoomTiles = new Tile[width, height];
        switch(roomType)
        {
            case RoomType.OBSTACLE:
                //by right define an obstacle size? then grab one? 
                //then spawn? (by right should also add into the array)
                Vector2Int obstacleSize = GetRandomSize();
                Obstacle obstacle = LevelManager.instance.GetObstacle(obstacleSize);
                Instantiate(obstacle.gameObject, GetObstaclePosInRoom(obstacleSize),Quaternion.identity);
                break;
        }
        //Arbitrary logic for spawning the nice walls.
        for(int x = 0; x < width; x++)
        {
            for(int y=0; y<height; y++)
            {
                //Spawn side walls
                bool topandbot = (y < height || y > 0);
                bool middleLeft = (x == 0 && topandbot);
                bool middleRight = (x == 5 && topandbot);

                if (middleLeft)
                {
                    GameObject middleLeftWall = Instantiate(wallTile.gameObject, new Vector2(x, y), Quaternion.identity);
                    middleLeftWall.GetComponent<SpriteRenderer>().sprite = tileSprites[1];
                }
                if (middleRight)
                {
                    GameObject middleRightWall = Instantiate(wallTile.gameObject, new Vector2(x+1, y), Quaternion.identity);
                    middleRightWall.GetComponent<SpriteRenderer>().sprite = tileSprites[0];
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
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
}
