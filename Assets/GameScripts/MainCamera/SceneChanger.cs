using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	private AsyncOperation operation;

	[HideInInspector]
	public string sceneName;

	public void LoadScene()
	{
        StartCoroutine(LoadAsychronously());
	}
    
    private IEnumerator LoadAsychronously()
    {
        operation = SceneManager.LoadSceneAsync(sceneName);

		operation.allowSceneActivation = false;

		yield return operation;
    }

    public void ActivateScene()
    {
		operation.allowSceneActivation = true;
	}
}
