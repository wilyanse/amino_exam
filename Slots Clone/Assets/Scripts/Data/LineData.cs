[System.Serializable]
public class LineData
{
    public int[] reelInd; // reel index for check which rows of each reel to check

    public LineData(int[] reelInd){
        this.reelInd = reelInd;
    }
}