using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleList")]
public class ObstacleList : ScriptableObject
{
    private static ObstacleList instance;
    public static ObstacleList Instance { get => instance; set => instance = value; }
    public List<Obstacle> ObstaclesPrefabs;

    public Obstacle GetObstacle(Vector2 size)
    {
        List<Obstacle> _obs = new List<Obstacle>();
        for (int i = 0; i < ObstaclesPrefabs.Count; i++)
        {
            if (ObstaclesPrefabs[i].Size.IsLesserThan(size))     //as long as obstacle fits in allocated size
            {
                _obs.Add(ObstaclesPrefabs[i]);
            }
        }
        if (_obs == null) Debug.LogError("ERROR! No Obstacle of such size!");
        int rand = Random.Range(0, _obs.Count);     //should change this to seed in future
        return _obs[rand];
    }

    
}
