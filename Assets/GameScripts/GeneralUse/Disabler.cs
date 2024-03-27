using UnityEngine;

public class Disabler : MonoBehaviour
{
    public float deathTime;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

		if (timer >= deathTime)
		{
			timer = 0;
			gameObject.SetActive(false);
		}
    }

}
