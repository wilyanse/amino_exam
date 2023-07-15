[System.Serializable]
public class SymbolData
{
    public int id; // used as symbol sprite id as well as id to identify symbol
    public string name; // not used
    public int[] payouts; // used for calculating the payout
    // Constructor
    public SymbolData(int id, string name, int[] payouts)
    {
        this.id = id;
        this.name = name;
        this.payouts = payouts;
    }
}
