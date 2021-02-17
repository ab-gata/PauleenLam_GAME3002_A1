using UnityEngine.Assertions;
using UnityEngine;
using System;

public class BasketBallProjectile : MonoBehaviour
{
    // Variables for objects involved, camera is used to determine the throw angle
    private Rigidbody m_rBall = null;
    [SerializeField]
    private Camera cam;


    // Variables for calculations
    [SerializeField]
    private float InputVelocity = 30.0f;
    private Vector3 m_vInitialVelocity = Vector3.zero;

    private Vector3 m_vAngleVector = Vector3.zero;
    private float m_fAngle = 0;

    private float m_fTime = 0;

    // Variables for tracking state of basketball
    private bool m_bSimulating = false;

    // Variable for object that will help point out throwing direction and strength(distance without disturbance)
    private GameObject m_landingDisplay = null;

    // Interface stuff
    private UIFunctions m_interface = null;
    private float fPoints = 0;
    private float fScore = 0;

    private void Start()
    {
        // Set up reference to interface
        m_interface = GetComponent<UIFunctions>();
        Physics.gravity = new Vector3(0, -9.8F, 0);

        // Set up reference to ball
        m_rBall = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rBall, "ERROR: No rigid body on basketball...");

        CreateLandingDisplay();
    }

    private void Update()
    {
        // Adjust input velocity based on arrow keys
        if (Input.GetKeyDown("up"))
        {
            InputVelocity += 1.0f;
        }
        if (Input.GetKeyDown("down"))
        {
            InputVelocity -= 1.0f;
        }

        // Update calculations (+ landing position)
        if (!m_bSimulating)
        {
            UpdateLandingPosition();
            CalculateLaunchProperties();
        }

        // Start/Stop simulation using right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            m_bSimulating = !m_bSimulating;

            if (!m_bSimulating)
            {
                ResetBall();
            }
            else
            {
                StartSimulation();
            }
        }

        // Request UI update
        if (m_interface != null)
        {
            m_interface.OnRequestUpdateUI(InputVelocity, fPoints, fScore, m_fAngle * Mathf.Rad2Deg);
        }
    }

    // Function gets called everytime the ball goes into the basket
    // There is a trigger box that is attached to the center of the hoop
    private void OnTriggerEnter(Collider targetObj)
    {
        // The trigger box will be labeled as Hoop
        if (targetObj.gameObject.tag == "Hoop")
        {
            // Increase the number of shots made
            fPoints++;

            // Increase the score by distance from the starting position
            fScore += targetObj.transform.position.z - -4;

            // Get refernce to the parent to access a function that will update the hoop and allow for progression
            HoopBehaviour hoop = targetObj.GetComponentInParent<HoopBehaviour>();
            hoop.MarkScore();
        }
    }



    // --------------------------------------------------------------------------------------------------// START
    // PROJECTILE----------------------------------------------------------------------------------------//
    // --------------------------------------------------------------------------------------------------//
    void CalculateLaunchProperties()
    {
        CalculateAngle();
        // Apply the player's input velocity to the magnitude of the initial velocity
        m_vInitialVelocity = new Vector3(InputVelocity * m_vAngleVector.x, InputVelocity * m_vAngleVector.y, InputVelocity * m_vAngleVector.z);

        // Time is foundfor the landing display
        m_fTime = 2f * (0f - m_vInitialVelocity.y / Physics.gravity.y);
    }

    void CalculateAngle()
    {
        // Get the camera's position, and adjust lower so the ball appears to bet shot upwards rather than striaght forward
        Vector3 vAdjustedCam = cam.transform.position;
        vAdjustedCam.y += -5;

        // Mark the axis to get angle from, z-axis
        Vector3 vHorizonal = new Vector3(0, 0, 1);

        // Angle vector found by finding the difference between the ball and camera
        m_vAngleVector = (m_rBall.transform.position - vAdjustedCam).normalized;

        // Calculate actual angle with formula
        float value = Vector3.Dot(m_vAngleVector, vHorizonal);
        float theta = Mathf.Acos(value);
        m_fAngle = theta;
    }
    void StartSimulation()
    {
        // Start the simulation by setting the new velocity to the ball 
        // and allowing the ball to be affected by gravity
        m_rBall.velocity = m_vInitialVelocity;
        m_rBall.useGravity = true;
    }

    void ResetBall()
    {
        // Stop the simulation by setting the velocity to 0  
        // preventing the ball to be affected by gravity
        // and returning the ball to the original position
        m_rBall.useGravity = false;
        m_rBall.velocity = new Vector3(0, 0, 0);
        m_rBall.transform.position = new Vector3(0, 1, -4);
    }
    // --------------------------------------------------------------------------------------------------// END
    // PROJECTILE----------------------------------------------------------------------------------------//
    // --------------------------------------------------------------------------------------------------//





    // --------------------------------------------------------------------------------------------------// START
    // DISPLAY FUNCTIONS-----(As learned in class)-------------------------------------------------------//
    // --------------------------------------------------------------------------------------------------//
    private void CreateLandingDisplay()
    {
        // create the object and set what it looks like
        m_landingDisplay = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        m_landingDisplay.transform.position = Vector3.zero;
        m_landingDisplay.transform.localScale = new Vector3(0.3f, 0.1f, 0.3f);

        m_landingDisplay.GetComponent<Renderer>().material.color = Color.red;
        m_landingDisplay.GetComponent<Collider>().enabled = false;
    }

    private void UpdateLandingPosition()
    {
        m_landingDisplay.transform.position = GetLandingPosition();
    }

    private Vector3 GetLandingPosition()
    {
        // The point the ball will be at along same y-axis after thrown
        Vector3 vFlatVel = m_vInitialVelocity;
        vFlatVel.y = 0f;
        vFlatVel *= m_fTime;

        return transform.position + vFlatVel;
    }
    // --------------------------------------------------------------------------------------------------// END
    // DISPLAY FUNCTIONS---------------------------------------------------------------------------------//
    // --------------------------------------------------------------------------------------------------//

}
