using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    public GameObject alertPanel;
    public float alertDuration = 5.0f;
    private bool playerDetected = false;

    private SecurityCamera SecurityCam; // Reference to the CCTVRotation script

    private void Start()
    {
        // Get a reference to the CCTVRotation script
        SecurityCam = transform.parent.GetComponent<SecurityCamera>();

        // Deactivate the alert panel at the start
        if (alertPanel != null)
        {
            alertPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerDetected)
        {
            // Player detected
            playerDetected = true;

            // Hide the alert panel
            if (alertPanel != null)
            {
                alertPanel.SetActive(false);
            }

            // Pause the camera rotation
            SecurityCam.rotationSpeed = 0;

            // Trigger the level restart after a certain duration
            Invoke("RestartLevel", alertDuration);

            Debug.Log("player detected");
        }
    }

    private void RestartLevel()
    {
        // Restart the level
    }
}
