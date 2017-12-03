using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public string sceneName = "UI";

    public bool additive = true;

    public float timeBeforeLoading = 0;

    private float currentTime = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        while (currentTime <= timeBeforeLoading)
        {
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (additive)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(sceneName);

        yield return null;
    }

	
}
