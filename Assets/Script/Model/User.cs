using System;

public class User
{


    private long id;
    private string firstName;
    private string lastName;
    private string username;
    private string email;
   // private DateTime sinceDate;
   // private DateTime lastLogin;
    private ArmorSet armorSet;
    private WeaponSet wepSet;
    private Inventory inventory;
    private int healthPoints;
    private int lvl;
    private int exp;
    private int credits;
    private int paidCredits;

    public User()
    {

    }

    public User(string username, string email, string password, long id, string firstName, string lastName, int healthPoints, int lvl, int exp, int credits, int paidCredits)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.healthPoints = healthPoints;
        this.lvl = lvl;
        this.exp = exp;
        this.credits = credits;
        this.paidCredits = paidCredits;
        //this.lastLogin = DateTime.Now;
        this.username = username;
        this.email = email;
        //this.sinceDate = DateTime.Today;
        this.armorSet = new ArmorSet();
        this.wepSet = new WeaponSet();
        this.inventory = new Inventory();
        this.healthPoints = 100;
        this.lvl = 1;
        this.exp = 0;
        this.credits = 1000;
        this.paidCredits = 10;
    }

    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
   // public DateTime SinceDate { get; set; }
   // public DateTime LastLogin { get; set; }
    public ArmorSet ArmorSet { get; set; }
    public WeaponSet WepSet { get; set; }
    public Inventory Inventory { get; set; }
    public int HealthPoints { get; set; }
   // public long Lvl { get; set; }
    public Level Lvl { get; set; }

    public int Exp { get; set; }
    public int Credits { get; set; }
    public int PaidCredits { get; set; }

}