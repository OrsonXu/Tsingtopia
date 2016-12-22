using UnityEngine;
using System.Collections;

public class ErxiaomengTask : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TaskManager.TriggerTask("Task3" + "Trigger");
        }
    }
}
