using UnityEngine;
using System.Collections;

public class TaskToggleButton : MonoBehaviour
{
	// Toggle button, pause the game time
    public void TogglePanel (GameObject panel) {
        panel.SetActive (!panel.activeSelf);
    }
}

