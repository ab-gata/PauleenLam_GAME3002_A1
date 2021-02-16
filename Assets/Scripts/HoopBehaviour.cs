using UnityEngine.Assertions;
using UnityEngine;

public class HoopBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject m_rHoop = null;

    protected float fLevel = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //if (m_rHoop.transform.position.x > 10)
        //    m_rHoop.transform.position.x++;
        //if (transform.position.x < -10)
        //    m_rHoop.velocity = new Vector3(1, 0, 0);
    }
}
