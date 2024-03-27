using System.Collections;
using UnityEngine;

public class BarrageReloadUIScript : MonoBehaviour
{
	private Animator[] anims;
	private bool canDissappear;

    // Start is called before the first frame update
    void Start()
    {
		anims = GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(StaticVariables.curNrOfRockets);

		if (StaticVariables.curNrOfRockets == StaticVariables.maxNrOfRockets && canDissappear)
		{
			StartCoroutine(MakeAllDissappear());
		}
		else
		{
			for (int i = 0; i < anims.Length; i++)
			{
				if (StaticVariables.curNrOfRockets > i)
				{
					anims[i].SetBool("Appear", true);
					if(i + 1 == StaticVariables.curNrOfRockets)
					{
						canDissappear = true;
					}
				}
				else
					anims[i].SetBool("Appear", false);
			}
		}

		if (StaticVariables.curNrOfRockets != StaticVariables.maxNrOfRockets)
			canDissappear = false;
	}


	private IEnumerator MakeAllDissappear()
	{
		yield return new WaitForSeconds(1f);
		foreach(Animator anim in anims)
		{
			anim.SetBool("Appear", false);
		}
	}
}
