using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLevel_", menuName = "Scriptable Objects/Dungeon/Dungeon Level")]
public class DungeonLevelSO : ScriptableObject
{

    #region Header BASIC LEVEL DETAILS
    [Space(10)]
    [Header("BASIC LEVEL DETAILS")]
    #endregion

    #region Tooltip
    [Tooltip("The name for the level")]
    #endregion

    public string levelName;

    #region Header ROOM TEMPLATES FOR LEVEL
    [Space(10)]
    [Header("ROOM TEMPLATES FOR LEVEL")]
    #endregion

    #region Tooltip
    [Tooltip("Populate the list with the room templates that you want to be part of the level." +
        "You need to ensure that room templates are included for all room node types that are specified in the Room Node Graph for the level.")]
    #endregion

    public List<RoomTemplateSO> roomTemplateList;

    #region Header ROOM NODE GRAPHS FOR LEVEL
    [Space(10)]
    [Header("ROOM NODE GRAPHS FOR LEVEL")]
    #endregion

    #region Tooltip
    [Tooltip("Populate this list with the room node graphs which should be randomly selected from for the level.")]
    #endregion

    public List<RoomNodeGraphSO> roomNodeGraphList;


    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(levelName), levelName);

        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomTemplateList), roomTemplateList))
            return;
        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomNodeGraphList), roomNodeGraphList))
            return;

        //Check to make sure that room templates are specified for all the node types in the specified node graph

        //First check that NS corridor, WE Corridor and entrance types have been specified
        bool isNSCorridor = false;
        bool isWECorridor = false;
        bool isEntrance = false;

        foreach (RoomTemplateSO roomTemplate in roomTemplateList)
        {
            if (roomTemplate == null)
                return;

            if (roomTemplate.roomNodeType.isCorridorWE)
                isWECorridor = true;

            if (roomTemplate.roomNodeType.isCorridorNS)
                isNSCorridor = true;

            if (roomTemplate.roomNodeType.isEntrance)
                isEntrance = true;
        }

        if(isNSCorridor == false)
        {
            Debug.Log("In " + this.name.ToString() + " : No N/S Corridor Room Type Specified");
        }

        if (isWECorridor == false)
        {
            Debug.Log("In " + this.name.ToString() + " : No W/E Corridor Room Type Specified");
        }

        if (isEntrance == false)
        {
            Debug.Log("In " + this.name.ToString() + " : No Entrance Room Type Specified");
        }

        foreach (RoomNodeGraphSO roomNodeGraph in roomNodeGraphList)
        {
            if (roomNodeGraph == null)
                return;

            foreach (RoomNodeSO roomNode in roomNodeGraph.roomNodeList)
            {
                if (roomNode == null)
                    continue;

                //Check that a room template has been specified for each node type

                if (roomNode.roomNodeType.isEntrance || roomNode.roomNodeType.isCorridorNS || roomNode.roomNodeType.isCorridorWE ||
                    roomNode.roomNodeType.isCorridor || roomNode.roomNodeType.isNone)
                    continue;

                bool isRoomNodeTypeFound = false;

                foreach (RoomTemplateSO roomTemplate in roomTemplateList)
                {
                    if (roomTemplate == null)
                        continue;

                    if(roomTemplate.roomNodeType == roomNode.roomNodeType)
                    {
                        isRoomNodeTypeFound = true;
                        break;
                    }
                }

                if (!isRoomNodeTypeFound)
                {
                    Debug.Log("In " + this.name.ToString() + " : no room template " + roomNode.roomNodeType.name.ToString() + " found for node graph " + roomNodeGraph.name.ToString());
                }

            }
        }
    }
#endif
    #endregion
}
