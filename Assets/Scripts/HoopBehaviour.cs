using UnityEngine.Assertions;
using UnityEngine;

public class HoopBehaviour : MonoBehaviour
{
    private float fLevel = 0;
    private Vector3 speed = new Vector3(1, 0, 0);


    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        MoveHoop();
        //InvokeRepeating("MoveHoop", 0.0f, 10.0f);
    }

    void MoveHoop()
    {

        if (fLevel == 0)
        { 
            Rigidbody temp = GetComponentInChildren<Rigidbody>();
            if (temp.transform.position.x > 5)
            {
                speed = new Vector3(-1, 0, 0);
            }
            if (temp.transform.position.x < -5) 
            {
                speed = new Vector3(1, 0, 0);
            }
            Debug.Log(temp.transform.position.x);

            foreach (Transform child in transform)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = speed;
                }
            }
        }

        if (fLevel == 1)
        {

        }
    }
}
