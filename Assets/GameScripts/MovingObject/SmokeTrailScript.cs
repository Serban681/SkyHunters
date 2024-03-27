using UnityEngine;

public class SmokeTrailScript : MonoBehaviour
{
	private ParticleSystem particleEffect;
	public float destroyTime = 8f;

	private void Start()
	{
		particleEffect = GetComponent<ParticleSystem>();
	}

	public void PlaneDestroyed()
	{
		particleEffect.Stop();
		Destroy(gameObject, destroyTime);
	}
}
