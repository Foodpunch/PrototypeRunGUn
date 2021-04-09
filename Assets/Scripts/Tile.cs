using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Tile : MonoBehaviour,IDamageable //For the tile prefab logic
{
    public Vector2 tilePosition;
    public int blockChance;
    SpriteRenderer _sr;
    Collider2D _col;
    public enum TileType        //actually might not need this anymore. 
    {
        EMPTY,
        WALL,
        HAZARD,
        NEUTRAL,
        SPECIAL
    }
    public TileType tileType;
    int health;
    public bool isStatic; //probabilistic tile or static
    //bool isDestructible = false;          //seems like a better implementation

    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
        _col.usedByEffector = false;    //only enable if the block is somehow special
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTakeDamage(StatWrapper entityStats, ContactPoint2D _contact)
    {
            
    }
    void Init()
    {
        switch(tileType)    //might not need this? Can just use a boolean for destructible, and bool for passthrough.
        {
            case TileType.WALL:
                break;
            case TileType.HAZARD:
                break;
            case TileType.NEUTRAL:
                break;
            case TileType.SPECIAL:
                _col.usedByEffector = true; //arbritarily assigning this as true. Assuming these are the "special" tiles
                break;
            case TileType.EMPTY:
                break;
        }
    }

    public void RollTile()
    {
        if(!isStatic)
        {
            int rand = Random.Range(1, 100);
            Debug.Log(rand);
            gameObject.SetActive(rand <= blockChance);  //if player rolls within chance, block spawns.
        }
    }
}
