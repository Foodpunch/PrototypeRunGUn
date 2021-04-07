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

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Vector2 Size
    {
        get { return obstacleSize; }
        //set { }
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
    void RollProbabilisticTiles()
    {
        if(ProbabilisticTileList==null)
        {
            GetAllProbabilisticTiles();
        }
        else
        {
            for (int i = 0; i < ProbabilisticTileList.Count; i++)
            {
                ProbabilisticTileList[i].GetComponent<Tile>().RollTile();
            }
        }
    }
    [Button]
    public void GetObstacleSize()
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
}
