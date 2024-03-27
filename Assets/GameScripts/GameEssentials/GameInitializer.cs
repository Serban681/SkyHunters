using UnityEngine;

public class GameInitializer : MonoBehaviour
{
	private void Start()
	{
		AudioManagerScript.instance.Play("GameplayTheme");
	}
}
