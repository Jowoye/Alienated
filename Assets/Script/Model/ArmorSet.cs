public class ArmorSet
{

    private long id;
    private User user;
    private Armor head;
    private Armor chest;
    private Armor legs;

    public ArmorSet()
    {

    }

    public ArmorSet(long id, User user, Armor head, Armor chest, Armor legs)
    {
        this.id = id;
        this.user = user;
        this.head = head;
        this.chest = chest;
        this.legs = legs;
    }

    public long Id { get; set; }
    public User User { get; set; }
    public Armor Head { get; set; }
    public Armor Chest { get; set; }
    public Armor Legs { get; set; }

}