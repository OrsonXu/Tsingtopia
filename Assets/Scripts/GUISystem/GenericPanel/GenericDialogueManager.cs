using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenericDialogueManager : BaseManager
{

    public Text displayText;
	public GameObject modalPanelObj;
    public float displayTime;
    public float fadeTime;

    private IEnumerator fadeAlpha;
    private static GenericDialogueManager gdManager;
	private string[] dialogueText;
	private int msgCounter;
	private bool isDialogue;
	private float _lastFrame;
	private float _updateTime = 0.1f;
    public static GenericDialogueManager Instance()
    {
        if (!gdManager)
        {
            gdManager = FindObjectOfType(typeof(GenericDialogueManager)) as GenericDialogueManager;
            if (!gdManager)
                Debug.LogError("There needs to be one active GenericDialogueManager script on a GameObject in your scene.");
        }

        return gdManager;
    }

    public void DisplayMessage(string message)
    {
        displayText.text = message;
		modalPanelObj.SetActive (true);
		msgCounter = 0;
		isDialogue = false;
//        SetAlpha();
    }

	public void DisplayMessage(string[] messages)
	{
		isDialogue = true;
		dialogueText = messages;
		displayText.text = dialogueText [0];
		msgCounter = 1;
		modalPanelObj.SetActive (true);
		//        SetAlpha();
	}


    public void SetDisactive()
    {
        modalPanelObj.SetActive(false);
    }


	private void Awake(){
		
	}
	private void Update(){
		if (_lastFrame < _updateTime) {
			_lastFrame += Time.deltaTime;
		} else {
			_lastFrame = 0f;
			if (isDialogue == true) {
				if (msgCounter < dialogueText.Length) {
					if (Input.GetKey (KeyCode.Space)) {
						displayText.text = dialogueText [msgCounter++];
					}
				}
				if (msgCounter >= dialogueText.Length) {
					if (Input.GetKey (KeyCode.Space)) {
						msgCounter = 0;
						displayText.text = "if you this, its an error";
						SetDisactive();
					}
				}
			}
		}
	}
//    void SetAlpha()
//    {
//        if (fadeAlpha != null)
//        {
//            StopCoroutine(fadeAlpha);
//        }
//        fadeAlpha = FadeAlpha();
//        StartCoroutine(fadeAlpha);
//    }
//
//    IEnumerator FadeAlpha()
//    {
//        Color resetColor = displayText.color;
//        resetColor.a = 1;
//        displayText.color = resetColor;
//
//        yield return new WaitForSeconds(displayTime);
//
//        while (displayText.color.a > 0)
//        {
//            Color displayColor = displayText.color;
//            displayColor.a -= Time.deltaTime / fadeTime;
//            displayText.color = displayColor;
//            yield return null;
//        }
//        yield return null;
//    }
}