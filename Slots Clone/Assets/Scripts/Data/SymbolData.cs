[System.Serializable]
public class SymbolData
{
    public int id;
    public string name;
    public int[] payouts;
    // Constructor
    public SymbolData(int id, string name, int[] payouts)
    {
        this.id = id;
        this.name = name;
        this.payouts = payouts;
    }
}
