using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5.0f;
    public float rotationSpeed = 700.0f;
    public Camera mainCamera;

    private CharacterController characterController;
    private Vector3 moveDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        if (mainCamera != null )
        {
            mainCamera = Camera.main;
        }
    }
    private void Update()
    {
        float HzInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vInput + right * HzInput).normalized;
        moveDirection *= Speed;

        characterController.Move(moveDirection * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed  * Time.deltaTime);
        }
    }
}
