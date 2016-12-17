using UnityEngine;
using System.Collections;

public class PlayerKillCounter : MonoBehaviour {
    public int Length { get; set; }
    private int[] _countList;

    void OnEnable()
    {
        MessageManager.StartListening("EnemyDieWithID", AddCount);
    }
    void OnDisable()
    {
        MessageManager.StopListening("EnemyDieWithID", AddCount);
    }
	// Use this for initialization
	void Start () {
        _countList = new int[Length];
        for (int i = 0; i < _countList.Length; i++)
        {
            _countList[i] = 0;
        }
	}

    public void AddCount(int EnemyID)
    {
        _countList[EnemyID]++;
    }
}
