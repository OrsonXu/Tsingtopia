using UnityEngine;
using System.Collections;

public class DestroyByTimeExtended : MonoBehaviour {

	public float lifeTime;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTime);
		GetComponent <AudioSource>().pitch = Random.Range (0.75f, 1.25f);
	}
}