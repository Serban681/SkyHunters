using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float destroyTime = 0f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
