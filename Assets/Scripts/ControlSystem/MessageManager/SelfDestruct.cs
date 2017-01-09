using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public GameObject explosion;

	private float shake = 0.2f;
	private AudioSource audioSource;

	void Awake () 
	{
		audioSource = GetComponent <AudioSource>();
	}

	void OnEnable () 
	{
		MessageManager.StartListening ("Destroy", Destroy);
	}

	void OnDisable () 
	{
		MessageManager.StopListening ("Destroy", Destroy);
	}

	void Destroy () 
	{
		MessageManager.StopListening ("Destroy", Destroy);
		StartCoroutine (DestroyNow());
	}

	IEnumerator DestroyNow() 
	{
		yield return new WaitForSeconds (Random.Range (0.0f, 1.0f));
		audioSource.pitch = Random.Range (0.75f, 1.75f);
		audioSource.Play ();
		float startTime = 0;
		float shakeTime = Random.Range (1.0f, 3.0f);
		while (startTime < shakeTime) 
		{
			transform.Translate (Random.Range (-shake, shake), 0.0f, Random.Range (-shake, shake));
			transform.Rotate ( 0.0f, Random.Range (-shake * 100, shake * 100), 0.0f);
			startTime += Time.deltaTime;
			yield return null;
		}
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}