using UnityEngine;
using System.Collections;

public class ErxiaomengTask : MonoBehaviour {
    /// <summary>
    /// Trigger the task when enter the trigger of the erxiaomeng
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TaskManager.TriggerTask("Task3" + "Trigger");
        }
    }
}
