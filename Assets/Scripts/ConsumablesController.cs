using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesController : MonoBehaviour
{
    //CharacterStats stats;
    Player player;
    private void Start()
    {
        player = GetComponent<Player>();
    }
    public void ConsumeItem(Item item)
    {
        GameObject itemToConsume = Instantiate(Resources.Load<GameObject>("Prefabs/Consumables/" + item.ObjectSlug));
        if (item.ItemModifier == true)
        {
            itemToConsume.GetComponent<IConsumable>().Consume(player);
            Destroy(itemToConsume);
            Debug.Log("You consumed " + item.ItemName);
        }
        else
        {
            itemToConsume.GetComponent<IConsumable>().Consume();
            Destroy(itemToConsume);
            Debug.Log("You consumed with no modifier");

        }

    }

}
