using UnityEngine;
using System.Collections;

public class TaskToggleButton : MonoBehaviour
{

    public void TogglePanel (GameObject panel) {
        panel.SetActive (!panel.activeSelf);
    }
}

