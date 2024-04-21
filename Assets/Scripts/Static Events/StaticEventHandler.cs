using System;
using UnityEngine;

public static class StaticEventHandler 
{
    //Room Changed Event
    public static event Action<RoomChangedEventArgs> OnRoomChanged;

    public static void CallRoomChangedEvent(Room room)
    {
        OnRoomChanged?.Invoke(new RoomChangedEventArgs(){ room = room});
    }
}

public class RoomChangedEventArgs : EventArgs
{
    public Room room;
}
