using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

	public GUIStyle Customized;

	private int _repeatButton1 = 0;
	private string _textString = "Show this test string";
	private Rect _windowRect = new Rect (20, 20, 120, 50);
	private Rect _settingMenu = new Rect (Screen.width/2, Screen.height/2, 300, 300);
	private float sliderValue = 50.0f;
	private float sliderValue2 = 50.0f;
	private float maxSliderValue = 100.0f;

	// Overwrite when Unity reload
	void Start(){
		
	}

	// Overwrite, when Unity UI Event System is triggered
	void OnGUI(){

		_textString = GUI.TextArea (new Rect (Screen.width * 0.5f - 200, Screen.height * 0.5f - 150, 100, 50), _textString);
		if (GUI.Button (new Rect (0, Screen.height - 50, 100, 50), "Settings")){
			Debug.Log("Click settings buttons");
		}

		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 50, 100, 50), "Task")) {
			Debug.Log ("Click task buttons");

			Debug.Log ("Text String is: " + _textString);
		}

		if (GUI.Button (new Rect (Screen.width * 0.5f - 100, Screen.height * 0.5f - 50, 100, 50), "Press this Repeated Button" )){
			_repeatButton1 += 1;
			Debug.Log("I was pressed for "+ _repeatButton1.ToString() + " times");
		}
		if (GUI.changed) {
			Debug.Log ("GUI changed!");
		}
//		GUILayout.BeginArea (_settingMenu);	
//		GUILayout.BeginVertical ();
//
//		GUI.Box (_settingMenu, "Settings");
//		GUILayout.EndVertical ();
//		GUILayout.EndArea ();
//		_windowRect = GUI.Window (0, _windowRect, WindowFunction, "My Window");

		// Wrap everything in the designated GUI Area
		GUILayout.BeginArea (new Rect (Screen.width/2 - 100, Screen.height/2 - 40,200,80));

		// Begin the singular Horizontal Group
		GUILayout.BeginHorizontal();

		GUILayout.BeginVertical();
		// Arrange two more Controls vertically beside the Button
		GUILayout.BeginVertical();
		GUILayout.Box("Music Value: " + Mathf.Round(sliderValue));
		sliderValue = GUILayout.HorizontalSlider (sliderValue, 0.0f, maxSliderValue);
		GUILayout.EndVertical();

		GUILayout.BeginVertical();
		GUILayout.Box("SE Value: " + Mathf.Round(sliderValue2));
		sliderValue2 = GUILayout.HorizontalSlider (sliderValue2, 0.0f, maxSliderValue);
		// End the Groups and Area
		GUILayout.EndVertical();

		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();


	}

	// This funciton is aborted *****
	void WindowFunction (int windowID) {
		// Draw any Controls inside the window here
		GUI.Button (new Rect (0, 0, 100, 50), "Pops up");
	}
}
