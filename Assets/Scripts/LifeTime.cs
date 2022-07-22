using UnityEngine;
using System.Collections;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float _seconds;

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterDelay(_seconds));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
