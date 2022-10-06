using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChattingPanel : Panel
{
    [SerializeField] public TextMeshProUGUI textBox;
    [SerializeField] public TextMeshProUGUI continueText;
    [SerializeField] public TextMeshProUGUI chatterName;

    private Input input;
    public bool textFinished;
    private float timeBetweenCharacters = 0.05f;
    private float speedBoost = 1f;

    public int chat = 0;
    public List<string> chats;
    public string chatter;

    public NpcInteraction npc;

    private void Awake()
    {
        Activate();
        input = new Input();
        chats = new();
        chat = 0;

        input.General.Enable();

        input.General.Interact.started += i =>
        {
            if (textFinished)
                Next();
        };
        input.General.Interact.performed += i =>
        {
            if (!textFinished)
                speedBoost = 3f;
        };
        input.General.Interact.canceled += i =>
        {
            speedBoost = 1f;
        };
    }
    public void OpenChat()
    {
        active = true;
        Activate();

        Chat();
    }
    public void Chat()
    {
        chats = new(npc.chats);
        chatter = npc.npcName;
        chat = 0;

        continueText.gameObject.SetActive(false);
        textFinished = false;
        textBox.text = "";
        chatterName.text = chatter;

        StartCoroutine(Write(chats[chat]));
    }
    private IEnumerator Write(string text)
    {
        foreach(char letter in text)
        {
            textBox.text += letter;
            yield return new WaitForSeconds(timeBetweenCharacters / speedBoost);
        }

        textFinished = true;
        continueText.gameObject.SetActive(true);
    }
    public void Next()
    {
        chat++;
        textBox.text = "";
        textFinished = false;
        continueText.gameObject.SetActive(false);

        if (chat == chats.Count)
        {
            if (npc.endQuest != null && npc.endQuest.inProgress)
                npc.endQuest.EndQuest();

            if (npc.startQuest != null)
                npc.startQuest.AcceptQuest();

            ClosePanel();
        }
        else
            StartCoroutine(Write(chats[chat]));
    }
    public void ClosePanel()
    {
        active = false;
        textBox.text = "";
        chatterName.text = "";
        textFinished = false;
        CanvasController.Instance.StopTalk();
        Activate();
    }
}
