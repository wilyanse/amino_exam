using Random=UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    public float spinSpeed;
    public Transform[] symbolPositions;  // Array of symbol positions on the reel
    public Sprite[] symbolSprites;  // Array of symbol sprites
    private SymbolData[] symbolSequence;
    public bool isSpinning = false;
    private int currentSymbolIndex;
    private int currentSeqIndex;
    public Transform startPos;
    public Transform finalPos;

    private Transform[] showingPositions;

    private void Start()
    {
        // Assign initial symbols to the reel
        spinSpeed = Random.Range(25.0f, 100.0f);
        currentSeqIndex = 0;
        currentSymbolIndex = symbolPositions.Length - 1;
        showingPositions = new Transform[symbolPositions.Length - 1];
        SetInitialSymbols();
        // Copy the middle elements into the destination array
        for (int i = 1; i < symbolPositions.Length; i++)
        {
            Transform sourcePos = symbolPositions[i];
            Transform showingPos = new GameObject().transform; // Create a new Transform object
            showingPos.position = sourcePos.position;
            showingPos.rotation = sourcePos.rotation;
            showingPos.localScale = sourcePos.localScale;
            showingPositions[i - 1] = showingPos;
        }
    }
    private void Update()
    {
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
    public void Spin()
    {
        if (!isSpinning)
        {
            spinSpeed = Random.Range(25.0f, 100.0f);
            isSpinning = true;
        }
    }

    public void Stop()
    {
        if (isSpinning)
        {
            isSpinning = false;
            UpdateSymbolPositions();
        }
    }

    // Set initial symbols on the reel
    private void SetInitialSymbols()
    {
        foreach (Transform symbolPosition in symbolPositions)
        {
            symbolPosition.GetComponent<SpriteRenderer>().sprite = symbolSprites[symbolSequence[currentSeqIndex].id];
            currentSeqIndex = Modulo((currentSeqIndex+1), symbolSequence.Length);
        }
    }

    // Shift the symbols on the reel
    private void ShiftSymbols()
    {
        // Shift the current symbol index
        currentSymbolIndex = Modulo((currentSymbolIndex - 1), symbolPositions.Length);
    }

    // Update the positions of symbols on the reel
    private void UpdateSymbolPositions()
    {
        symbolPositions[currentSymbolIndex].position = startPos.position;
        // Get a random sprite from the symbolSprites array
        Sprite randomSprite = symbolSprites[symbolSequence[currentSeqIndex].id];

        // Assign the random sprite to the symbolPosition object
        symbolPositions[currentSymbolIndex].GetComponent<SpriteRenderer>().sprite = randomSprite;
        currentSeqIndex = Modulo((currentSeqIndex+1), symbolSequence.Length);
        SnapSymbols();
    }

    private void SnapSymbols(){
        for(int i = 0; i < showingPositions.Length; i ++){
            int firstSymbolIndex = Modulo(currentSymbolIndex + i + 1, symbolPositions.Length);
            symbolPositions[firstSymbolIndex].position = showingPositions[i].position;
        }
    }

    public SymbolData GetVisibleSymbols()
    {
        int centerIndex = Mathf.FloorToInt(symbolPositions.Length / 2f);
        // TODO
        return null;
    }

    public void SetSymbolSequence(SymbolData[] sequence)
    {
        symbolSequence = sequence;
    }

    // modulo
    int Modulo(int x, int m) {
        return (x%m + m)%m;
    }
}
