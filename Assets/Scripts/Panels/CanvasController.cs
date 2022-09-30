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
    public PausePanelManager pausePanelManager;
    public QuestPanel questPanel;
    public InventoryPanel inventoryPanel;
    public GameOverPanel gameOverPanel;
    public CraftingPanel craftingPanel;

    private void Awake()
    {
        inPause = false;
        pausePanelManager = FindObjectOfType<PausePanelManager>(true);
        questPanel = FindObjectOfType<QuestPanel>(true);
        inventoryPanel = FindObjectOfType<InventoryPanel>(true);
        gameOverPanel = FindObjectOfType<GameOverPanel>(true);
        craftingPanel = FindObjectOfType<CraftingPanel>(true);

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
        if (!inPause && !inventoryView && !gameOver && !craftingView)
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
        if (!inPause && !viewQuest && !gameOver && !craftingView)
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
        if(!inPause && !viewQuest && !gameOver && !inventoryView)
        {
            craftingView = !craftingView;
            if (craftingView)
                craftingPanel.OpenPanel();
            else
                craftingPanel.ClosePanel();
        }
    }
    private void GameOver()
    {
        gameOver = true;
        gameOverPanel.ShowPanel();
    }
}
