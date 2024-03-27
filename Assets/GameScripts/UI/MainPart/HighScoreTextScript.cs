using System;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTextScript : MonoBehaviour
{
	private Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
		highScoreText = GetComponent<Text>();

		PlayerHealth.instance.OnPlayerDeath += _OnPlayerDeath;
    }

	private void _OnPlayerDeath(object sender, EventArgs e)
	{
		highScoreText.text = $"High Score: { StaticVariables.highScore.ToString()}";
	}
}
