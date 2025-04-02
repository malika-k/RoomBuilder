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

    [SerializeField] private GameObject cornerMarker;
    [SerializeField] private List<GameObject> roomCorners;
    [SerializeField] private LineRenderer lineRenderer;
    private HitTestResult lastFoundSurface;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

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
            lineRenderer.SetPosition(roomCorners.Count, hit.Position);
        }

        // if we have 4 corners, connect the last corner to the first corner
        if (roomCorners.Count == 4) {
            lineRenderer.loop = true;
            // to do: turn off plane finder or hit test, stop connecting line.
        }
    }

    // independently draw lines using room corners


    public void CreateCornerMarker() {
        if (lastFoundSurface == null) {
            return;
        }

        GameObject corner = Instantiate(cornerMarker, lastFoundSurface.Position, lastFoundSurface.Rotation);
        roomCorners.Add(corner);

        lineRenderer.positionCount = roomCorners.Count + 1; // +1 to show the line from the last corner to the current hit
        lineRenderer.SetPosition(roomCorners.Count - 1, corner.transform.position);
        lineRenderer.SetPosition(roomCorners.Count, lastFoundSurface.Position);
    }
}
