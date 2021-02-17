using UnityEngine.Assertions;
using UnityEngine;

public class HoopBehaviour : MonoBehaviour
{
    // To track basket score count for progression
    private float fScored = 0;
    private float fLevel = 0;

    // Speed of the basket
    private Vector3 speed = new Vector3(0, 0, 0);


    void FixedUpdate()
    {
        MoveHoop();
    }

    // A function that handles the movement of the hoops based on level, which is effected by the amount of time scored
    void MoveHoop()
    {

        if (fLevel == 1) // Moves the basket L/R on x-axis
        {
            // First check the position to know when to switch velocity
            Rigidbody temp = GetComponentInChildren<Rigidbody>();
            // Move hoop left/right
            if (temp.transform.position.x > 5)
            {
                speed.x = -1;
            }
            if (temp.transform.position.x < -5)
            {
                speed.x = 1;
            }
        }

        if (fLevel == 2) // Moves the basket U/D on y-axis
        {
            // First check the position to know when to switch velocity
            Rigidbody temp = GetComponentInChildren<Rigidbody>();
            // Return to the center on x axis
            if (temp.transform.position.x > 0)
            {
                speed.x = -1;
            }
            if (temp.transform.position.x < 0)
            {
                speed.x = 1;
            }
            // Move hoop up/down
            if (temp.transform.position.y > 3)
            {
                speed.y = -1;
            }
            if (temp.transform.position.y < 0)
            {
                speed.y = 1;
            }
        }

        if (fLevel == 3) // Moves the basket L/R on x-axis but faster
        {
            // First check the position to know when to switch velocity
            Rigidbody temp = GetComponentInChildren<Rigidbody>();
            // Move hoop left/right
            if (temp.transform.position.x > 5)
            {
                speed.x = -2;
            }
            if (temp.transform.position.x < -5)
            {
                speed.x = 2;
            }
        }

        if (fLevel == 4) // Moves the basket L/R on x-axis and U/D on y-axis
        {
            // First check the position to know when to switch velocity
            Rigidbody temp = GetComponentInChildren<Rigidbody>();
            // Move hoop left/right
            if (temp.transform.position.x > 5)
            {
                speed.x = -2;
            }
            if (temp.transform.position.x < -5)
            {
                speed.x = 2;
            }
            // Move hoop up/down
            if (temp.transform.position.y > 3)
            {
                speed.y = -1;
            }
            if (temp.transform.position.y < 0)
            {
                speed.y = 1;
            }
        }

        // Loop to set the velocity for each child in the parent (parts that make up the Hoop)
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = speed;
            }
        }
    }

    // A function that the collider can call to tally the number of time scored in the specific hoop.
    // Will update the level of the hoop when appropriate
    public void MarkScore()
    {
        // Increase the scored tally
        fScored++;

        // Set new level
        if (fScored == 1)
        {
            fLevel++;

            // Loop to allow movement for each child in the parent (parts that make up the Hoop)
            // And set the first speed
            foreach (Transform child in transform)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Allow the movement
                    rb.isKinematic = false;
                    // Restrict movement except for x position
                    rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY |
                                     RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                                     RigidbodyConstraints.FreezeRotationX;
                    // Set the starting speed
                    speed = new Vector3(1, 0, 0);
                }
            }
        }
        if (fScored == 4)
        {
            fLevel++;

            // Loop to allow movement for each child in the parent (parts that make up the Hoop)
            // And set the first speed
            foreach (Transform child in transform)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Allow the movement
                    rb.isKinematic = false;
                    // Restrict movement except for y position
                    rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                     RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                                     RigidbodyConstraints.FreezeRotationY;
                    // Set the starting speed
                    speed = new Vector3(0, 1, 0);
                }
            }
        }

        if (fScored == 7)
        {
            fLevel++;

            // Loop to allow movement for each child in the parent (parts that make up the Hoop)
            // And set the first speed
            foreach (Transform child in transform)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Allow the movement
                    rb.isKinematic = false;
                    // Restrict movement except for x position
                    rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY |
                                     RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                                     RigidbodyConstraints.FreezeRotationX;
                    // Set the starting speed
                    speed = new Vector3(2, 0, 0);
                }
            }
        }

        if (fScored == 10)
        {
            fLevel++;

            // Loop to allow movement for each child in the parent (parts that make up the Hoop)
            // And set the first speed
            foreach (Transform child in transform)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Allow the movement
                    rb.isKinematic = false;
                    // Restrict movement except for y position
                    rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                     RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
                                     RigidbodyConstraints.FreezeRotationY;
                    // Set the starting speed
                    speed = new Vector3(1, 2, 0);
                }
            }
        }
    }
}