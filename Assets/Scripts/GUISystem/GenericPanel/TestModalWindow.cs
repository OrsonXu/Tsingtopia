using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

//  This script will be updated in Part 2 of this 2 part series.
public class TestModalWindow : MonoBehaviour
{
    private ModalPanel modalPanel;
    private GenericDialogueManager gdManager;

    void Awake()
    {
        modalPanel = ModalPanel.Instance();
        gdManager = GenericDialogueManager.Instance();
    }

    //  Send to the Modal Panel to set up the Buttons and Functions to call
    public void TestYNC()
    {
        modalPanel.Choice("Do you want to spawn this sphere?", TestYesFunction, TestNoFunction, TestCancelFunction);
        
    }

    //  Test functions
    void TestYesFunction()
    {
        gdManager.DisplayMessage("Heck yeah! Yup!");
    }

    void TestNoFunction()
    {
        gdManager.DisplayMessage("No way, José!");
    }

    void TestCancelFunction()
    {
        gdManager.DisplayMessage("I give up!");
    }
}