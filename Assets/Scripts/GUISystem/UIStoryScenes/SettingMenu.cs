using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SettingMenu : MonoBehaviour {

	Canvas _settingMenuCanvas;

	// Use this for initialization
	void Start () {
		_settingMenuCanvas = GetComponent<Canvas>();
		_settingMenuCanvas.enabled = false;
	}

	// Setting response test
	public void pressedSettingButton(){
		MessageManager.TriggerEvent ("TimeScaleChange");
        _settingMenuCanvas = GetComponent<Canvas>();
        if (_settingMenuCanvas == null)
        {
            Debug.Log("Is null!!");
            return;
        }
		_settingMenuCanvas.enabled = !_settingMenuCanvas.enabled;
	}
	public void pressedResumeButton(){
		MessageManager.TriggerEvent ("TimeScaleChange");
		_settingMenuCanvas.enabled = !_settingMenuCanvas.enabled;
	}
	// This function should be moved to Game Controller
	public void Quit(){
	#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
	#else 
		Application.Quit();
	#endif
	}
}
