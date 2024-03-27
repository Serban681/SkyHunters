using UnityEngine;

public class EndBarsManager : MonoBehaviour
{
	public static EndBarsManager instance;

	protected Vector2 initialSizeDelta = new Vector2(0, 50);

	public Transform adsBar;
	public Transform buyPlaneBar;
	public Transform giftBar;

	private bool[] defaultValues1 = new bool[7];
	private bool[] defaultValues2 = new bool[7];

	private void Awake()
	{
		instance = this;

        StaticVariables.gamesStarted++;

		StaticVariables.curUnlockedPlanes = 0;

		for (int i = 0; i < StaticVariables.ww1PlanesStats.Length; i++)
			if(StaticVariables.ww1PlanesStats[i] == true)
			{
				StaticVariables.curUnlockedPlanes++;
			}

		for (int i = 0; i < StaticVariables.ww2PlanesStats.Length; i++)
			if (StaticVariables.ww2PlanesStats[i] == true)
			{
				StaticVariables.curUnlockedPlanes++;
			}

		Debug.Log(StaticVariables.curUnlockedPlanes);
	}

    public void ShowBars()
    {
        if(StaticVariables.coins >= 100 && StaticVariables.curUnlockedPlanes != StaticVariables.maxNrOfPlanes)
        {
            MakeBarAppear(buyPlaneBar);
        }

        MakeBarAppear(giftBar);

        if(StaticVariables.gamesStarted % 2 != 0)
        {
            MakeBarAppear(adsBar);
        }
    }

	public void MakeBarAppear(Transform bar)
	{
		bar.Find("BarItself").GetComponent<Animator>().SetBool("Show", true);
	}

	void MakeBarDisappear(Transform bar)
	{
		bar.Find("BarItself").GetComponent<Animator>().SetBool("Hide", true);
	}
}
