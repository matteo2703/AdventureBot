using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasController : MonoBehaviour
{
    Input input;
    PlayerStats playerStats;

    public bool inPause;
    public bool viewQuest;
    public bool inventoryView;
    public bool gameOver;
    public bool craftingView;
    public bool isTalking;
    public PausePanelManager pausePanelManager;
    public QuestPanel questPanel;
    public InventoryPanel inventoryPanel;
    public GameOverPanel gameOverPanel;
    public CraftingPanel craftingPanel;
    public ChattingPanel chatPanel;

    private void Awake()
    {
        inPause = false;
        pausePanelManager = FindObjectOfType<PausePanelManager>(true);
        questPanel = FindObjectOfType<QuestPanel>(true);
        inventoryPanel = FindObjectOfType<InventoryPanel>(true);
        gameOverPanel = FindObjectOfType<GameOverPanel>(true);
        craftingPanel = FindObjectOfType<CraftingPanel>(true);
        chatPanel = FindObjectOfType<ChattingPanel>(true);

        input = new Input();
        playerStats = FindObjectOfType<PlayerStats>(true);
    }

    private void OnEnable()
    {
        input.General.Enable();
        input.General.Exit.started += i => ChangePause();
        input.General.Quest.started += i => ChangeQuest();
        input.General.Inventory.started += i => ChangeInventory();
    }
    private void OnDisable()
    {
        input.General.Disable();
    }

    private void Update()
    {
        if (playerStats.actualLife <= 0)
            GameOver();
    }

    public void ChangePause()
    {
        if (!gameOver)
        {
            inPause = !inPause;
            if (inPause)
                pausePanelManager.SetPause();
            else
                pausePanelManager.Resume();
        }
    }
    public void ChangeQuest()
    {
        if (!inPause && !inventoryView && !gameOver && !craftingView && !isTalking)
        {
            viewQuest = !viewQuest;
            if (viewQuest)
                questPanel.OpenPanel();
            else
                questPanel.ClosePanel();
        }
    }
    public void ChangeInventory()
    {
        if (!inPause && !viewQuest && !gameOver && !craftingView && !isTalking)
        {
            inventoryView = !inventoryView;
            if (inventoryView)
                inventoryPanel.OpenPanel();
            else
                inventoryPanel.ClosePanel();
        }
    }
    public void ChangeCrafting()
    {
        if(!inPause && !viewQuest && !gameOver && !inventoryView && !isTalking)
        {
            craftingView = !craftingView;
            if (craftingView)
                craftingPanel.OpenPanel();
            else
                craftingPanel.ClosePanel();
        }
    }
    public void Talk(List<string> chat, string chatter)
    {
        if(!inPause && !viewQuest && !gameOver && !inventoryView && !craftingView && !isTalking)
        {
            isTalking = true;
            if (isTalking)
                chatPanel.OpenChat(chat, chatter);
        }
    }
    public void StopTalk()
    {
        StartCoroutine(StopTalking());
    }

    public IEnumerator StopTalking()
    {
        yield return new WaitForSeconds(0.5f);
        isTalking = false;
    }
    private void GameOver()
    {
        gameOver = true;
        gameOverPanel.ShowPanel();
    }
}
