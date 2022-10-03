using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    [SerializeField] public string npcName;
    [SerializeField] public List<string> chats;

    Input input;
    CanvasController controller;
    [SerializeField] GameObject talkingButton;
    ChattingPanel chatPanel;
    public bool interactable;

    public Quest startQuest;
    public Quest endQuest;

    private void Awake()
    {
        input = new Input();
        input.General.Enable();
        controller = FindObjectOfType<CanvasController>(true);
        chatPanel = FindObjectOfType<ChattingPanel>(true);
        talkingButton.SetActive(false);

        input.General.Interact.started += i =>
        {
            if (interactable && !controller.isTalking)
                controller.Talk();
        };

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            talkingButton.SetActive(true);
            interactable = true;

            chatPanel.npc = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            talkingButton.SetActive(false);
            interactable = false;
            if(chatPanel.active)
                chatPanel.ClosePanel();
        }
    }
}
