using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public void Shake(float magnitude, float roughness, float fadeOutTime)
	{
		CameraShaker.Instance.ShakeOnce(magnitude, roughness, 0, fadeOutTime);
	}
}
