//using UnityEngine;
//using System.Collections;
//using UnityEngine.Events;
//using System.Collections.Generic;
//public class TaskRegister : MonoBehaviour {
//
//
//	private Dictionary<string, int> taskList;
//
//	private static TaskRegister taskRegister;
//
//	public staticTaskRegister instance	{
//		get	{
//			if (!taskRegister){
//				taskRegister = FindObjectOfType (typeof (TaskRegister)) as TaskRegister;
//				if (!taskRegister){
//					Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
//				}
//				else{
//					taskRegister.Init (); 
//				}
//			}
//			return taskRegister;
//		}
//	}
//	void Init(){
//		if (taskList == null){
//			taskList = new Dictionary<string, UnityEvent>();
//		}
//	}
//	public void RegisterTask(string taskName, int taskID){
//		UnityEvent thisEvent = null;
//		Debug.Log("TaskID" + taskID.ToString() + " added " + " name: " + taskName);
//		// Try get value a eventName and add listener to it
//		if (instance.taskList.TryGetValue (taskName, out thisEvent)){
//			thisEvent.AddListener (listener);
//		} 
//		else{
//			thisEvent = new UnityEvent ();
//			thisEvent.AddListener (listener);
//			instance.eventDictionary.Add (eventName, thisEvent);
//		}
//	}
//}
