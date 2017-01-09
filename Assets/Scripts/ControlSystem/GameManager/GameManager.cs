//using UnityEngine;
//using System.Collections;
//public class GameManager : BaseManager {
//
//	public static string gameName = "Player";
//	private static GameObject gameObj = null;
//	private static GameManager gameManager = null;
//
//	public static GameManager instance	{
//		get	{
//			if (!gameManager){
//				gameManager = FindObjectOfType (typeof (GameManager)) as GameManager;
//				if (!gameManager){
//					Debug.LogError ("There needs to be one active GameManager script on a GameObject in your scene.");
//				}
//				else{
//					gameManager.Init (); 
//				}
//			}
//			return gameManager;
//		}
//	}
//	public static void Init(){
//		GameObject obj = Resources.Load("Prefabs/Characters/Player") as GameObject;
//		obj = Instantiate(obj);
//		obj.name = gameName;
//		DontDestroyOnLoad(obj);
//	}
//
//	public static GameObject Obj{
//		get { return gameObj; }
//	}
//
//
//
//	public override void Awake(){
//		base.Awake();
//		gameObj = gameObject;
//		gameManager = this;
//	}
//
//	public void Start(){
//		Debug.Log("--- GameManager.Start");
//	}
//
//
//	public override void Clear(){
//		BaseManager[] comps = gameObject.GetComponents<BaseManager>();
//		for (int i = 0; i< comps.Length; ++i){
//			if (!(comps[i] is GameManager)) 
//			{
//				comps[i].Clear();
//			}
//		}
//		base.Clear();
//	}
//
//	public void Quit(){
//		Debug.Log("--- GameManager.Quit");
//		Clear();
//		DestroyObject(gameObj);
//	}
//}