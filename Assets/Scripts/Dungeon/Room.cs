using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public string id;

    public string roomTemplateID;

    public GameObject prefab;

    public RoomNodeTypeSO roomNodeType;

    public Vector2Int lowerBounds;

    public Vector2Int upperBounds;

    public Vector2Int roomTemplateLowerBounds;

    public Vector2Int roomTemplateUpperBounds;

    public Vector2Int[] spawnPositionArray;

    public List<SpawnableObjectsByLevel<EnemyDetailsSO>> enemiesByLevelList;

    public List<RoomEnemySpawnParameters> roomLevelEnemySpawnParametersList;

    public List<string> childRoomIDList;

    public string parentRoomID;

    public List<Doorway> doorWayList;

    public bool isPositioned = false;

    public InstantiatedRoom instantiatedRoom;

    public bool isLit = false;

    public bool isClearedOfEnemies = false;

    public bool isPreviouslyVisited = false;

    public Room()
    {
        childRoomIDList = new List<string>();
        doorWayList = new List<Doorway>();
    }

    public int GetNumberOfEnemiesToSpawn(DungeonLevelSO dungeonLevel)
    {
        foreach (RoomEnemySpawnParameters roomEnemySpawnParameters in roomLevelEnemySpawnParametersList)
        {
            if (roomEnemySpawnParameters.dungeonLevel == dungeonLevel)
            {
                return Random.Range(roomEnemySpawnParameters.minTotalEnemiesToSpawn,
                    roomEnemySpawnParameters.maxTotalEnemiesToSpawn);
            }
        }

        return 0;
    }

    public RoomEnemySpawnParameters GetRoomEnemySpawnParameters(DungeonLevelSO dungeonLevel)
    {
        foreach (RoomEnemySpawnParameters roomEnemySpawnParameters in roomLevelEnemySpawnParametersList)
        {
            if (roomEnemySpawnParameters.dungeonLevel == dungeonLevel)
            {
                return roomEnemySpawnParameters;
            }
        }

        return null;
    }
}