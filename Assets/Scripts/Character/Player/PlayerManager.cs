using UnityEngine;

public class PlayerManager : MonoBehaviour, IDataManager
{
    [HideInInspector] InputManager inputManager;
    [HideInInspector] CameraManager cameraManager;
    [HideInInspector] Animator animator;
    [HideInInspector] PlayerLocomotion playerLocomotion;

    [HideInInspector] public bool isInteracting;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        cameraManager = FindObjectOfType<CameraManager>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Time.timeScale != 0f)
            inputManager.HandleAllInputs();
    }
    private void FixedUpdate()
    {
        if (Time.timeScale != 0f)
            playerLocomotion.HandleAllMovement();
    }
    private void LateUpdate()
    {
        if (Time.timeScale != 0f)
        {
            cameraManager.HandleAllCameraMovement();

            isInteracting = animator.GetBool("isInteracting");
            playerLocomotion.isJumping = animator.GetBool("isJumping");
            animator.SetBool("isGrounded", playerLocomotion.isGrounded);
        }
    }

    public void SaveGame(GenericGameData data)
    {
        data.player = new SerializableObjectPosition(transform.position, transform.rotation);
    }

    public void LoadGame(GenericGameData data)
    {
        transform.position = data.player.position;
        transform.rotation = data.player.rotation;
    }
}
