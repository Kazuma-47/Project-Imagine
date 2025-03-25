using UnityEngine;
using UnityEngine.UI;

sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float turningSpeed;
    private Rigidbody rigidBody;
    private bool isMoving;
    private float movementSpeed;
    private void Start() => rigidBody = GetComponent<Rigidbody>();

    public void Move(float inputDirection)
    {
        movementSpeed = Mathf.Lerp(rigidBody.velocity.magnitude, maxMovementSpeed, accelerationSpeed * Time.deltaTime);

        Vector3 forwardMovement = transform.forward * inputDirection * movementSpeed * Time.deltaTime;
        Vector3 newVelocity = rigidBody.velocity + forwardMovement;
        newVelocity = Vector3.ClampMagnitude(newVelocity, maxMovementSpeed);

        rigidBody.velocity = new Vector3(newVelocity.x, rigidBody.velocity.y, newVelocity.z);
    }

    private void FixedUpdate()
    {
        print(rigidBody.velocity);
        if(rigidBody.velocity != Vector3.zero && !isMoving) 
            rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, Vector3.zero, Time.deltaTime * 1.5f);
    }

    public void Turn(float steerDirection)
    {
        if (steerDirection != 0 && isMoving)
        {
            float turnStrength = turningSpeed * Time.deltaTime * (rigidBody.velocity.magnitude / maxMovementSpeed);
            transform.Rotate(Vector3.up, steerDirection * turnStrength);
        }
    }

    public void SetIsMoving(bool value) => isMoving = value;
}


