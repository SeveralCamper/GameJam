using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Transform _shootTransform;
    private CircleCollider2D _collider;

    private void OnEnable()
    {
        _shootTransform = transform.GetChild(0);
        _collider = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Shoot()
    {

    }

    public void Rotate()
    {
        if (!_collider.enabled)
        {
            Vector2 selfPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 difference = PlayerController.Instance.WorldMousePosition - selfPosition;

            float onStartRotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            float rotateX = (onStartRotateZ > 90 || onStartRotateZ < -90) ? 180 : 0;
            float OnEndRotateZ = (onStartRotateZ > 90 || onStartRotateZ < -90) ? -onStartRotateZ : onStartRotateZ;

            transform.rotation = Quaternion.Euler(rotateX, 0, OnEndRotateZ);
        }
    }
}
