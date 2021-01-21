using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


// IT SETUPS CLASSES, FOR ITEMDATABES TO READ ITEMS.
// CREATES ITEM OBJECT/CLASS

public class Item
{
    public enum ItemTypes { Weapon, Consumable, Quest, Armor, Rune}
    public List<StatBase> Stats { get; set; }
    public string ObjectSlug { get; set; }
    public string Description { get; set; }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public ItemTypes ItemType { get; set; }
    public bool Stackable { get; set; }
    public string Action { get; set; }
    public string ItemName { get; set; }
    public int ID { get; set; }
    public bool ItemModifier { get; set; } //Does that Item affects mc stats
    public bool Sellable { get; set; }
    public int Cost { get; set; }
    public int Level { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public Item(List<StatBase> _Stats, string _ObjectSlug, string _ItemName, string _Description, ItemTypes _ItemType, bool _Stackable, string _Action, int _ID, bool _ItemModifier, bool _Sellable, int _Cost, int _Level)
    {
        this.Stats = _Stats;
        this.ObjectSlug = _ObjectSlug;
        this.ItemName = _ItemName;
        this.Description = _Description;
        this.ItemType = _ItemType;
        this.Stackable = _Stackable;
        this.Action = _Action;
        this.ID = _ID;
        this.ItemModifier = _ItemModifier;
        this.Sellable = _Sellable;
        this.Cost = _Cost;
        this.Level = _Level;
    }
    public Item()
    {
        this.ID = -1;
    }
}
