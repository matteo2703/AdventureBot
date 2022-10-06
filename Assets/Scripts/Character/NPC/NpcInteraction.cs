using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    [SerializeField] public string npcName;
    [SerializeField] public List<string> chats;

    public Quest startQuest;
    public Quest endQuest;
}
