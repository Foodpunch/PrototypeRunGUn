﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "ObstacleList")]
public class ObstacleList : ScriptableObject
{
    private static ObstacleList instance;
    public static ObstacleList Instance { get => instance; set => instance = value; }
    public Obstacle[] ObstaclesPrefabsArray; //serialisable hash set next time?

    

    public Obstacle GetObstacle(Vector2 size)
    {
        List<Obstacle> _obs = new List<Obstacle>();
        for (int i = 0; i < ObstaclesPrefabsArray.Length; i++)
        {
            if (ObstaclesPrefabsArray[i].Size.IsLesserThan(size))     //as long as obstacle fits in allocated size
            {
                _obs.Add(ObstaclesPrefabsArray[i]);
            }
        }
        if (_obs == null) Debug.LogError("ERROR! No Obstacle of such size!");
        int rand = Random.Range(0, _obs.Count);     //should change this to seed in future
        return _obs[rand];
    }


#if UNITY_EDITOR
    [Button]
    ///<summary>
    ///Only used in editor!!! Used for adding all obstacles to array!
    ///</summary>
    public void GetAllObstaclePrefabs()
    {
        ObstaclesPrefabsArray = Resources.LoadAll<Obstacle>("Prefabs/Obstacles");
        for(int i=0; i<ObstaclesPrefabsArray.Length; i++)
        {
            Debug.Log(ObstaclesPrefabsArray[i].name);
        }
    }
#endif
}
