public class Slot
{

    private long id;
    private int quantity;
    private Item item;
    private string itemClassType;

    public Slot()
    {

    }

    public Slot(long id, int stackSize, Item item, string itemClss)
    {
        this.quantity = stackSize;
        this.item = item;
        this.id=id;
        this.itemClassType = itemClss;
    }

    public long ID { get; set; }  
    public int Quantity { get; set; }
    public Item Item { get; set; }
    public string ItemClassType { get; set; }
}