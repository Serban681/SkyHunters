using System.Collections;
using System;
using UnityEngine;

public class FancyShootButtonScript : MonoBehaviour
{
	private PlaneShooter controller;
    public GameObject flash;
    private Vector3 touchPos;

    // Start is called before the first frame update
    void Start()
    {
		controller = PlayerController.instance.transform.GetComponent<PlaneShooter>();

		PlayerHealth.instance.OnPlayerDeath += _OnPlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
		/*
        if (StaticVariables.playerHealth <= 0)
        {
            this.enabled = false;
        }

        if (StaticVariables.playerHealth > 0 && controller == null)
        {
            controller = FindObjectOfType<PlayerController>();
        }
		*/

#if UNITY_EDITOR
		/*
		if (Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width / 2)
        {
            controller.Shoot();
        }
		*/
#endif

#if UNITY_ANDROID

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).position.x > Screen.width / 2)
            {
                touchPos = Input.GetTouch(i).position;
                controller.Shoot();
                break;
            }
        }
#endif
    }

    public void Flash()
    {
		//Instantiate(flash, new Vector3(Screen.width / 1.3f + UnityEngine.Random.Range(-10f, 10f), Screen.height / 3 + UnityEngine.Random.Range(-50f, 50f)), Quaternion.identity, GameObject.Find("Canvas").transform);

#if UNITY_EDITOR
		//Instantiate(flash, Input.mousePosition, Quaternion.identity, GameObject.Find("Canvas").transform);
#endif

#if UNITY_ANDROID
		Instantiate(flash, touchPos, Quaternion.identity, GameObject.Find("Canvas").transform);
#endif

	}

	public void _OnPlayerDeath(object sender, EventArgs e)
	{
		enabled = false;
	}
}
