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

    private int chat = 0;
    private List<string> chats;

    CanvasController canvasController;

    private void Awake()
    {
        Activate();
        input = new Input();
        chats = new();
        canvasController = FindObjectOfType<CanvasController>(true);

        input.General.Enable();

        input.General.Interact.started += i =>
        {
            if (textFinished)
                Next();
        };
    }
    public void OpenChat(List<string> chat, string chatterName)
    {
        active = true;
        Activate();

        Chat(chat, chatterName);
    }
    public void Chat(List<string> chat, string chatterName)
    {
        this.chat = 0;
        chats = new(chat);

        continueText.gameObject.SetActive(false);
        textFinished = false;
        textBox.text = "";
        this.chatterName.text = chatterName;

        StartCoroutine(Write(chat[this.chat]));
    }
    private IEnumerator Write(string text)
    {
        foreach(char letter in text)
        {
            textBox.text += letter;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }

        textFinished = true;
        continueText.gameObject.SetActive(true);
    }
    public void Next()
    {
        chat++;
        textBox.text = "";
        if (chat == chats.Count)
            ClosePanel();
        else
            StartCoroutine(Write(chats[chat]));
    }
    public void ClosePanel()
    {
        active = false;
        textBox.text = "";
        chatterName.text = "";
        textFinished = false;
        canvasController.StopTalk();
        Activate();
    }
}
