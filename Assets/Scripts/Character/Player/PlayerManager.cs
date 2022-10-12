using UnityEngine;

public class PlayerManager : MonoBehaviour, IPlayerManager
{
    public static PlayerManager Instance;
    Input input;

    [HideInInspector] Animator animator;

    [HideInInspector] public bool isInteracting;

    public bool chatInteractable;
    [SerializeField] GameObject talkingButton;
    ChattingPanel chatPanel;

    public bool agricultureInteraction;
    public TerrainSlot terrain;
    [SerializeField] InventoryItem hoe;
    [SerializeField] PlowButton plowderButton;
    [SerializeField] SeedButton seedButton;
    [SerializeField] HarvestButton harvestButton;

    public bool craftInteractable;
    [SerializeField] GameObject craftingButton;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DeactivateButtons();

        animator = GetComponent<Animator>();
        chatPanel = FindObjectOfType<ChattingPanel>(true);

        input = new Input();
        input.General.Enable();
        input.General.Interact.started += input => { Interaction(); };
    }
    private void DeactivateButtons()
    {
        talkingButton.SetActive(false);
        plowderButton.gameObject.SetActive(false);
        seedButton.gameObject.SetActive(false);
        harvestButton.gameObject.SetActive(false);
        craftingButton.SetActive(false);
    }
    private void Update()
    {
        if (Time.timeScale != 0f)
            InputManager.Instance.HandleAllInputs();
    }
    private void FixedUpdate()
    {
        if (Time.timeScale != 0f)
            PlayerLocomotion.Instance.HandleAllMovement();
    }
    private void LateUpdate()
    {
        if (Time.timeScale != 0f)
        {
            CameraManager.Instance.HandleAllCameraMovement();

            isInteracting = animator.GetBool("isInteracting");
            PlayerLocomotion.Instance.isJumping = animator.GetBool("isJumping");
            animator.SetBool("isGrounded", PlayerLocomotion.Instance.isGrounded);
        }
    }
    public void Interaction()
    {
        if (chatInteractable && !CanvasController.Instance.isTalking)
            CanvasController.Instance.Talk();
        else if (craftInteractable)
            CanvasController.Instance.ChangeCrafting();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            talkingButton.SetActive(true);
            chatInteractable = true;

            chatPanel.npc = other.GetComponent<NpcInteraction>();
        }
        else if (other.CompareTag("Terrain"))
        {
            agricultureInteraction = true;
            terrain = other.GetComponent<TerrainSlot>();
            terrain.interactable = true;
            if (!terrain.plowded && ItemsManager.Instance.FindItemInInventory(hoe))
            {
                plowderButton.gameObject.SetActive(true);
                plowderButton.slot = terrain;
            }
            else if (terrain.plowded && !terrain.planted)
            {
                seedButton.gameObject.SetActive(true);
                seedButton.slot = terrain;
            }
            else if (terrain.plowded && terrain.planted && terrain.growed)
            {
                harvestButton.gameObject.SetActive(true);
                harvestButton.slot = terrain;
            }
        }
        else if (other.CompareTag("Crafting"))
        {
            craftingButton.SetActive(true);
            craftInteractable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            talkingButton.SetActive(false);
            chatInteractable = false;
            if (chatPanel.active)
                chatPanel.ClosePanel();
        }
        else if (other.CompareTag("Terrain"))
        {
            plowderButton.gameObject.SetActive(false);
            seedButton.gameObject.SetActive(false);
            harvestButton.gameObject.SetActive(false);

            terrain = null;
            agricultureInteraction = false;
            plowderButton.slot = null;
            seedButton.slot = null;
            harvestButton.slot = null;
        }
        else if (other.CompareTag("Crafting"))
        {
            craftingButton.SetActive(false);
            craftInteractable = false;
        }
    }

    public void SaveGame(GenericPlayerData data)
    {
        data.player = new SerializableObjectPosition(transform.position, transform.rotation);
    }

    public void LoadGame(GenericPlayerData data)
    {
        transform.position = data.player.position;
        transform.rotation = data.player.rotation;
    }
}
