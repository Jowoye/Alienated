using System.Collections.Generic;

public class Inventory
{
    /*
    private long id;
    private User user;
    private Slot s1;
    private Slot s2;
    private Slot s3;
    private Slot s4;
    private Slot s5;
    private Slot s6;
    private Slot s7;
    private Slot s8;
    private Slot s9;
    private Slot s10;

*/

    public Inventory()
    {

    }

    public long ID { get; set; }
    public User User { get; set; }
    public Slot S1 { get; set; }
    public Slot S2 { get; set; }
    public Slot S3 { get; set; }
    public Slot S4 { get; set; }
    public Slot S5 { get; set; }
    public Slot S6 { get; set; }
    public Slot S7 { get; set; }
    public Slot S8 { get; set; }
    public Slot S9 { get; set; }
    public Slot S10 { get; set; }

    public List<Slot> GetListOfSlots(){

        List<Slot> slts = new List<Slot>();

        slts.Add(this.S1);
        slts.Add(this.S2);
        slts.Add(this.S3);
        slts.Add(this.S4);
        slts.Add(this.S5);
        slts.Add(this.S6);
        slts.Add(this.S7);
        slts.Add(this.S8);
        slts.Add(this.S9);
        slts.Add(this.S10);

        return slts;
    }
}