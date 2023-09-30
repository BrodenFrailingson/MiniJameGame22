using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float rotationSpeed = 30.0f;
    public float rotationAngle = 45.0f; // Maximum rotation angle from the center
    private Quaternion startRotation;
    private Transform player;
    private bool isRotatingRight = true;

    private void Start()
    {
        startRotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.up, directionToPlayer);

        // Check if the player is within the rotation angle
        if (angleToPlayer <= rotationAngle)
        {
            // Player is within the field of view, stop rotating and alert
            // You can trigger the alert here, similar to the previous example.

            // Example: Display an alert message
            Debug.Log("Player detected!");
        }
        else
        {
            // Player is not within the field of view, continue rotating
            RotateCCTV();
        }
    }

    private void RotateCCTV()
    {
        // Rotate the CCTV left and right
        float rotationAmount = isRotatingRight ? rotationSpeed * Time.deltaTime : -rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationAmount);

        // Check if the CCTV reached its maximum rotation angle
        float currentRotationAngle = Quaternion.Angle(startRotation, transform.rotation);
        if (currentRotationAngle >= rotationAngle)
        {
            // Reverse the rotation direction
            isRotatingRight = !isRotatingRight;
        }
    }
}