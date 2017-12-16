using UnityEngine;
using System.Collections;

public class Weapon : Item
{
    protected double damageRatio;
    protected Bullet bulletType;
    protected double magazineSize;
    protected double realodTime;
    protected double firingRate;
    protected string rarity;

    public Weapon(string name, string description, string minumunLvl, string stackSize, double damageRatio, double magazineSize, double realodTime, double firingRate, string rarity)
    {
        this.damageRatio = damageRatio;
        //this.bulletType = bulletType;
        this.magazineSize = magazineSize;
        this.realodTime = realodTime;
        this.firingRate = firingRate;
        this.rarity = rarity;
    }

    public Weapon(){}

    public string Rarity { get; set; }
    public double DamageRatio { get; set; }
    public Bullet BulletType { get; set; }
    public double MagazineSize { get; set; }
    public double RealodTime { get; set; }
    public double FiringRate { get; set; }


}