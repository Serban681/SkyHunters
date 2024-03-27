using UnityEngine;

public class VariableLoader : MonoBehaviour
{
	private bool[] defaultValues1 = new bool[7];
	private bool[] defaultValues2 = new bool[7];

	private bool loadedBefore;

	// Start is called before the first frame update
	void Awake()
    {
		loadedBefore = ES3.Load("loadedBefore", false);

		if (!loadedBefore)
		{
			for (int i = 0; i < 7; i++)
			{
				defaultValues1[i] = false;
				defaultValues2[i] = false;
			}

			defaultValues1[0] = true;

			StaticVariables.ww1PlanesStats = ES3.Load("ww1Planes", defaultValues1);
			StaticVariables.ww2PlanesStats = ES3.Load("ww2Planes", defaultValues2);

			StaticVariables.focussedPlane = ES3.Load("focussedPlane", 0);
			StaticVariables.playerPlaneType = ES3.Load("focussedPlaneType", 0);

			StaticVariables.coins = ES3.Load("coins", 0);
			StaticVariables.highScore = ES3.Load("highScore", 0);

			ES3.Save("loadedBefore", true);
		}
		//ES3.Save<int>("highScore", StaticVariables.currentScore);
		//ES3.Save<int>("coins", StaticVariables.coins);
		//ES3.Save<int>("focussedPlane", focusedItem);
		//ES3.Save<int>("focussedPlaneType", StaticVariables.playerPlaneType);
	}
}
