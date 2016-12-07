using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TaskManager : MonoBehaviour {

    private static TaskManager TaskManager;
    private List<TaskNode> activeNodes;

    public static TaskManager Instance()
    {
        if (!TaskManager)
        {
            TaskManager = FindObjectOfType(typeof(TaskManager)) as TaskManager;
            if (!TaskManager)
                Debug.LogError("There needs to be one active TaskManager script on a GameObject in your scene.");
        }

        return TaskManager;
    }
    // if tasknode is active,  update it
    void tryUpdate(TaskNode tn)
    {
        if(activeNode.Find( tasknode => {tasknode == tn;}))
            tn.tryUpdate();
    }
    // called by tn, when a tn is finished
    void removeFinishedNode(TaskNode tn)
    {
        activeNodes.Remove(tn);
    }
    // a new tn is discovered and added to active list
    void addActiveNode(TaskNode tn)
    {
        activeNodes.Add(tn);
    }
    
}
