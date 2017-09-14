using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	void Update () {
        if (Input.anyKey)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int numCurrentScene = currentScene.buildIndex;
            numCurrentScene++;
            SceneManager.LoadScene(numCurrentScene);
        }
    }
}
