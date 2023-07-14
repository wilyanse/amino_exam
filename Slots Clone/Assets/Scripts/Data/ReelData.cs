[System.Serializable]
public class ReelData
{
    public int id;
    public string name;
    public int[] payouts;

    // Constructor
    public ReelData(int id, string name, int[] payouts)
    {
        this.id = id;
        this.name = name;
        this.payouts = payouts;
    }
}
