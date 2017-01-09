using UnityEngine;
using System.Collections;
/// <summary>
/// Basic class for recover
/// </summary>
public class Recover : MonoBehaviour {

    public int RecoverValue = 10;
    public float RotationSpeed = 5;
    public float SustainTime = 10;

    private float _timeClock;
    private int _ID;
    private RecoverManager _recoverManager;


    void Update()
    {
        transform.Rotate(Vector3.left * RotationSpeed * Time.deltaTime, 1f, Space.World);
        _timeClock += Time.deltaTime;
        if (_timeClock > SustainTime) {
            DestorySelf();
            _timeClock = 0;
        }
    }
    /// <summary>
    /// Initiate the position and ID, together with the manager
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="ID"></param>
    /// <param name="rm">Recover manager which manages the recover</param>
    public void init(Vector3 pos, int ID, RecoverManager rm)
    {
        this._ID = ID;
        gameObject.transform.position = pos;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(45f, 0f, 0f));
        _recoverManager = rm;
        _recoverManager.SetTrue(ID);
    }
    /// <summary>
    /// Destroy the recover
    /// </summary>
    public void DestorySelf()
    {
        _recoverManager.SetFalse(_ID);
        Destroy(gameObject);
    }
}
