using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private Rigidbody2D _rigidbody;

    private Vector3 _moveDirection;
    private IInputModule _inputModule;

    public float Velocity => _rigidbody.velocity.magnitude;

    public void Init(IInputModule inputModule)
    {
        _inputModule = inputModule;
    }

    private void Update()
    {
        _moveDirection = _inputModule.GetInput();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position + _moveDirection * _speed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, _leftBorder, _rightBorder);
        _rigidbody.MovePosition(newPosition);
    }
}
