using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpinController : MonoBehaviour
{
    public Button spinBtn; // reference to spinbutton

    [SerializeField] 
    private TextMeshProUGUI spinTxt; // text of spin button

    private bool isSpinning = false;
    public ReelController[] reelControllers;  // Array of reel controllers
    public GameController gameController; // reference to game controller for its public functions
    void Start()
    {
        spinBtn.onClick.AddListener(OnClick); // adds listener of spin button
    }
    
    // stop and spin button;
    // spins if isSpinning = false
    // stops if isSpinning = true
    private void OnClick()
    {
        if (!isSpinning)
        {
            // spins each reelcontroller
            foreach (ReelController reelController in reelControllers)
            {
                reelController.Spin();
            }
            isSpinning = true;

            gameController.NewSpin(); // initializes the variables for each spin

            spinTxt.text = "Stop"; // turns into stop button
        } else
        {
            StopSpinning();
        }
    }

    private void StopSpinning()
    {
        // stops each controller
        foreach (ReelController reelController in reelControllers)
        {
            reelController.Stop();
        }

        isSpinning = false;

        spinTxt.text = "Spin"; // turns into spin button

        gameController.CalculateResults(); // results after spinning
    }
}