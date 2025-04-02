using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private LineRenderer roomOutline;

    [SerializeField] private RoomManager roomManager;

    // Start is called before the first frame update
    void Start()
    {
        roomOutline.loop = false;

    }

    /**
     * Show a preview line
     * i.e. draw a line from the last corner to the current hit on surface
     */
    public void DrawPreviewLine() {
        roomOutline.SetPosition(roomManager.roomCorners.Count, roomManager.lastFoundSurface.Position);
    }

    /**
     * Called when a new corner is created
     * This will add the new corner to the line renderer
     */
    public void AddCornerToLine() {
        roomOutline.positionCount = roomManager.roomCorners.Count + 1; // +1 to show the line from the last corner to the current hit
        roomOutline.SetPosition(roomManager.roomCorners.Count - 1, roomManager.lastFoundSurface.Position);
        roomOutline.SetPosition(roomManager.roomCorners.Count, roomManager.lastFoundSurface.Position);
    }
    /**
     * Called when the room is finished
     * This will make the line renderer loop
     * and connect the last corner to the first corner
     */
    public void FinishRoomLine() {
        roomOutline.loop = true;
    }
}
