using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunctions
{
    public static bool Chance(int chance)
	{
		int x = Random.Range(0, 100);

		if(x <= chance)
			return true;
		else
			return false;
	}

	public static float LerpValue (float initValue, float targetValue, float lerpTime)
	{
		float percentageComplete = Time.deltaTime / lerpTime;

		var result = Mathf.Lerp(initValue, targetValue, percentageComplete);

		return result;
	}
}
