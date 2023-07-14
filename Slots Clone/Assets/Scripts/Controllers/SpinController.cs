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
    void Start()
    {
        Debug.Log(spinTxt.text);
        spinBtn.onClick.AddListener(OnClick);
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
}