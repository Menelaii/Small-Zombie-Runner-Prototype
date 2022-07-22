using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _offset;
    [SerializeField] private Transform _target;

    private void LateUpdate() 
    {
        if(_target == null)
            return;

        transform.position = new Vector3(transform.position.x, _target.position.y + _offset, transform.position.z);
    }
}
