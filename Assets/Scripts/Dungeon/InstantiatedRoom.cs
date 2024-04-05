using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InstantiatedRoom : MonoBehaviour
{
    [HideInInspector]
    public Room room;

    [HideInInspector]
    public Grid grid;

    [HideInInspector]
    public Tilemap groundTilemap;

    [HideInInspector]
    public Tilemap decoration1Tilemap;

    [HideInInspector]
    public Tilemap decoration2Tilemap;

    [HideInInspector]
    public Tilemap frontTilemap;

    [HideInInspector]
    public Tilemap collisionTilemap;

    [HideInInspector]
    public Tilemap minimapTilemap;

    [HideInInspector]
    public Bounds roomColliderBounds;
}
