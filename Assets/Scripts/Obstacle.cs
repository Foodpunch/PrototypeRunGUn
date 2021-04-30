using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Obstacle : MonoBehaviour
{
    public List<Tile> tiles = new List<Tile>();
    List<Tile> ProbabilisticTileList = new List<Tile>();
    [SerializeField]
    Vector2 obstacleSize;
    bool canMirror;         //bool to check if this obstacle can be mirrored
    [HideInInspector]
    public bool isMirrored;

    Tile[,] ObstacleSprites;

    // Start is called before the first frame update
    void Start()
    {
        if (obstacleSize == Vector2.zero) Debug.LogError("Size of obstacle not set in prefab!");
        RollProbabilisticTiles();
        ObstacleSprites = new Tile[(int)obstacleSize.x, (int)obstacleSize.y];
        Test();
      //  Debug.Log("Name : " + transform.parent.transform.parent.name +"\n" + "World Pos : " + transform.position + "\n" + "IsTouchingWall? : " + IsTouchingWall());
    }
    public Vector2 Size     //size should be readonly
    {
        get { return obstacleSize; }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Init()
    {
        if(tiles ==null)
        {
            GetAllTilesInObstacle();
            SetTileNames();
            GetAllProbabilisticTiles();
            RollProbabilisticTiles();
        }
   
    }
    [Button]
    public void GetAllTilesInObstacle()
    {
        tiles.Clear();
        for(int i =0; i< gameObject.transform.childCount;i++)
        {
            if(gameObject.transform.GetChild(i).GetComponent<Tile>()!=null)
            {
                tiles.Add(gameObject.transform.GetChild(i).GetComponent<Tile>());
            }
        }
    }
    [Button]
    public void SetTileNames()
    {
        if(tiles!=null)
        {
            for(int i =0; i< tiles.Count;i++)
            {
                string name = tiles[i].isStatic ? "Static" : "Probabilistic";
                Vector2 tilePos = new Vector2(tiles[i].transform.position.x, tiles[i].transform.localPosition.y);
                 tiles[i].gameObject.name = name + " , " + ((Vector2)tiles[i].transform.localPosition).ToString();
            }
        }
    }
    [Button]
    public void GetAllProbabilisticTiles()
    {
        if(tiles!=null)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (!tiles[i].isStatic) ProbabilisticTileList.Add(tiles[i]);
            }
        }
    }
    [Button]
    public void RollProbabilisticTiles()       //need to make sure roll first before changing sprites!
    {
        GetAllProbabilisticTiles();
        for (int i = 0; i < ProbabilisticTileList.Count; i++)
        {
            int rand = Random.Range(0, 100);
            if (rand < 50) MirrorObstacle();
            ProbabilisticTileList[i].GetComponent<Tile>().RollTile();
        }
        //if (ProbabilisticTileList==null)
        //{
        //    for (int i = 0; i < ProbabilisticTileList.Count; i++)
        //    {
        //        ProbabilisticTileList[i].GetComponent<Tile>().RollTile();
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < ProbabilisticTileList.Count; i++)
        //    {
        //        ProbabilisticTileList[i].GetComponent<Tile>().RollTile();
        //    }
        //}
    }
    [Button]
    public void GetObstacleSize()       //tool to help assign obstacle size
    {
        if(obstacleSize == Vector2.zero)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                obstacleSize.x = obstacleSize.x < tiles[i].transform.localPosition.x ? tiles[i].transform.localPosition.x : obstacleSize.x;
                obstacleSize.y = obstacleSize.y < tiles[i].transform.localPosition.y ? tiles[i].transform.localPosition.y : obstacleSize.y;
            }
            obstacleSize += Vector2.one;    //increment by 1 to get the actual;
        }
    }
   
    [Button]
    public void MirrorObstacle()
    {
        //Check if can mirror first!
        Vector2 flipVector = new Vector2(-1, 1);
        Vector2 translationVector = new Vector2(-(Size.x - 1), 0);    //technically its min-max. 0-Size.x-1 = -Size.x-1
        for(int i =0; i< tiles.Count; i++)
        {
            tiles[i].transform.localPosition += (Vector3)translationVector;     //translate first
            tiles[i].transform.localPosition *= flipVector;                     //then flip
        }
        SetTileNames();
        isMirrored = true;          //bool to check if mirrored and place correct sprites
    }
   
    public bool IsTouchingWall()
    {
        return transform.position.x == 0 || (transform.position.x + Size.x) == 7;
    }
    [Button]
    public void Test()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            //Debug.Log("Grid Pos = "+tiles[i].name + (int)(tiles[i].transform.position.x) + " , " + (int)(tiles[i].transform.localPosition.y - transform.localPosition.y));
            //Debug.Log("Tile Pos = " + tiles[i].name + (int)(tiles[i].tilePosition.x) + " , " + (int)(tiles[i].tilePosition.y - transform.localPosition.y));
            //Debug.Log(ObstacleSprites.GetLength(0) + " , " + ObstacleSprites.GetLength(1));
            if(tiles[i].gameObject.activeInHierarchy)
            {
                ObstacleSprites[(int)(tiles[i].transform.localPosition.x), (int)(tiles[i].transform.localPosition.y)] = tiles[i];

            }

            //if (ObstacleSprites[(int)(tiles[i].transform.localPosition.x), (int)(tiles[i].transform.localPosition.y)].gameObject.activeInHierarchy)
            //{
            //  }
        }
        //Debug.Log("Name : " + transform.parent.transform.parent.name + "\n" + "World Pos : " + transform.position + "\n" + "IsTouchingWall? : " + IsTouchingWall());
        bool isLeftWall = transform.position.x == 0;
        bool isRightWall = (transform.position.x + Size.x) == 7;

        //for (int x = 0; x < ObstacleSprites.GetLength(0); x++)
        //{
        //    for (int y = 0; y < ObstacleSprites.GetLength(1); y++)
        //    {
        //        if (ObstacleSprites[x, y] != null)
        //        {
        //            //left most 
        //            if (isLeftWall)
        //            {
        //                if (ObstacleSprites[0, y] != null)
        //                {
        //                    ObstacleSprites[x, y]._sr.sprite = LevelManager.instance.levelSprites.SpriteList[53];
        //                  //  ObstacleSprites[(int)obstacleSize.x-1, y]._sr.sprite = LevelManager.instance.levelSprites.SpriteList[52];


        //                }

        //            }
        //            if (isRightWall)
        //            {
        //                if (ObstacleSprites[(int)obstacleSize.x - 1, y] != null)
        //                {
        //                    ObstacleSprites[(int)obstacleSize.x - 1, y]._sr.sprite = LevelManager.instance.levelSprites.SpriteList[53];
        //                }
        //            }
        //        }
        //    }
        //}
    }
 
    
    #region unused functions
    //Vector2 GetSize()     //used to get size at runtime.
    //{
    //    Vector2 size = new Vector2();
    //    for (int i = 0; i < tiles.Count; i++)
    //    {
    //        size.x = size.x < tiles[i].transform.localPosition.x ? tiles[i].transform.localPosition.x : size.x;
    //        size.y = size.y < tiles[i].transform.localPosition.y ? tiles[i].transform.localPosition.y : size.y;
    //    }
    //    size += Vector2.one;    //increment by 1 to get the actual;
    //    return size;
    //}
    #endregion
}
