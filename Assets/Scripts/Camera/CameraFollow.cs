using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing;

    private Vector3 offset;

    void Start() {
        offset = - target.position + transform.position;
    }

    void FixedUpdate()
    {
        Vector3 Camposition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, Camposition, smoothing * Time.deltaTime);
    }
}
