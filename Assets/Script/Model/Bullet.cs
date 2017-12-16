
public class Bullet : Item
{

    private int damage;
    private int velocity;
    private int stackSize;
    private int minumunLvl;

    public Bullet(string name, string description, int minLvl, int stkSize, int damage, int velocity)
    {
        this.damage = damage;
        this.velocity = velocity;
        this.stackSize = stkSize;
        this.minumunLvl = minLvl;
    }

    public Bullet(){


    }

    public int Damage { get; set; }
    public int Velocity { get; set; }
    public int StackSize { get; set; }
    public int MinimunLvl { get; set; }

}