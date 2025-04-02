using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

public class RoomManager : MonoBehaviour
{
    // need to enable corner creation mode - need to have multiple modes.
    // need to have a way to save/load the corners to a file
    // need to have a way to clear the corners
    [SerializeField] public List<GameObject> roomCorners;
    public HitTestResult lastFoundSurface;

    [SerializeField] private GameObject cornerMarker;

    [SerializeField] private DrawManager drawManager;

    /**
     * Used in Vuforia's Automatic Hit Test - runs every time a surface is detected
     *
     * Keep track of the most recently found surface
     */
    public void UpdateSurfaceHit(HitTestResult hit) {
        if (hit == null) {
            return;
        }

        lastFoundSurface = hit;

        if (roomCorners.Count > 0) {
            // draw a line from the last corner to the current hit
            drawManager.DrawPreviewLine();
        }

        // if we have 4 corners, connect the last corner to the first corner
        if (roomCorners.Count == 4) {
            drawManager.FinishRoomLine();
            // to do: turn off plane finder or hit test, stop connecting line.
        }
    }
    /**
     * Called when the user clicks on the screen
     */
    public void CreateCornerMarker() {
        if (lastFoundSurface == null) {
            return;
        }

        GameObject corner = Instantiate(cornerMarker, lastFoundSurface.Position, lastFoundSurface.Rotation);
        roomCorners.Add(corner);
        drawManager.AddCornerToLine();
    }
}
