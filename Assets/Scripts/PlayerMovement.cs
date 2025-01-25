using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 5f; // Constant forward speed
    public float strafeSpeed = 5f;  // Speed for strafing left and right
    public float strafeLimit = 5f;  // Boundary limit for strafing

    private float currentStrafePosition;

    void Update()
    {
        // Move forward constantly
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Get strafe input (A/D or Left Arrow/Right Arrow)
        float strafeInput = Input.GetAxis("Horizontal");

        // Calculate new strafe position
        currentStrafePosition = Mathf.Clamp(
            transform.position.x + strafeInput * strafeSpeed * Time.deltaTime,
            -strafeLimit,
            strafeLimit
        );

        // Apply strafing movement
        transform.position = new Vector3(
            currentStrafePosition,
            transform.position.y,
            transform.position.z
        );
    }
}
