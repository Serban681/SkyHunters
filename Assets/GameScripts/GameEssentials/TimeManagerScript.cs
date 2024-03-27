using UnityEngine;

public class TimeManagerScript : MonoBehaviour
{
	public static TimeManagerScript instance;

	public float slowdownFactor = 0.05f;
	public float slowdownLength = 3f;

	private bool gamePaused = false;

	private void Awake()
	{
		instance = this;
	}

	void Update()
    {
		if (!gamePaused)
		{
			Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
			Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
		}
		else
		{
			Time.timeScale = 0f;
		}
    }

	public void DoSlowmotion(float slowdownFactor, float slowdownLength)
	{
		this.slowdownFactor = slowdownFactor;
		this.slowdownLength = slowdownLength;

		Time.timeScale = slowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;
	}

	public void TogglePause()
	{
		gamePaused = !gamePaused;
	}
}
