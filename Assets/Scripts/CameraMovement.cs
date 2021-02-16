using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 previousPosition;

    private bool m_bSimulating = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_bSimulating = !m_bSimulating;
        }

        if (!m_bSimulating)
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

                cam.transform.position = transform.position;

                cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
                cam.transform.Translate(new Vector3(0, 2, -5));

                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
        }
    }
}
