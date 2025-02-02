using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField]
    private Room startingRoom;
    [SerializeField, ReadOnly]
    private List<Room> rooms = new List<Room>();
    [SerializeField, ReadOnly]
    private Room currentRoom = null;
    private CameraPanning cameraControl;

    public static event System.Action<Room, Room> OnRoomChange;
    public static Location Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        InitAllRoom();
    }
    private void InitAllRoom()
    {
        cameraControl = FindFirstObjectByType<CameraPanning>();
        rooms = new List<Room>(GetComponentsInChildren<Room>());

        if (startingRoom == null) startingRoom = rooms[0];
        ChangeRoom(startingRoom);
    }

    public void ChangeRoom(Room newRoom)
    {
        if (newRoom == null || newRoom == currentRoom)
            return;

        Room previousRoom = currentRoom;
        currentRoom = newRoom;

        ExitRoom(previousRoom);
        EnterRoom(currentRoom);

        OnRoomChange?.Invoke(previousRoom, currentRoom);
    }
    private void ExitRoom(Room room)
    {
        //cleanup
    }
    private void EnterRoom(Room newRoom)
    {
        cameraControl.SetBoundary(newRoom.roomBoundary);
        Camera.main.transform.position = newRoom.roomBoundary.bounds.center;
    }
}