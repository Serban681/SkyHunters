using UnityEngine;

[System.Serializable]
public class CloudArrangement
{
	//[HideInInspector]
	public string name;
	public GameObject[] clouds;
	public GameObject[] backClouds;
	public GameObject[] farBackClouds;
	public Color bgColor;
}

public class CloudGenerator : MonoBehaviour
{
	public GameObject rain;
	public SpriteRenderer background;
	public CloudArrangement[] cloudArrangements;

	private int curArrangement;

	public Transform cloudsPar;
	public Transform backCloudsPar;
	public Transform farBackCloudsPar;

    // Start is called before the first frame update
    void Awake()
    {
		curArrangement = Random.Range(0, 1);

		curArrangement = 0;

		background.color = cloudArrangements[curArrangement].bgColor;

        for(int i = -15; i < 15; i++)
            for(int j = -15; j < 15; j++)
            {
                GameObject instance = Instantiate(cloudArrangements[curArrangement].clouds[Random.Range(0, cloudArrangements[curArrangement].clouds.Length)], new Vector3(transform.position.x + i * 60 + Random.Range(-20, 20), transform.position.y + j * 60 + Random.Range(-20, 20), 0), Quaternion.identity);
				instance.transform.parent = cloudsPar;
            }
		
        for (int i = -15; i < 15; i++)
            for (int j = -15; j < 15; j++)
            {
				GameObject instance = Instantiate(cloudArrangements[curArrangement].backClouds[Random.Range(0, cloudArrangements[curArrangement].backClouds.Length)], new Vector3(transform.position.x + i * 60 + Random.Range(-20, 20) + 20, transform.position.y + j * 60 + Random.Range(-20, 20) + 20, 0), Quaternion.identity);
				instance.transform.parent = backCloudsPar;
			}

		for (int i = -15; i < 15; i++)
			for (int j = -15; j < 15; j++)
			{
				GameObject instance = Instantiate(cloudArrangements[curArrangement].farBackClouds[Random.Range(0, cloudArrangements[curArrangement].farBackClouds.Length)], new Vector3(transform.position.x + i * 60 + Random.Range(-30, 30) + 40, transform.position.y + j * 60 + Random.Range(-30, 30) + 40, 0), Quaternion.identity);
				instance.transform.parent = farBackCloudsPar;
			}

		if(curArrangement == 2)
		{
			transform.Find("Stars").gameObject.SetActive(true);
		}
		else if(curArrangement == 3)
		{
			rain.SetActive(true);
		}
	}
}
