using UnityEngine;

public class PlayerManager : MonoBehaviour, IDataManager
{
    public static PlayerManager Instance;
    Input input;

    [HideInInspector] Animator animator;

    [HideInInspector] public bool isInteracting;

    public bool chatInteractable;
    [SerializeField] GameObject talkingButton;
    ChattingPanel chatPanel;

    public bool agricultureInteraction;
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

        animator = GetComponent<Animator>();
        chatPanel = FindObjectOfType<ChattingPanel>(true);

        input = new Input();
        input.General.Enable();
        input.General.Interact.started += input => { Interaction(); };
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
            TerrainSlot slot = other.GetComponent<TerrainSlot>();
            slot.interactable = true;
            if (!slot.plowded && ItemsManager.Instance.FindItemInInventory(hoe))
            {
                plowderButton.gameObject.SetActive(true);
                plowderButton.slot = slot;
            }
            else if (slot.plowded && !slot.planted)
            {
                seedButton.gameObject.SetActive(true);
                seedButton.slot = slot;
            }
            else if (slot.plowded && slot.planted && slot.growed)
            {
                harvestButton.gameObject.SetActive(true);
                harvestButton.slot = slot;
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
