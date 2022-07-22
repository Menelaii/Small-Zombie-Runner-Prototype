using UnityEngine;

public class AttentionField : MonoBehaviour
{
    [SerializeField] private TargetLock _targeLock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IAttentionThing thing))
        {
            _targeLock.AddPotentialTarget(thing);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IAttentionThing thing))
        {
            _targeLock.RemovePotentialTarget(thing);
        }
    }
}
