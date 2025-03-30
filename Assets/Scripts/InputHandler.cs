using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Controls controls;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        controls = new Controls();
        controls.PlayerControls.Enable();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        float movementInput = controls.PlayerControls.Movement.ReadValue<float>();
        float turnInput = controls.PlayerControls.Turn.ReadValue<float>();

        controls.PlayerControls.Movement.performed += (InputAction.CallbackContext context) => playerMovement.SetIsMoving(true);
        
        playerMovement.Move(movementInput);
        playerMovement.Turn(turnInput);


        controls.PlayerControls.Movement.canceled += (InputAction.CallbackContext context) => playerMovement.SetIsMoving(false);
    }
}
