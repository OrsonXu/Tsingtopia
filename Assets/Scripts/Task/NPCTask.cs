using UnityEngine;
using System.Collections;

public class NPCTask : MonoBehaviour {
    /// <summary>
    /// Trigger the task when enter the trigger of the NPC
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        Task task3 = GameObject.Find("TaskSystem/TaskFactory/task3").GetComponent<Task>();
        if (other.tag == "Player")
        {
            // If the task3 is discovered adn haven't finished, show a simple dialogue
            if (task3.getStatus() == TaskStatus.DISCOVERED)
            {
                GenericDialogueManager.Instance().DisplayMessage("董老师:\n小清你回来那么快?二校门那里有线索吗?");
            }
            else
            {
                //TaskManager.TriggerTask("Task1" + "Trigger");
                TaskManager.TriggerTask("Task2" + "Trigger");
                TaskManager.TriggerTask("Task4" + "Trigger");
                TaskManager.TriggerTask("Task7" + "Trigger");
            }
        } 
    }
}
