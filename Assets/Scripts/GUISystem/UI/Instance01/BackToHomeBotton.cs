﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackToHomeBotton : MonoBehaviour {
    public int sceneHome = 0;

	// Use this for initialization
	void Start () {
	
	}

    public void BackToHomeClicked()
    {
        SceneManager.LoadScene(sceneHome);
    }
}
