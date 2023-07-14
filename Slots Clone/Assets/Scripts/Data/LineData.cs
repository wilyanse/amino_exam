[System.Serializable]
public class LineData
{
    public int[] line; // Line configuration (0 = no symbol, 1 = symbol)
    public int minSymbolCount; // Minimum number of symbols on the line for a win
    public int[] payouts; // Payout values based on the number of symbols
}