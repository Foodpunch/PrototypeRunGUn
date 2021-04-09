using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;
//[ExecuteInEditMode]
public class ObstacleTemplate : MonoBehaviour
{
    [OnValueChanged("OnSizeChangeCallback")]
    [BoxGroup("Obstacle Size")]
    [MinValue(1)]
    public int sizeX = 1;
    [OnValueChanged("OnSizeChangeCallback")]
    [BoxGroup("Obstacle Size")]
    [MinValue(1)]
    public int SizeY = 1;


    Vector2Int Size;
    [SerializeField]
    GameObject templateObj;
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
    void OnSizeChangeCallback()
    {
        Size = new Vector2Int(sizeX, SizeY);
        if (obstacleArea != null) ClearPreviousTemplate();
        if (Size.IsLesserThan(Vector2Int.zero)) return;
        if(Size.IsMoreThan(Vector2Int.zero))
        {
            obstacleArea = new GameObject[(int)Size.x,(int)Size.y];
            for (int x=0; x<obstacleArea.GetLength(0); x++)
            {
                for(int y=0; y< obstacleArea.GetLength(1);y++)
                {
                    Vector2 pos = new Vector2(x, y);
                    GameObject objClone = Instantiate(templateObj, pos+(Vector2)transform.position, Quaternion.identity);
                    objClone.transform.SetParent(transform);
                    obstacleArea[x, y] = objClone;
                }
            }
        }
        else
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                DestroyImmediate(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
    void ClearPreviousTemplate()
    {
        for (int x = 0; x < obstacleArea.GetLength(0); x++)
        {
            for (int y = 0; y < obstacleArea.GetLength(1); y++)
            {
                DestroyImmediate(obstacleArea[x, y].gameObject);
            }
        }
    }
    [Button]
    public void TestObstacle()
    {
        Size = new Vector2Int(sizeX, SizeY);
        Debug.Log(Size);
        Obstacle _obs = _obstaclesList.GetObstacle(Size);
        Instantiate(_obs.gameObject, transform.position, Quaternion.identity);
        // Obstacle Obstacle = ObstacleList.Instance.GetObstacle(Size);
        // Instantiate(Obstacle.gameObject, transform.position, Quaternion.identity);
    }
}
