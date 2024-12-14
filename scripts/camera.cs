using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform carTransform; // Reference to the car's transform
    public Vector3 offset = new Vector3(0, 5, -10); // Position offset (above and behind)
    public float followSpeed = 5f; // Speed at which the camera follows

    void LateUpdate()
    {
        if (carTransform == null) return;

        // Compute the target position for the camera
        Vector3 targetPosition = carTransform.position + offset;

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Maintain the camera's original rotation
        transform.rotation = Quaternion.Euler(30, 0, 0); // Adjust the angles as needed
    }
}
