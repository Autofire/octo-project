using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public string sceneName;
	public float minWaitTime;

	public void StartLoad() {
		StartCoroutine(LoadYourAsyncScene());
	}

	IEnumerator LoadYourAsyncScene() {

		yield return new WaitForSecondsRealtime(minWaitTime);

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		while(!asyncLoad.isDone) {
			yield return null;
		}
	}
}
