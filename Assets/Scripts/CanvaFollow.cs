
using UnityEngine;

public class CanvaFollow : MonoBehaviour
{
    [SerializeField] Vector3 distanceFromCamera;
    [SerializeField] float smoothSpeed;
    Transform cameraTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform = Camera.main.transform;
        Vector3 targetPosition = cameraTransform.position + (cameraTransform.forward * distanceFromCamera.z)
        + (cameraTransform.up * distanceFromCamera.y) + (cameraTransform.right * distanceFromCamera.x);

        transform.position = targetPosition;

        float cameraYaw = cameraTransform.eulerAngles.y;
        float cameraPitch = cameraTransform.eulerAngles.x;

        Quaternion targetRotation = Quaternion.Euler(cameraPitch, cameraYaw, 0);


    }


    void LateUpdate()
    {
        Vector3 targetPosition = cameraTransform.position + (cameraTransform.forward * distanceFromCamera.z)
        + (cameraTransform.up * distanceFromCamera.y) + (cameraTransform.right * distanceFromCamera.x);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

        float cameraYaw = cameraTransform.eulerAngles.y;
        float cameraPitch = cameraTransform.eulerAngles.x;

        Quaternion targetRotation = Quaternion.Euler(cameraPitch, cameraYaw, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
