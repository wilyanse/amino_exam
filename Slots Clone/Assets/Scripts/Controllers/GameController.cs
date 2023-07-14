using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private SymbolData[] symbolOptions;
    public ReelController[] reelControllers;
    void Start()
    {
        symbolOptions = CreateSymbolData();
        for(int i = 0; i < reelControllers.Length; i++){
            SymbolData[] shuffledSymbols = ShuffleSymbols(symbolOptions);
            SymbolData[] randomSequence = GenerateRandomSequence(shuffledSymbols);
            reelControllers[i].SetSymbolSequence(randomSequence);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    private SymbolData[] ShuffleSymbols(SymbolData[] symbols)
    {
        // Create a copy of the symbols array
        SymbolData[] shuffledSymbols = new SymbolData[symbols.Length];
        System.Array.Copy(symbols, shuffledSymbols, symbols.Length);

        // Shuffle the symbols using Fisher-Yates algorithm
        for (int i = shuffledSymbols.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            SymbolData temp = shuffledSymbols[i];
            shuffledSymbols[i] = shuffledSymbols[randomIndex];
            shuffledSymbols[randomIndex] = temp;
        }

        return shuffledSymbols;
    }

    private SymbolData[] GenerateRandomSequence(SymbolData[] symbols)
    {
        SymbolData[] sequence = new SymbolData[symbols.Length];

        for (int i = 0; i < symbols.Length; i++)
        {
            sequence[i] = symbols[i];
        }

        return sequence;
    }

}
