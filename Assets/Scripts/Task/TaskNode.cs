using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TaskNode : MonoBehaviour {

    public enum taskNodeStatus
    {
        UNDISCOVERED,
        DISCOVERED,
        COMPLETED
    };

    private taskNodeStatus status;
    public List<TaskNode> parentNode;
    public List<TaskNode> childNode;
    public Notifier ntf;

	// Use this for initialization

    void Awake()
    {
        status = taskNodeStatus.UNDISCOVERED;
    }

    // Set task node status 
   
    public void setStatus(int inStatusId)
    {
        status = (taskNodeStatus)inStatusId;
    }

    public void setStatus(taskNodeStatus inTaskStatus)
    {
        status = inTaskStatus;
    }

    // Compare status with an input 
    public bool equalStatus(int inStatusId)
    {
        return status == (taskNodeStatus)inStatusId;
    }

    public bool equalStatus(taskNodeStatus inTaskStatus)
    {
        return status == inTaskStatus;
    }
    // Try to update self status, called by listener
    public void tryUpdateSelf()
    {
        // if status is not undiscovered or have no parent return
        if((status != taskNodeStatus.UNDISCOVERED) || parentNode.Count == null)
            return;

        bool uncompletedParent = false;
        for (int i = 0; i < parentNode.Count; i++)
        {
            if ((taskNodeStatus)parentNode[i].status != taskNodeStatus.COMPLETED)
                uncompletedParent = true;
        }

        // if not all parent completed or have no child, this node will not be discovered
        if (uncompletedParent || (childNode.Count == 0))
            return;

        status = taskNodeStatus.DISCOVERED;

    }


    // Set self state as finish and try to update other node

    public void finishUpdate()
    {
        status = taskNodeStatus.COMPLETED;
        for (int i = 0; i < childNode.Count; i++)
        {
            childNode[i].tryUpdateSelf();
        }
        callNotifier();
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
