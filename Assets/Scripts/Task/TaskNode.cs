using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TaskNode : MonoBehaviour {

    public enum TaskNodeStatus
    {
        UNDISCOVERED,
        DISCOVERED,
        COMPLETED
    };

    private TaskNodeStatus status;
    private TaskManager tskmnger;
    public List<TaskNode> parentNode;
    public List<TaskNode> childNode;
    public Notifier ntf;

	// Use this for initialization

    void Awake()
    {
        status = TaskNodeStatus.UNDISCOVERED;
        tskmnger = TaskManager.Instance();
    }

    // Set task node status 
   
    public void setStatus(int inStatusId)
    {
        status = (TaskNodeStatus)inStatusId;
    }

    public void setStatus(TaskNodeStatus inTaskStatus)
    {
        status = inTaskStatus;
    }

    // Compare status with an input 
    public bool equalStatus(int inStatusId)
    {
        return status == (TaskNodeStatus)inStatusId;
    }

    public bool equalStatus(TaskNodeStatus inTaskStatus)
    {
        return status == inTaskStatus;
    }
   
    // Try to update self status, called by listener
    public void tryUpdate()
    {
        // if status is not undiscovered or have no parent return
        if((status != TaskNodeStatus.UNDISCOVERED) || parentNode.Count == null)
            return;
        bool uncompletedParent = false;
        for (int i = 0; i < parentNode.Count; i++)
        {
            if ((TaskNodeStatus)parentNode[i].status != TaskNodeStatus.COMPLETED)
                uncompletedParent = true;
        }
        // if not all parent completed or have no child, this node will not be discovered
        if (uncompletedParent || (childNode.Count == 0))
            return;
        status = TaskNodeStatus.DISCOVERED;
        tskmnger.addActiveNode(this);
    }


    // Set self state as finish and try to update other node

    public void finishUpdate()
    {
        status = TaskNodeStatus.COMPLETED;
        for (int i = 0; i < childNode.Count; i++)
        {
            childNode[i].tryUpdateSelf();
        }
        callNotifier();
        tskmnger.removeFinishedNode(this);
    }


    // Call notifier

    public void callNotifier()
    {
        ntf.Awake();
    }

    public void addParent(TaskNode pn)
    {
        parentNode.Add(pn);
    }

    public void addChild(TaskNode cn)
    {
        childNode.Add(cn);
    }

	
}
