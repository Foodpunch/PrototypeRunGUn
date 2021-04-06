using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour,IDamageable
{
    public enum TileType
    {
        WALL,
        HAZARD,
        NEUTRAL,
        SPECIAL
    }
    public TileType tileType;
    int health;
    public bool isStatic; //probabilistic tile or static
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTakeDamage(StatWrapper entityStats, ContactPoint2D _contact)
    {
            
    }
}
