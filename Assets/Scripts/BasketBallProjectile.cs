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
    private Transform m_tFinalPosition = null;


    private bool m_bIsGrounded = true;
    private bool m_bSimulating = false;

    private GameObject m_landingDisplay = null;

    private void Start()
    {
        m_rBall = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rBall, "ERROR: No rigid body on basketball...");

        CreateLandingDisplay();
    }

    private void Update()
    {
        CalculateLaunchProperties();
        UpdateLandingPosition();

        if (Input.GetMouseButtonDown(1))
        {
            if (m_bSimulating)
            {
                m_bSimulating = false;
                ResetBall();
            }
            else
            {
                m_bSimulating = true;
                StartSimulation();
            }
        }
    }



    // --------------------------------------------------------------------------------------------------// START
    // PROJECTILE----------------------------------------------------------------------------------------//
    // --------------------------------------------------------------------------------------------------//
    void CalculateLaunchProperties()
    {
        CalculateAngle();

        m_vInitialVelocity = new Vector3(InputVelocity * Mathf.Cos(m_fAngle), InputVelocity * Mathf.Sin(m_fAngle), InputVelocity * Mathf.Cos(m_fAngle));

        m_fTime = 2f * (0f - m_vInitialVelocity.y / Physics.gravity.y);
    }

    void CalculateAngle()
    {
        Vector3 vAdjustedCam = cam.transform.position;
        vAdjustedCam.y += -4;

        Vector3 vHorizonal = new Vector3(0, 0, 1);

        m_vAngleVector = (m_rBall.transform.position - vAdjustedCam).normalized;

        float value = Vector3.Dot(m_vAngleVector, vHorizonal);

        float theta = Mathf.Acos(value);
        m_fAngle = theta;

        Debug.Log(theta * Mathf.Rad2Deg);
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
    }

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
        m_landingDisplay.transform.localScale = new Vector3(1f, 0.1f, 1f);

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
