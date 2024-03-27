using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
	public static ScoreText instance;

	private Text scoreText;

    // Start is called before the first frame update
    void Awake()
    {
		instance = this;

		StaticVariables.currentScore = 0;
		scoreText = GetComponent<Text>();
    }

	public void ChangeText()
	{
		scoreText.text = StaticVariables.currentScore.ToString();
	}
}
