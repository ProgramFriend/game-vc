using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShop : Interactable
{
    public override void Interact()
    {
        ShopSystem.Instance.OpenShop();
    }
    
}
