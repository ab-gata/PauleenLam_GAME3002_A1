using UnityEngine.Assertions;
using UnityEngine;
using System;

public class BasketBallProjectile : MonoBehaviour
{
    private Rigidbody m_rBall = null;
    [SerializeField]
    private Camera cam;


    [SerializeField]
    private float InputVelocity = 30.0f;
    private Vector3 m_vInitialVelocity = Vector3.zero;
    private Vector3 m_vAngleVector = Vector3.zero;

    private float m_fAngle = 0;
    private float m_fMaxHeight = 0;
    private float m_fTime = 0;

    private bool m_bIsGrounded = true;
    private bool m_bSimulating = false;

    private GameObject m_landingDisplay = null;

    // Interface stuff
    private UIFunctions m_interface = null;
    private float fScore = 0;

    private void Start()
    {
        m_interface = GetComponent<UIFunctions>();
        Physics.gravity = new Vector3(0, -9.8F, 0);

        m_rBall = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rBall, "ERROR: No rigid body on basketball...");

        CreateLandingDisplay();
    }

    private void Update()
    {
        // Adjust input velocity
        if (Input.GetKeyDown("up"))
        {
            InputVelocity += 1.0f;
        }
        if (Input.GetKeyDown("down"))
        {
            InputVelocity -= 1.0f;
        }

        // Update calculations (+ landing position)
        CalculateLaunchProperties();
        if (!m_bSimulating)
        {
            UpdateLandingPosition();
        }

        // Start/Stop simulation
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
            m_interface.OnRequestUpdateUI(InputVelocity, fScore, m_fAngle * Mathf.Rad2Deg);
        }
    }

    void OnCollisionEnter(Collision targetObj)
    {
        if(targetObj.gameObject.tag == "Ground")
        {
            Debug.Log("hit the ground");
        }
    }

    private void OnTriggerEnter(Collider targetObj)
    {
        if (targetObj.gameObject.tag == "Hoop")
        {
            fScore++;
            Debug.Log("point!");
        }
    }



    // --------------------------------------------------------------------------------------------------// START
    // PROJECTILE----------------------------------------------------------------------------------------//
    // --------------------------------------------------------------------------------------------------//
    void CalculateLaunchProperties()
    {
        CalculateAngle();

        m_vInitialVelocity = new Vector3(InputVelocity * m_vAngleVector.x, InputVelocity * m_vAngleVector.y, InputVelocity * m_vAngleVector.z);

        m_fTime = 2f * (0f - m_vInitialVelocity.y / Physics.gravity.y);

        // Extra Information
        m_fMaxHeight = m_vInitialVelocity.y * Physics.gravity.y / m_fTime;
    }

    void CalculateAngle()
    {
        Vector3 vAdjustedCam = cam.transform.position;
        vAdjustedCam.y += -5;

        Vector3 vHorizonal = new Vector3(0, 0, 1);

        m_vAngleVector = (m_rBall.transform.position - vAdjustedCam).normalized;

        float value = Vector3.Dot(m_vAngleVector, vHorizonal);

        float theta = Mathf.Acos(value);
        m_fAngle = theta;
    }
    void StartSimulation()
    {
        m_rBall.velocity = m_vInitialVelocity;
        m_rBall.useGravity = true;
    }

    void ResetBall()
    {
        m_rBall.useGravity = false;
        m_rBall.velocity = new Vector3(0, 0, 0);
        m_rBall.transform.position = new Vector3(0, 1, -4);
        Quaternion target = Quaternion.Euler(0, 0, 0);
        m_rBall.rotation = Quaternion.Slerp(transform.rotation, target, 1);
    }

    // Util
    Vector3 Multiply(Vector3 v1, Vector3 v2)
    {
        float x = v1.x * v2.x;
        float y = v1.y * v2.y;
        float z = v1.z * v2.z;


        return new Vector3(x, y, z);
    }

    // --------------------------------------------------------------------------------------------------// END
    // PROJECTILE----------------------------------------------------------------------------------------//
    // --------------------------------------------------------------------------------------------------//





    // --------------------------------------------------------------------------------------------------// START
    // DISPLAY FUNCTIONS---------------------------------------------------------------------------------//
    // --------------------------------------------------------------------------------------------------//
    private void CreateLandingDisplay()
    {
        m_landingDisplay = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        m_landingDisplay.transform.position = Vector3.zero;
        m_landingDisplay.transform.localScale = new Vector3(0.3f, 0.1f, 0.3f);

        m_landingDisplay.GetComponent<Renderer>().material.color = Color.blue;
        m_landingDisplay.GetComponent<Collider>().enabled = false;
    }

    private void UpdateLandingPosition()
    {
        if (m_landingDisplay != null && m_bIsGrounded)
        {
            m_landingDisplay.transform.position = GetLandingPosition();
        }
    }

    private Vector3 GetLandingPosition()
    {
        Vector3 vFlatVel = m_vInitialVelocity;
        vFlatVel.y = 0f;
        vFlatVel *= m_fTime;

        return transform.position + vFlatVel;
    }
    // --------------------------------------------------------------------------------------------------// END
    // DISPLAY FUNCTIONS---------------------------------------------------------------------------------//
    // --------------------------------------------------------------------------------------------------//

}
