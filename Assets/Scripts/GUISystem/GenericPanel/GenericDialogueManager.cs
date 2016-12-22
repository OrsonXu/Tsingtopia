using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenericDialogueManager : BaseManager
{

    public Text displayText;
    public float displayTime;
    public float fadeTime;

    private IEnumerator fadeAlpha;

    private static GenericDialogueManager gdManager;

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
        SetAlpha();
    }

    void SetAlpha()
    {
        if (fadeAlpha != null)
        {
            StopCoroutine(fadeAlpha);
        }
        fadeAlpha = FadeAlpha();
        StartCoroutine(fadeAlpha);
    }

    IEnumerator FadeAlpha()
    {
        Color resetColor = displayText.color;
        resetColor.a = 1;
        displayText.color = resetColor;

        yield return new WaitForSeconds(displayTime);

        while (displayText.color.a > 0)
        {
            Color displayColor = displayText.color;
            displayColor.a -= Time.deltaTime / fadeTime;
            displayText.color = displayColor;
            yield return null;
        }
        yield return null;
    }
}