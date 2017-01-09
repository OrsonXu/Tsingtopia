
	
	
	#pragma strict

var bird: GameObject;

	
	function OnGUI() {
	
		GUILayout.BeginHorizontal ("box");
		
		if (GUILayout.Button("Idle")){
		Time.timeScale = 1;
		bird.GetComponent.<Animation>().CrossFade("idle");
		
		
		}
		
		if (GUILayout.Button("fly")){
		Time.timeScale = 1;
		bird.GetComponent.<Animation>().CrossFade("fly");
		
		}
		
		
		if (GUILayout.Button("walk")){
		Time.timeScale = .7;
		bird.GetComponent.<Animation>().CrossFade("walk");
		
		}
		

		

		
		GUILayout.EndHorizontal ();
	}
	
	