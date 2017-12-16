
public class WeaponSet
{

    private long id;
    private User user;
    private Weapon primary;
    private Weapon secondary;

    public WeaponSet()
    {

    }

    public WeaponSet(long id, User user, Weapon primary, Weapon secondary)
    {
        this.id = id;
        this.user = user;
        this.primary = primary;
        this.secondary = secondary;
    }

    public long Id { get; set; }
    public User User { get; set; }
    public Weapon Primary { get; set; }
    public Weapon Secondary { get; set; }

}