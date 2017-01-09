using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

    private float _duration = 1f;
    private float _rotationSpeed = 90f;
    private Light light;
    Color color1;
    Color color2;
	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        color1 = new Color(142 / 255, 53/255, 74/255);
        color2 = new Color(85 / 255, 66 / 255, 54 / 255);

	}
	
	// Update is called once per frame
	void Update () {
        //float t = Mathf.PingPong(Time.time, _duration) / _duration;
        //light.color = Color.Lerp(color1, color2, t);

        transform.RotateAround(transform.position, transform.up, Time.deltaTime * _rotationSpeed);
	}
}
