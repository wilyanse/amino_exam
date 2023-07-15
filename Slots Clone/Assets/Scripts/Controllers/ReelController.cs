using Random=UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    public float spinSpeed; //how fast the spinning animation occurs
    public Transform[] symbolPositions;  // Array of symbol positions on the reel
    public Sprite[] symbolSprites;  // Array of symbol sprites
    private SymbolData[] symbolSequence; // symbols used by the reel
    public bool isSpinning = false; // spins the reel if true
    private int currentSymbolIndex; // keeps track of which symbol is out of the frame
    private int currentSeqIndex; // keeps track of which symbol of the symbolsequence is to be added next
    public Transform startPos; // starting position of the square of the reel
    public Transform finalPos; // end line of the reel where anything below this is sent back to startpos

    private Transform[] showingPositions; // positions that are visible

    private SymbolData[] showingSymbols; // symbols that are visible; used to return spinresults
    private void Start()
    {
        spinSpeed = Random.Range(25.0f, 100.0f); // randomized speed for each reel

        currentSeqIndex = 0; // starting index for the symbol sequence

        currentSymbolIndex = symbolPositions.Length - 1; // currently the last element of the visible symbols; keep track in order to teleport back to starting position after reaching the end position

        showingPositions = new Transform[symbolPositions.Length - 1]; // initialize list of showing positions to snap the symbols into place
        showingSymbols = new SymbolData[3]; // initialize list of showing symbols
        // Copy the middle elements into the destination array
        // copies by value instead of by reference
        for (int i = 1; i < symbolPositions.Length; i++)
        {
            Transform sourcePos = symbolPositions[i];

            Transform showingPos = new GameObject().transform; // Create a new Transform object
            // copies the transform values into new object
            showingPos.position = sourcePos.position;
            showingPos.rotation = sourcePos.rotation;
            showingPos.localScale = sourcePos.localScale;
            // new object is stored into showing positions
            showingPositions[i - 1] = showingPos;
        }
    }
    private void Update()
    {
        // checks if spinning
        // set by the spincontroller
        if (isSpinning)
        {
            // Move symbols downwards at a constant speed
            float step = spinSpeed * Time.deltaTime;
            for (int i = 0; i < symbolPositions.Length; i++)
            {
                symbolPositions[i].Translate(Vector3.down * step);
            }
            // Check if the top symbol has moved below the reel area
            if (symbolPositions[currentSymbolIndex].position.y < finalPos.position.y)
            {
                // Shift the symbols and update their positions
                UpdateSymbolPositions();
                ShiftSymbols();
            }
        }
    }
    // Start the reel spin animation
    // called by spin controller
    public void Spin()
    {
        if (!isSpinning)
        {
            spinSpeed = Random.Range(25.0f, 100.0f); // randomizes spinning speed
            isSpinning = true;
        }
    }

    // stops the reel spin animation
    // called by spin controller
    public void Stop()
    {
        if (isSpinning)
        {
            isSpinning = false;
            UpdateSymbolPositions(); // snaps squares of reel into proper places
        }
    }

    // Set initial symbols on the reel
    // called by gamecontroller
    public void SetInitialSymbols()
    {
        // each square is given a sprite according to the sequence provided by game controller
        foreach (Transform symbolPosition in symbolPositions)
        {
            symbolPosition.GetComponent<SpriteRenderer>().sprite = symbolSprites[symbolSequence[currentSeqIndex].id];
            currentSeqIndex = Modulo((currentSeqIndex+1), symbolSequence.Length); // increments with looping
        }
    }

    // Shift the symbols on the reel by 1
    // maintains reference on the third square or last showing symbol
    private void ShiftSymbols()
    {
        // Shift the current symbol index
        currentSymbolIndex = Modulo((currentSymbolIndex - 1), symbolPositions.Length);
    }

    // Update the positions of symbols on the reel
    private void UpdateSymbolPositions()
    {
        symbolPositions[currentSymbolIndex].position = startPos.position; // sets last square to the hidden start position of the reel
        // Get a random sprite from the symbolSprites array
        Sprite randomSprite = symbolSprites[symbolSequence[currentSeqIndex].id];

        // Assign the random sprite to the symbolPosition object
        symbolPositions[currentSymbolIndex].GetComponent<SpriteRenderer>().sprite = randomSprite;
        currentSeqIndex = Modulo((currentSeqIndex+1), symbolSequence.Length); // increment with looping
        SnapSymbols();
    }

    // snaps symbols into proper place
    private void SnapSymbols(){
        for(int i = 0; i < showingPositions.Length; i ++){
            //starts from top row to bottom
            int firstSymbolIndex = Modulo(currentSymbolIndex + i + 1, symbolPositions.Length); // first showing symbol
            symbolPositions[firstSymbolIndex].position = showingPositions[i].position; // symbol is snapped into place
            
            // starts from the bottom row to the top
            int showingSeqIndex = Modulo(currentSeqIndex - i - 2, symbolSequence.Length); // sequence symbol that is showing
            showingSymbols[i] = symbolSequence[showingSeqIndex];
        }
    }

    // called by gamecontroller to get values
    public SymbolData GetVisibleSymbol(int row)
    {
        return showingSymbols[row];
    }

    // called by gamecontroller to set sequence
    public void SetSymbolSequence(SymbolData[] sequence)
    {
        symbolSequence = sequence;
    }

    // modulo function because C# % is only a remainder operation
    int Modulo(int x, int m) {
        return (x%m + m)%m;
    }
}
