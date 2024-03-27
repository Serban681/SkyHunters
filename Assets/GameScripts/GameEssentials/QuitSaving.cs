using UnityEngine;

public class QuitSaving : MonoBehaviour
{
	private void OnApplicationPause(bool pause)
	{
		ES3.Save("ww1Planes", StaticVariables.ww1PlanesStats);
		ES3.Save("ww2Planes", StaticVariables.ww2PlanesStats);

		ES3.Save("focussedPlane", StaticVariables.focussedPlane); 
		ES3.Save("focussedPlaneType", StaticVariables.playerPlaneType);

		ES3.Save("coins", StaticVariables.coins);
		ES3.Save("highScore", StaticVariables.highScore);

		ES3.Save("loadedBefore", false);
	}

	private void OnApplicationQuit()
	{
		ES3.Save("ww1Planes", StaticVariables.ww1PlanesStats);
		ES3.Save("ww2Planes", StaticVariables.ww2PlanesStats);

		ES3.Save("focussedPlane", StaticVariables.focussedPlane);
		ES3.Save("focussedPlaneType", StaticVariables.playerPlaneType);

		ES3.Save("coins", StaticVariables.coins);
		ES3.Save("highScore", StaticVariables.highScore);

		ES3.Save("loadedBefore", false);
	}
}
