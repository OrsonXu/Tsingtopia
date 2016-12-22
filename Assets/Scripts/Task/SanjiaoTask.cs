using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SanjiaoTask : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TaskManager.TriggerTask("Task5" + "Trigger");
            Task task = GameObject.Find("TaskSystem/TaskFactory/task5").GetComponent<Task>();
            if (task.getStatus() != TaskStatus.UNDISCOVERED)
            {
                SceneManager.LoadScene("Instance01");
            }

        }
    }
}
