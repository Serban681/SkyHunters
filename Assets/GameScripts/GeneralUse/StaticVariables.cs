using UnityEngine;

public static class StaticVariables
{
	public static int curUnlockedPlanes;
	public static int maxNrOfPlanes = 14;

    public static bool musicOn;
    public static bool sfxOn;

    public static int gamesStarted = 0;

	public static bool[] ww1PlanesStats;
	public static bool[] ww2PlanesStats;

	public static int playerPlaneType = 0;

	public static int focussedPlane = 0;

	//Main Part Stuff
	public static bool enemiesRotating;
	public static float playerSpeed;
	public static float playerMaxSpeed;
	public static float playerMinSpeed;
	public static Vector2 playerDir;

	public static float playerHealth;

	public static int currentScore;
	public static int highScore = 0;

	public static int coins = 0;

	public static int curNrOfRockets;
	public static int maxNrOfRockets;
}
