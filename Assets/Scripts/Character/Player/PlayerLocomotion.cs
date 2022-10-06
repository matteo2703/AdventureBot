using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    public static PlayerLocomotion Instance;

    [HideInInspector] Vector3 moveDirection;
    [HideInInspector] Transform cameraObject;
    [HideInInspector] Rigidbody playerRigidbody;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float maxDistance = 0.5f;
    public LayerMask groundLayer;
    public float rayCastHeightOffset = 0.5f;
    public float maxSafeDistance = 0.3f;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;
    public bool isSwimming;

    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5f;
    public float sprintSpeed = 7f;
    public float rotationSpeed = 15f;
    public float runningTime = 0f;

    [Header("Jump Speeds")]
    public float jumpHeight = 2f;
    public float gravityIntensity = -9.81f;

    [Header("Swimming")]
    public LayerMask waterLayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }
    public void HandleAllMovement()
    {
        if (!isSwimming)
        {
            HandleFallingAndLanding();
            AnimatorManager.Instance.animator.SetBool("isSwimming", false);
        }
        else
            HandleSwim();

        if (PlayerManager.Instance.isInteracting)
            return;

        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        if (isJumping)
            return;

        moveDirection = cameraObject.forward * InputManager.Instance.verticalInupt;
        moveDirection = moveDirection + cameraObject.right * InputManager.Instance.horizontalInupt;
        moveDirection.Normalize();
        moveDirection.y = 0f;

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintSpeed;
            InputManager.Instance.sprintTime -= Time.deltaTime;
            runningTime += Time.deltaTime;

            if (InputManager.Instance.sprintTime < 0)
            {
                InputManager.Instance.sprintTime = 0;
                if (runningTime >= PlayerStats.Instance.maxSprintTime * 90/100)
                {
                    PlayerStats.Instance.MoreResistance();
                    runningTime = 0f;
                    PlayerStats.Instance.maxSprintTime += PlayerStats.Instance.addAbilityValue;
                }
            }
        }
        else
        {
            runningTime = 0f;
            if (InputManager.Instance.sprintInput == false)
            {
                InputManager.Instance.sprintTime += Time.deltaTime;
                if (InputManager.Instance.sprintTime > PlayerStats.Instance.maxSprintTime)
                    InputManager.Instance.sprintTime = PlayerStats.Instance.maxSprintTime;
            }
            
            if (InputManager.Instance.moveAmount >= 0.5f)
                moveDirection = moveDirection * runningSpeed;
            else
                moveDirection = moveDirection * walkingSpeed;
        }

        Vector3 movementVelocity = moveDirection;
        movementVelocity -= movementVelocity * AnimatorManager.Instance.slowFactor;
        playerRigidbody.velocity = movementVelocity;
    }
    private void HandleRotation()
    {
        if (isJumping)
            return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * InputManager.Instance.verticalInupt;
        targetDirection = targetDirection + cameraObject.right * InputManager.Instance.horizontalInupt;
        targetDirection.Normalize();
        targetDirection.y = 0f;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

        transform.rotation = playerRotation;
    }
    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        Vector3 targetPosition;
        raycastOrigin.y = raycastOrigin.y + rayCastHeightOffset;
        targetPosition = transform.position;

        if (!isGrounded && !isJumping)
        {
            if(!PlayerManager.Instance.isInteracting)
                AnimatorManager.Instance.PlayTargetAnimation("Fall", true);

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, maxDistance, groundLayer))
        {
            if (!isGrounded && !PlayerManager.Instance.isInteracting && !isSwimming)
                AnimatorManager.Instance.PlayTargetAnimation("Land", true);

            if (inAirTimer > maxSafeDistance)
                FallingDamage(inAirTimer);

            Vector3 raycastHitPoint = hit.point;
            targetPosition.y = raycastHitPoint.y;
            inAirTimer = 0f;
            isGrounded = true;
        }
        else
            isGrounded = false;

        if (isGrounded && !isJumping)
        {
            if (PlayerManager.Instance.isInteracting || InputManager.Instance.moveAmount > 0)
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            else
                transform.position = targetPosition;
        }

    }
    public void FallingDamage(float timeInAir)
    {
        float extraFloatingTime = timeInAir - maxSafeDistance;
        float damage = -Mathf.NextPowerOfTwo((int)(extraFloatingTime * 100));
        PlayerStats.Instance.SetHealth(damage);
        
    }
    public void HandleJumping()
    {
        if (isGrounded && !isSwimming)
        {
            AnimatorManager.Instance.animator.SetBool("isJumping", true);
            AnimatorManager.Instance.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;
        }
    }

    public void HandleSwim()
    {
        AnimatorManager.Instance.animator.SetBool("isSwimming", true);
        inAirTimer = 0f;
    }
}