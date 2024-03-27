using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreData
{
	public int highScore;
	//public bool[] ww1Planes;
	//public bool[] ww2Planes;

	public HighScoreData()
	{
		highScore = StaticVariables.highScore;
		/*
		ww1Planes = new bool[7];

		for(int i = 0; i < ww1Planes.Length; i++)
		{
			ww1Planes[i] = player.ww1Planes[i];
		}

		ww2Planes = new bool[7];

		for (int i = 0; i < ww2Planes.Length; i++)
		{
			ww2Planes[i] = player.ww2Planes[i];
		}
		*/
	}
}
