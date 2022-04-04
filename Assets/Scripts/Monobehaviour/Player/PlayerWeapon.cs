using UnityEngine;
using System;

public class PlayerWeapon : MonoBehaviour
{
    private WeaponConfig _config;
    private CircleCollider2D _collider;
    private Transform _shootPoint;
    private PoolMonobehaviour<Bullet> _pool;

    private float _delayAfterShot = 50f;
    private float rotateZ = 0f;
    private const int _flashLightId = 2;

    private void OnEnable()
    {
        _shootPoint = transform.GetChild(0);
        _collider = GetComponent<CircleCollider2D>();
        _config = GetComponent<CollectableItem>().WeaponConfig;
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    public void Shoot()
    {
        if (_config.Id != _flashLightId)
        {
            if (_config == null || _shootPoint == null) { return; }

            if (_pool == null)
            {
                _pool = new PoolMonobehaviour<Bullet>(_config.BulletPrefab, _shootPoint, 5);
            }

            if (_delayAfterShot >= _config.MaxDelayAfterShot)
            {
                //if (_shootEffect) { _shootEffect.Play(); }
                Bullet bullet = _pool.GetObject(false);

                bullet.transform.position = _shootPoint.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, rotateZ);
                bullet.gameObject.SetActive(true);

                _delayAfterShot = 0;
            }

            try
            {
                _delayAfterShot = checked(_delayAfterShot + Time.fixedDeltaTime);
            }
            catch (OverflowException) { _delayAfterShot = _config.MaxDelayAfterShot; }
        }
    }

    private void Rotate()
    {
        if (!_collider.enabled)
        {
            Vector2 selfPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 difference = PlayerController.Instance.WorldMousePosition - selfPosition;

            rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            float rotateX = (rotateZ > 90 || rotateZ < -90) ? 180 : 0;
            float OnEndRotateZ = (rotateZ > 90 || rotateZ < -90) ? -rotateZ : rotateZ;

            transform.rotation = Quaternion.Euler(rotateX, 0, OnEndRotateZ);
        }
    }

    private void OnDestroy()
    {
        if (_pool != null)
        {
            _pool = PoolMonobehaviour<Bullet>.DestroyPool(ref _pool);
        }
    }
}
