using UnityEngine;
using System.Collections;

public class NPCTask : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TaskManager.TriggerTask("Task2" + "Trigger");
            TaskManager.TriggerTask("Task4" + "Trigger");
        } 
    }
}
