using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int targetFrameRate = 500;

	public string[] initialScenes;
	public string initialLevel = "Level 1";

	[SerializeField]
	private string currentLevel;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = targetFrameRate;
		QualitySettings.vSyncCount = 0;

		foreach (string sceneName in initialScenes) {
			LoadSceneAdditiveDedupe(sceneName);
		}

		if (initialLevel != "") {
			SetLevel(initialLevel);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey("escape")) {
			Application.Quit();
		}
	}

	public void SetLevel (string sceneName) {
		Scene queriedScene = SceneManager.GetSceneByName(currentLevel);
		if (queriedScene.IsValid() && queriedScene.isLoaded) {
			SceneManager.UnloadSceneAsync(currentLevel);
		}

		Debug.LogFormat("Setting level... {0}", sceneName);

		currentLevel = sceneName;

		LoadSceneAdditiveDedupe(sceneName);

		// GameObject.Find("Player").GetComponent<PlayerBehavior>().Reset();
	}

	public void LoadSceneAdditiveDedupe (string sceneName) {
		Scene queriedScene = SceneManager.GetSceneByName(sceneName);
		if (queriedScene.IsValid() && queriedScene.isLoaded) return;

		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
	}
}
