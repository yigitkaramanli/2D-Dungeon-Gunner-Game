using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNodeSO : ScriptableObject
{
    [HideInInspector]
    public string id;

    [HideInInspector]
    public List<string> parentRoomNodeList = new List<string>();

    [HideInInspector]
    public List<string> childRoomNodeList = new List<string>();

    [HideInInspector]
    public RoomNodeGraphSO roomNodeGraph;

    public RoomNodeTypeSO roomNodeType;

    [HideInInspector]
    public RoomNodeTypeListSO roomNodeTypeList;
}
