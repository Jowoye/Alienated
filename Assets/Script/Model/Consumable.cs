using UnityEngine;
using System.Collections;

public class Consumable : Item
{
    protected string type;
    protected string consumeTimer;
    protected int hpReg;

    public Consumable(string name, string description, string minumunLvl, string stackSize, string type, string consumeTimer, int hpReg)
    {
        this.type = type;
        this.consumeTimer = consumeTimer;
        this.hpReg = hpReg;
    }

    public Consumable() { }

    public string Type { get; set; }
    public string ConsumeTimer { get; set; }
    public int HpReg { get; set; }

}