using UnityEngine.Assertions;
using UnityEngine;

public class HoopBehaviour : MonoBehaviour
{
    private Rigidbody m_rHoop = null;

    protected float fLevel = 0;


    // Start is called before the first frame update
    void Start()
    {
        m_rHoop = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rHoop, "ERROR: No rigid body on hoop...");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = m_rHoop.velocity;
        Vector3 p = m_rHoop.transform.position;


        if (fLevel == 0)
        {
            v = new Vector3(-1, 0, 0);
        }

            if (fLevel == 1)
        {
            if (p.x > p.x + 10)
            {
                v = new Vector3(-1, 0, 0);
            }
            if (p.x > p.x - 10)
            {
                v = new Vector3(1, 0, 0);
            }
        }

        m_rHoop.velocity = v;
        Debug.Log(m_rHoop.velocity);
    }
}
