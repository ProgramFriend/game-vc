using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestGiver : NPC
{
    public int WhichQuest { get; set; }
    public bool AssignedQuest { get; set; } //false once the quest giver realises that quest was completed
    public bool GotQuest { get; set; }
    public bool Helped { get; set; } //if quest was completed

    [SerializeField] private GameObject quests;
    [SerializeField] private string[] questTypes;

    [SerializeField]private int[] questCodes; //must match length of questTypes;

    private Quest Quest { get; set; }

    private void Start()
    {
        GotQuest = false;
        CheckForQuest();
        EventHandler.OnNewQuestCode += CheckForQuest;
    }

    public override void Interact()
    {
        if (!AssignedQuest && !Helped)
        {
            AssignQuest();
        }
        else if(AssignedQuest && !Helped)
        {
            CheckQuest();
        }
        else
        {
            DialogueSystem.Instance.AddNewDialogue(Quest.completedDialogue, npcName);
        }
    }
    public void GetQuest()
    {
        if (questTypes.Length > WhichQuest && questCodes[WhichQuest] <= DialogueSystem.Instance.PlayerQuestCode)
        {
            Debug.Log(name + " grabbed new quest");
            Invoke(nameof(ShowAvailableAnime), 2);
            Quest = (Quest)quests.AddComponent(System.Type.GetType(questTypes[WhichQuest]));
            WhichQuest++;
            Helped = false;
            GotQuest = true;
        }
        else
        {
            HideAllAnime();
        }
    }
    void CheckForQuest()
    {
        if (questCodes[WhichQuest] <= DialogueSystem.Instance.PlayerQuestCode && !GotQuest)
        {
            Invoke(nameof(ShowAvailableAnime), 2);
            GetQuest();
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        DialogueSystem.Instance.AddNewDialogue(Quest.beforeQuest, npcName);
    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            GotQuest = false;
            DialogueSystem.Instance.AddNewDialogue(Quest.rewardDialogue, npcName);
            HideAllAnime();
            CheckForQuest();
        }
        else
        {
            DialogueSystem.Instance.AddNewDialogue(Quest.inProgressDialogue, npcName);
        }
    }

    void ShowAvailableAnime()
    {
        Debug.Log(name + " IS AVAILABLE!");
    }
    void HideAllAnime()
    {
        Debug.Log(name + " NOT AVAILABLE!");
    }

}
