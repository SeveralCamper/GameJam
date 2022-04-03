using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpSpeed = 1.0f;
    private Vector3 _offset;
    private Vector3 _targetPos;

    private void Start()
    {
        if (!_target) { return; }

        _offset = transform.position - _target.position;
    }

    private void FixedUpdate()
    {
        if (!_target) { return; }

        _targetPos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _targetPos, _lerpSpeed * Time.fixedDeltaTime);
    }
}
