using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float turningSpeed;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 forwardMovement = transform.forward * movementSpeed * Time.deltaTime;
            Vector3 newVelocity = rigidBody.velocity + forwardMovement;

            newVelocity = Vector3.ClampMagnitude(newVelocity, maxMovementSpeed);

            rigidBody.velocity = new Vector3(newVelocity.x, rigidBody.velocity.y, newVelocity.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turningSpeed * Time.deltaTime* (rigidBody.velocity.magnitude / maxMovementSpeed));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime * (rigidBody.velocity.magnitude / maxMovementSpeed));
        }
        else
        {
            rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, Vector3.zero, Time.deltaTime * 2f);
        }
    }
}


