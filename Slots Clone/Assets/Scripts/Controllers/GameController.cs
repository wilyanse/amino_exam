using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    private SymbolData[] symbolOptions; //global list of symbols to choose from
    public ReelController[] reelControllers; //controls the spin animation of reels, sets reel data, returns data from reel
    private SymbolData[][] spinResults; // matrix that contains spin results from reels
    private LineData[] linePayouts; //global list of payout lines

    private int totalWins; //global int of winnings
    public TextMeshProUGUI winsText; // textUI for winnings

    private int coins; //global int of coins
    public TextMeshProUGUI coinsText; //textUI for coins

    private int bets; // number of bets (number of times the increase bets button was pressed)
    private int totalBets; // bets * payoutlines.Length
    public TextMeshProUGUI betsText; // textUI for totalBets
    public Button increaseBets; // reference to increase button
    public Button decreaseBets; // reference to decrease button
    void Start()
    {
        // winnings initially set to 0 and displayed
        totalWins = 0;
        winsText.text = totalWins.ToString();
        // inital coins and display
        coins = 1000;
        coinsText.text = "Coins: " + coins.ToString();
        
        spinResults = new SymbolData[3][]; // instantiate spin results matrix

        // initialization of reel for each controller
        symbolOptions = CreateSymbolData(); //populated with symbols to choose from
        // for each reel controller, a set reel is passed
        for(int i = 0; i < reelControllers.Length; i++){
            // symbols are shuffled and put into a new array which is passed to the reel controller to initialize
            SymbolData[] shuffledSymbols = ShuffleSymbols(symbolOptions); 
            SymbolData[] randomSequence = GenerateRandomSequence(shuffledSymbols);
            reelControllers[i].SetSymbolSequence(randomSequence);
            reelControllers[i].SetInitialSymbols();
        }
        // incase a custom sequence is set the code below is used along with the GenerateReelSequence function
        //reelControllers[0].SetSymbolSequence(GenerateReelSequence(symbolOptions));

        linePayouts = CreateLineData(); //initializes line payout array

        // initializes bets and edits text
        bets = 1;
        totalBets = bets * linePayouts.Length;
        betsText.text = totalBets.ToString();
        // listeners for bets buttons
        increaseBets.onClick.AddListener(IncreaseBets);
        decreaseBets.onClick.AddListener(DecreaseBets);
    }
    // initializes symboldata
    // modify as needed
    // values are arbitrary; name is unused
    private SymbolData[] CreateSymbolData(){
        return new SymbolData[]
        {
            new SymbolData(0, "A", new int[] {0,0,1,5,10}),
            new SymbolData(1, "B", new int[] {0,0,2,8,25}),
            new SymbolData(2, "C", new int[] {0,0,3,9,45}),
            new SymbolData(3, "D", new int[] {0,0,4,16,80}),
            new SymbolData(4, "E", new int[] {0,0,5,25,150}),
            new SymbolData(5, "F", new int[] {0,0,6,40,400})
        };
    }
    // randomly the symbols in the symbols array to help generate a random sequence
    private SymbolData[] ShuffleSymbols(SymbolData[] symbols)
    {
        // copy of the symbols array
        SymbolData[] shuffledSymbols = new SymbolData[symbols.Length];
        System.Array.Copy(symbols, shuffledSymbols, symbols.Length);

        // Fisher-Yates algorithm for random shuffling
        for (int i = shuffledSymbols.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            SymbolData temp = shuffledSymbols[i];
            shuffledSymbols[i] = shuffledSymbols[randomIndex];
            shuffledSymbols[randomIndex] = temp;
        }

        return shuffledSymbols;
    }
    // copies array sequence to copy by value instead of by reference
    private SymbolData[] GenerateRandomSequence(SymbolData[] symbols)
    {
        SymbolData[] sequence = new SymbolData[symbols.Length];

        for (int i = 0; i < symbols.Length; i++)
        {
            sequence[i] = symbols[i];
        }

        return sequence;
    }
    // returns personalized reel sequence
    // should be in SymbolData form; can create new SymbolData as well
    private SymbolData[] GenerateReelSequence(SymbolData[] symbols)
    {
        return new SymbolData[]{
            symbols[1],
            symbols[2],
            symbols[4],
            new SymbolData(6, "G", new int[] {0,0,7,100,550}),
            symbols[3],
            symbols[0]
        };
    }
    // calculates results after spinning has stopped
    // called by spincontroller after spinning is finished
    public void CalculateResults(){
        // stores results in matrix where spinresults[row][reel]
        for(int i = 0; i < 3; i++){
            SymbolData[] rowResults = new SymbolData[reelControllers.Length];
            for(int j = 0; j < reelControllers.Length; j++){
                rowResults[j] = reelControllers[j].GetVisibleSymbol(i);
            }
            spinResults[i] = rowResults;
        }
        // checks for payout
        CheckLines();
    }
    // custom line data
    // modify as needed
    // arbitrary values
    private LineData[] CreateLineData(){
        return new LineData[]{
            new LineData(new int[] {0,0,0,0,0}), //1
            new LineData(new int[] {1,1,1,1,1}), //2
            new LineData(new int[] {2,2,2,2,2}), //3
            new LineData(new int[] {0,1,2,1,0}), //4
            new LineData(new int[] {2,1,0,1,2}), //5
            new LineData(new int[] {1,0,1,0,1}), //6
            new LineData(new int[] {0,1,0,1,0}), //7
            new LineData(new int[] {1,2,1,2,1}), //8
            new LineData(new int[] {2,1,2,1,2}), //9
            new LineData(new int[] {0,1,1,1,0}), //10
            new LineData(new int[] {1,0,0,0,1}), //11
            new LineData(new int[] {2,1,1,1,2}), //12
            new LineData(new int[] {1,2,2,2,1}), //13
            new LineData(new int[] {0,0,1,0,0}), //14
            new LineData(new int[] {1,1,0,1,1}), //15
            new LineData(new int[] {1,1,2,1,1}), //16
            new LineData(new int[] {2,2,1,2,2}), //17
            new LineData(new int[] {0,2,0,2,0}), //18
            new LineData(new int[] {2,0,2,0,2}), //19
            new LineData(new int[] {0,2,1,2,0}) //20
        };
    }
    // checks if the spinresult contains a payout
    private void CheckLines(){
        for(int i = 0; i < linePayouts.Length; i++){
            int[] curCheck = linePayouts[i].reelInd; //current line being checked for payouts

            SymbolData[] lineResults = new SymbolData[5]; // contains lineResults in SymbolData format

            // dictionary for counting occurrences
            Dictionary<SymbolData, int> countDict = new Dictionary<SymbolData, int>(); 
            SymbolData maxKey = null; // keeps track of key with maxValue
            int maxOccur = 0; // greatest value


            for(int j = 0; j < curCheck.Length; j++){
                lineResults[j] = spinResults[curCheck[j]][j]; // stores into lineResults for SymbolData format
                // adds occurrence if previously seen; otherwise is set to 1
                if (countDict.ContainsKey(lineResults[j])) {
                    countDict[lineResults[j]]++;
                }
                else {
                    countDict[lineResults[j]] = 1;
                }
                // changes maxkey and value if higher occurrence is found
                if(countDict[lineResults[j]] > maxOccur){
                    maxOccur = countDict[lineResults[j]];
                    maxKey = lineResults[j];
                }
            }
            // checks if maxkey and value has a payout
            // adds to totalWins and modifies text
            totalWins += CheckPayout(maxKey, maxOccur);
            winsText.text = totalWins.ToString();
        }
        // adds coins and modifies text
        coins += totalWins;
        coinsText.text = "Coins: " + coins.ToString();
    }
    // checks if payout is present
    // returns payout value
    private int CheckPayout(SymbolData symbol, int occurrences){
        if(occurrences == 0){
            return symbol.payouts[occurrences];
        }
        return symbol.payouts[occurrences-1];
    }
    // called by spincontroller 
    // initializes variables for new spin
    public void NewSpin(){
        coins -= totalBets;
        coinsText.text = "Coins: " + coins.ToString();
        totalWins = 0;
        winsText.text = totalWins.ToString();
    }
    // onclick event of increase bets button
    // increases bets if possible
    // sets button to uninteractable if not possible
    public void IncreaseBets(){
        if(coins > (bets + 1) * linePayouts.Length){
            bets++;
            totalBets = bets * linePayouts.Length;
            betsText.text = totalBets.ToString();

            decreaseBets.interactable = true; // since it was increased, it can be decreased
        } else {
            increaseBets.interactable = false;
        }
        
    }

    // onclick event of decrease bets button
    // decreases bets if possible
    // sets button to uninteractable if not possible
    public void DecreaseBets(){
        if(bets > 1){
            bets--;
            totalBets = bets * linePayouts.Length;
            betsText.text = totalBets.ToString();

            increaseBets.interactable = true; //since it was decreased, it should be increasable
        } else {
            decreaseBets.interactable = false;
        }
    }
}
