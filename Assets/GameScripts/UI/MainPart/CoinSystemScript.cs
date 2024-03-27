using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystemScript : MonoBehaviour
{
	private Text coinAmmountText;
	public GameObject movingCoin;

	private Animator coinSystemAnim;

    // Start is called before the first frame update
    void Start()
    {
		coinAmmountText = transform.Find("CoinText").GetComponent<Text>();
		coinSystemAnim = transform.GetComponent<Animator>();

		coinAmmountText.text = StaticVariables.coins.ToString();
	}

	public IEnumerator MovingCoin(GameObject caller)
	{
		caller.transform.GetComponent<SpriteRenderer>().enabled = false;

		coinSystemAnim.SetBool("FadeIn", true);

		GameObject instance = Instantiate(movingCoin, transform.parent);

		yield return new WaitForSeconds(0.40f * 100 / 60);

		StaticVariables.coins++;

		coinAmmountText.text = StaticVariables.coins.ToString();

		yield return new WaitForSeconds(0.5f * 100 / 60);
		Destroy(instance);

		coinSystemAnim.SetBool("FadeIn", false);

		FindObjectOfType<EnemySpawner>().curCoins--;
		Destroy(caller);
	}

	public void Reset()
	{
		coinSystemAnim.SetBool("Changed", false);
	}

    public void AddCoins()
    {
        coinAmmountText.text = StaticVariables.coins.ToString();

		if(StaticVariables.playerHealth < 0 && StaticVariables.coins >= 100 && StaticVariables.curUnlockedPlanes != StaticVariables.maxNrOfPlanes)
		{
			EndBarsManager.instance.MakeBarAppear(EndBarsManager.instance.buyPlaneBar);
		}
    }
}