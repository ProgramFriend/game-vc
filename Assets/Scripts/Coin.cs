using System.Collections;
using UnityEngine;

public class Coin : PickUpItem
{
    public int PQC;

    public override void Interact()
    {
        DialogueSystem.Instance.PlayerQuestCode = PQC;
        EventHandler.GotNewQuestCode();
    }
}
