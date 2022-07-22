using UnityEngine;

public abstract class PickUpItem : MonoBehaviour, IAttentionThing
{
    [SerializeField] private AttentionPriority _attentionPriority;

    public AttentionPriority Priority => _attentionPriority;
    public Transform Transform => transform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Human human))
        {
            UseOn(human);
            Destroy(gameObject);
        }
    }

    protected abstract void UseOn(Human human);
}
