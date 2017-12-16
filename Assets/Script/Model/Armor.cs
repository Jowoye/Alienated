using UnityEngine;
using System.Collections;

public class Armor : Item
{

    protected string armorRating;
    protected int durability;
    protected string armorType;
    protected string rarity;

    public Armor(string name, string description, string minumunLvl, string stackSize, string armorRating, int durability, string armorType, string rarity)
    {
        this.armorRating = armorRating;
        this.durability = durability;
        this.armorType = armorType;
        this.rarity = rarity;
    }

    public Armor()
    {

        this.armorRating = "";
        this.durability = 0;
        this.armorType = "";
        this.rarity = "";

    }

    public string ArmorRating { get; set; }
    public int Durability { get; set; }
    public string ArmorType { get; set; }
    public string Rarity { get; set; }

}