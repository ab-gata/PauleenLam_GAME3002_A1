using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Variables for the camera
    [SerializeField]
    private Camera cam;
    private Vector3 previousPosition;

    // Need to keep track of when to move the camera
    // Only move when the ball is NOT thrown
    private bool m_bSimulating = false;

    void Update()
    {
        // Activate/Deactivate the simulation by pressing the Right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            m_bSimulating = !m_bSimulating;
        }

        // If NOT simulating, allow for the movement of camera using Left mouse button
        if (!m_bSimulating)
        {
            // On click, get the first position of the mouse pointer
            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
            // On hold, get the change in position and apply to camera
            if (Input.GetMouseButton(0))
            {
                // Get direction by finding the difference
                Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

                // Set camera position to that of the basketball
                cam.transform.position = transform.position;

                // Rotate the camera based off earlier input
                //    -the screen goes from 0,0 to 1,1, so we multiply by 180 (one side veiwable of the basketball)
                cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                //    -set the x with Space.World to prevent losing sense of upright direction
                cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
                //    -move the camera up and behind the basketball so we can see the ball
                cam.transform.Translate(new Vector3(0, 2, -5));

                //    -after applying the changes, set the previous position to new position
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
        }
    }
}
