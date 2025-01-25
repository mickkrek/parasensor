using UnityEngine;

public class DestroyChildren : MonoBehaviour
{
    public void DestroyAllButLast()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != transform.childCount - 1)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
