using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _triggered;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.TryGetComponent(out Human human))
        {
            _triggered?.Invoke();
        }
    }
}
