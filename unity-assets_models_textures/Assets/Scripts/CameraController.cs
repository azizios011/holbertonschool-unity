using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSpeed = 10f;
    public float rotationSpeed = 100f;
    public bool allwoFreeLook = true;
    public bool requireRightClick = true;

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        if ( target == null )
        {
            Debug.LogError("CameraController: No target assigned.");
            enabled = false;
            return;
        }
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        FollowTarget();

        if (allwoFreeLook)
        {
            FreeLook();
        }
    }

    void FollowTarget()
    {
        Vector3 desierdPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desierdPosition, followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    void FreeLook()
    {
        bool caRotate = !requireRightClick || (requireRightClick && Input.GetMouseButtonDown(1));
        if (caRotate)
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            pitch += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -35f, 60f);

            transform.rotation = Quaternion.Euler(yaw, pitch, 0f);
            offset = transform.rotation * new Vector3(0, 0, -offset.magnitude);
        }
    }
}
