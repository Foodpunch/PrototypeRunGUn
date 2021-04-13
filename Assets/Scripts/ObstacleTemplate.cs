using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;
//[ExecuteInEditMode]
public class ObstacleTemplate : MonoBehaviour
{
    //Selects an area in the room and chooses an obstacle to be placed inside.

    [BoxGroup("Obstacle Size")]
    [MinValue(1)]
    public int sizeX = 1;
    [BoxGroup("Obstacle Size")]
    [MinValue(1)]
    public int SizeY = 1;

    [SerializeField]
    Color gizmosColor;

    Vector2Int Size;
    GameObject[,] obstacleArea;
    [SerializeField]
    ObstacleList _obstaclesList;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 GetObstaclePos(Obstacle _obs)
    {
        int x = Mathf.FloorToInt( _obs.Size.x);         //do we even need to floor?
        int y = Mathf.FloorToInt( _obs.Size.y);
        int offsetX = Random.Range(0, Size.x - x+1);      //get the max allowed positions.
        int offsetY = Random.Range(0, Size.y - y+1);

        return new Vector2(offsetX, offsetY);
    }
  

    [Button]
    public void TestObstacle()
    {
        Size = new Vector2Int(sizeX, SizeY);
        Obstacle _obs = _obstaclesList.GetObstacle(Size);
        GameObject obsClone = Instantiate(_obs.gameObject,transform);
        obsClone.transform.localPosition = GetObstaclePos(_obs);
        obsClone.GetComponent<Obstacle>().RollProbabilisticTiles();
        // Obstacle Obstacle = ObstacleList.Instance.GetObstacle(Size);
        // Instantiate(Obstacle.gameObject, transform.position, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Size = new Vector2Int(sizeX, SizeY);
        if (Size.IsLesserThan(Vector2Int.zero)) return;
        else
        {
            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    Vector2 pos = new Vector2(x, y);
                    Gizmos.DrawCube(pos + (Vector2)transform.position, Vector2.one*0.95f);
                }
            }
        }
    }

    #region unused function
    //void OnSizeChangeCallback()
    //{
    //    Size = new Vector2Int(sizeX, SizeY);
    //    if (obstacleArea != null) ClearPreviousTemplate();
    //    if (Size.IsLesserThan(Vector2Int.zero)) return;
    //    if (Size.IsMoreThan(Vector2Int.zero))
    //    {
    //        obstacleArea = new GameObject[(int)Size.x, (int)Size.y];
    //        for (int x = 0; x < obstacleArea.GetLength(0); x++)
    //        {
    //            for (int y = 0; y < obstacleArea.GetLength(1); y++)
    //            {
    //                Vector2 pos = new Vector2(x, y);
    //                GameObject objClone = Instantiate(templateObj, pos + (Vector2)transform.position, Quaternion.identity);
    //                objClone.transform.SetParent(transform);
    //                obstacleArea[x, y] = objClone;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < gameObject.transform.childCount; i++)
    //        {
    //            DestroyImmediate(gameObject.transform.GetChild(i).gameObject);
    //        }
    //    }
    //}

    //void ClearPreviousTemplate()
    //{
    //    for (int x = 0; x < obstacleArea.GetLength(0); x++)
    //    {
    //        for (int y = 0; y < obstacleArea.GetLength(1); y++)
    //        {
    //            DestroyImmediate(obstacleArea[x, y].gameObject);
    //        }
    //    }
    //}
    #endregion
}
