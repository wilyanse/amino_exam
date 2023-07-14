using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpinController : MonoBehaviour
{
    [SerializeField]
    private Button spinBtn;

    [SerializeField] 
    private TextMeshProUGUI spinTxt;

    private bool isSpinning = false;
    public ReelController[] reelControllers;  // Array of reel controller

    public int[][] spinResults; // Array of line payouts
    void Start()
    {
        Debug.Log(spinTxt.text);
        spinBtn.onClick.AddListener(OnClick);
        spinResults = new int[3][];
    }

    private void OnClick()
    {
        if (!isSpinning)
        {
            foreach (ReelController reelController in reelControllers)
            {
                reelController.Spin();
            }
            isSpinning = true;
            spinTxt.text = "Stop";
        } else
        {
            StopSpinning();
        }
    }

    private void StopSpinning()
    {
        foreach (ReelController reelController in reelControllers)
        {
            reelController.Stop();
        }
        isSpinning = false;
        spinTxt.text = "Spin";
    }

    public void CalculateSpinResult()
    {

        // Step 2: Get the visible symbols from the center positions of each reel
        SymbolData[] visibleSymbols = GetVisibleSymbols();
    }

    private SymbolData[] GetVisibleSymbols()
    {
        SymbolData[] visibleSymbols = new SymbolData[reelControllers.Length];

        for (int i = 0; i < reelControllers.Length; i++)
        {
            // Get the symbol at the center position of each reel
            visibleSymbols[i] = reelControllers[i].GetVisibleSymbols();
        }

        return visibleSymbols;
    }
}