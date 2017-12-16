using UnityEngine;
using System.Collections;

public class Item
{
    private string name;
    private string description;
    private int minumunLvl;
    private int stackSize;
    private Bullet bulletType;
    private double magazineSize;
    private double realodTime;
    private double firingRate;
    private string rarity;
    private string type;
    private int id;

    // Constructor
    public Item()
    {
       
    }
    public int ID { get; set; }
    public string Name{ get; set; }
    public string Description { get; set; }
    public int MinumunLvl { get; set; }
    public int StackSize { get; set; }
    public Bullet BulletType { get; set; }
    public double MagazineSize { get; set; }
    public double RealodTime { get; set; }
    public double FiringRate { get; set; }
    public string Rarity { get; set; }

}