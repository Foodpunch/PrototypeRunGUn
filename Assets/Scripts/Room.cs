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
    int width = 6;      //set to 6 first
    int height = 4;          //min 4, max 6?

    Vector2[,] RoomTiles;       //vector 2 because you just need the positions

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
