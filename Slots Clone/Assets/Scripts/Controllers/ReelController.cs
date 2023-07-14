using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    public float spinSpeed = 100f;
    public Transform[] symbolPositions;  // Array of symbol positions on the reel
    public Sprite[] symbolSprites;  // Array of symbol sprites

    public bool isSpinning = false;
    private int currentSymbolIndex = 0;
    private Vector3 finalPosition;

    private void Start()
    {
        // Assign initial symbols to the reel
        SetInitialSymbols();
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
            if (symbolPositions[0].position.y < transform.position.y)
            {
                // Shift the symbols and update their positions
                ShiftSymbols();
                UpdateSymbolPositions();
            }
        }
    }
    // Start the reel spin animation
    public void Spin()
    {
        if (!isSpinning)
        {
            isSpinning = true;
        }
    }

    public void Stop()
    {
        if (isSpinning)
        {
            isSpinning = false;
        }
    }

    // Set initial symbols on the reel
    private void SetInitialSymbols()
    {
        foreach (Transform symbolPosition in symbolPositions)
        {
            symbolPosition.GetComponent<SpriteRenderer>().sprite = symbolSprites[currentSymbolIndex];
        }
    }

    // Shift the symbols on the reel
    private void ShiftSymbols()
    {
        // Shift the current symbol index
        currentSymbolIndex = (currentSymbolIndex + 1) % symbolSprites.Length;
    }

    // Update the positions of symbols on the reel
    private void UpdateSymbolPositions()
    {
        int symbolCount = symbolPositions.Length;
        for (int i = 0; i < symbolCount; i++)
        {
            int symbolIndex = (currentSymbolIndex + i) % symbolSprites.Length;
            symbolPositions[i].GetComponent<SpriteRenderer>().sprite = symbolSprites[symbolIndex];

            int initialPositionIndex = (symbolCount - i + currentSymbolIndex) % symbolCount;
            symbolPositions[i].position = symbolPositions[initialPositionIndex].position;
        }
    }
}
