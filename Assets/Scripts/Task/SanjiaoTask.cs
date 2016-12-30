using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SanjiaoTask : MonoBehaviour {
    /// <summary>
    /// Trigger the task when enter the trigger of the sanjiao
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Task task5 = GameObject.Find("TaskSystem/TaskFactory/task5").GetComponent<Task>();
            Task task6 = GameObject.Find("TaskSystem/TaskFactory/task6").GetComponent<Task>();
            // If the task5/6 is discovered and not finished, load into the attack scene.
            if (task5.getStatus() == TaskStatus.DISCOVERED 
                || task6.getStatus() == TaskStatus.DISCOVERED)
            {
                TaskManager.TriggerTask("Task5" + "Trigger");
                PlayerPrefs.SetFloat("x", other.transform.position.x);
                PlayerPrefs.SetFloat("z", other.transform.position.z);
                SceneManager.LoadScene("Instance01");
            }
            // Else show a simple dialogue.
            else
            {
                GenericDialogueManager.Instance().DisplayMessage("三教感觉有点阴森...里面好像有些奇怪的声音...");
            }

        }
    }
}
