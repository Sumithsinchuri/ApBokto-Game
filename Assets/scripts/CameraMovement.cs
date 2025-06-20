using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float trailDistance = 5.0f;
    public float heightOffset = 3.0f;
    public float cameraDelay = 0.02f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 followPos = target.position - target.forward * trailDistance;
        followPos.y += heightOffset;
        transform.position += (followPos - transform.position) * cameraDelay;
        transform.LookAt(target.transform);
    }
}
