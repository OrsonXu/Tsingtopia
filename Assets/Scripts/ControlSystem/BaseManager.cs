using UnityEngine;
using System.Collections;

public class BaseManager : MonoBehaviour {

	public virtual void Awake(){
		Debug.Log("BaseManager.Awake");
	}

	public int mCounter = 0;

	public virtual void Print(){
		Debug.LogFormat("--- name:{0}, counter:{1}", this.GetType().Name, mCounter);
		++mCounter;
	}

	public virtual void Clear(){
		Debug.LogFormat("--- name:{0}, Clear", this.GetType().Name);
	}
}