using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashes : MonoBehaviour
{
	private bool dissapear = false;

    void Update()
    {
		if(dissapear)
		{
			transform.GetComponent<Animator>().SetTrigger("Dissapear");
		}		
	}

	public void Enough()
	{
		dissapear = true;
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
