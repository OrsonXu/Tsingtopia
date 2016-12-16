using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingMenu : MonoBehaviour {

	private Button settingButton;
	private Canvas settingCanvas;
	// Use this for initialization
	void Start () {
		GameObject settingButtonGO = GameObject.FindWithTag ("SettingButton");
		settingButton = settingButtonGO.GetComponent<Button> ();
		settingButton.onClick.AddListener (pressedSettingButton);
		GameObject settingCanvasGO = GameObject.FindWithTag ("SettingPanel");
		settingCanvas= settingCanvasGO.GetComponent<Canvas> ();
		}
	
	void pressedSettingButton(){
		MessageManager.TriggerEvent ("TimeScaleChange");

		settingCanvas.enabled = !settingCanvas.enabled;
	}
}
