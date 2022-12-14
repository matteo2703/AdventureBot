using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] Input playerControls;
    [HideInInspector] PlayerLocomotion playerLocomotion;
    [HideInInspector] AnimatorManager animatorManager;
    [HideInInspector] PlayerStats playerStats;

    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public Vector2 cameraInput;

    [HideInInspector] public float cameraInputX;
    [HideInInspector] public float cameraInputY;

    [HideInInspector] public float moveAmount;
    [HideInInspector] public float verticalInupt;
    [HideInInspector] public float horizontalInupt;
    public float sprintTime = 5f;

    [HideInInspector] public bool sprintInput;
    [HideInInspector] public bool jumpInput;
    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerStats = GetComponent<PlayerStats>();
    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new Input();

            playerControls.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            playerControls.Player.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.Player.Sprint.performed += i => sprintInput = true;
            playerControls.Player.Sprint.canceled += i => sprintInput = false;

            playerControls.Player.Jump.performed += i => jumpInput = true;
            playerControls.Player.Jump.canceled += i => jumpInput = false;
        }

        playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }
    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintInput();
        HandleJumpInput();
    }
    private void HandleMovementInput()
    {
        verticalInupt = movementInput.y;
        horizontalInupt = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInupt) + Mathf.Abs(verticalInupt));
        animatorManager.UpdateAnimatorValue(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintInput()
    {
        if (sprintInput && moveAmount > 0.5f && sprintTime > 0)
            playerLocomotion.isSprinting = true;
        else
            playerLocomotion.isSprinting = false;
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerLocomotion.HandleJumping();
        }
    }
}
