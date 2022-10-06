using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [HideInInspector] Input playerControls;

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
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
        AnimatorManager.Instance.UpdateAnimatorValue(0, moveAmount, PlayerLocomotion.Instance.isSprinting);
    }

    private void HandleSprintInput()
    {
        if (sprintInput && moveAmount > 0.5f && sprintTime > 0)
            PlayerLocomotion.Instance.isSprinting = true;
        else
            PlayerLocomotion.Instance.isSprinting = false;
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            PlayerLocomotion.Instance.HandleJumping();
        }
    }
}
