using UnityEngine;
using System.Collections;

public class PlayerKillCounter : MonoBehaviour {
    public int Length { get; set; }
    private int[] _countList;
    private bool finished;

    void OnEnable()
    {
        MessageManager.StartListening("EnemyDieWithID", AddCount);
    }
    void OnDisable()
    {
        MessageManager.StopListening("EnemyDieWithID", AddCount);
    }
	
	void Start () {
        _countList = new int[Length];
        finished = false;
        for (int i = 0; i < _countList.Length; i++)
        {
            _countList[i] = 0;
        }
	}

    public void AddCount(int EnemyID)
    {
        _countList[EnemyID]++;
    }

    public bool checkFinish()
    {
        foreach (int i in _countList)
        {
            if (i <= 1)
                return false;
        }
        return true;
    }

    public void Update()
    {
        if (!finished && checkFinish())
        {
            Debug.Log("Clear!!!!");
            MessageManager.TriggerEvent("InstanceFinish");
            finished = true;
        }
    }
}
