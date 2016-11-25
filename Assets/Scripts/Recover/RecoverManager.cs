using UnityEngine;
using System.Collections;
//using UnityEngine.Random = System.Random;
using System;

public class RecoverManager : MonoBehaviour {

    public Transform[] RecoverPoints;
    public float spawnInterval;
    public Recover[] recoverList;

    private bool[] hasRecoverObject;
    private float timeStamp;

    void Awake()
    {
        hasRecoverObject = new bool[RecoverPoints.Length];
        for (int i = 0; i < hasRecoverObject.Length; i++)
        {
            hasRecoverObject[i] = false;
        }
    }

    void Update()
    {
        timeStamp += Time.deltaTime;
        if (timeStamp > spawnInterval)
        {
            SpawnRecover();
            timeStamp = 0;
        }
    }

    void SpawnRecover()
    {
        int index = UnityEngine.Random.Range(0, RecoverPoints.Length);
        int option = UnityEngine.Random.Range(0, 2);
        int tmp_count;
        if ( option == 1 )
        {
            tmp_count = 0;
            for (int i = index; i < RecoverPoints.Length; i = (i + 1) % RecoverPoints.Length)
            {
                if (!hasRecoverObject[i])
                {
                    Recover recover = Instantiate(recoverList[1]) as Recover;
                    recover.init(RecoverPoints[i].position, i, this);
                    break;
                }
                tmp_count++;
                if (tmp_count == RecoverPoints.Length)
                {
                    break;
                }
            }
        }
        else
        {
            tmp_count = 0;
            for (int i = index; i < RecoverPoints.Length; i = (i + 1) % RecoverPoints.Length)
            {
                if (!hasRecoverObject[i])
                {
                    Recover recover = Instantiate(recoverList[0]) as Recover;
                    recover.init(RecoverPoints[i].position, i, this);
                    break;
                }
                tmp_count++;
                if (tmp_count == RecoverPoints.Length)
                {
                    break;
                }
            }
        }
    }

    public void SetTrue(int ID)
    {
        hasRecoverObject[ID] = true;
    }

    public void SetFalse(int ID)
    {
        hasRecoverObject[ID] = false;
    }
}
