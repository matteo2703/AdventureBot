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

    CanvasController canvasController;

    private void Awake()
    {
        Activate();
        input = new Input();
        canvasController = FindObjectOfType<CanvasController>(true);

        input.General.Enable();

        input.General.Interact.started += i =>
        {
            if (textFinished)
                ClosePanel();
        };
    }
    public void Chat(string chat, string chatterName)
    {
        continueText.gameObject.SetActive(false);
        textFinished = false;
        textBox.text = "";
        gameObject.SetActive(true);
        this.chatterName.text = chatterName;

        StartCoroutine(Write(chat));
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
    public void ClosePanel()
    {
        textBox.text = "";
        chatterName.text = "";
        textFinished = false;
        canvasController.StopTalk();
        gameObject.SetActive(false);
    }
}
