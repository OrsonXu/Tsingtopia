using UnityEngine;
using System.Collections;

public class Recover : MonoBehaviour {

    public int RecoverValue = 10;
    public float RotationSpeed = 5;
    public float SustainTime = 10;

    private float timeClock;
    private int ID;
    private RecoverManager recoverManager;


    void Update()
    {
        transform.Rotate(Vector3.left * RotationSpeed * Time.deltaTime, 1f, Space.World);
        timeClock += Time.deltaTime;
        if (timeClock > SustainTime) {
            DestorySelf();
            timeClock = 0;
        }
    }

    public void init(Vector3 pos, int ID, RecoverManager rm)
    {
        this.ID = ID;
        gameObject.transform.position = pos;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(45f, 0f, 0f));
        recoverManager = rm;
        recoverManager.SetTrue(ID);
    }

    public void DestorySelf()
    {
        recoverManager.SetFalse(ID);
        Destroy(gameObject);
    }
}
