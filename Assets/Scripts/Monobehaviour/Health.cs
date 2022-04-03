using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _healthCount = 100;
    [HideInInspector] public UnityEvent<int> OnChangeHealthEvent;

    [Header("VFX:")]
    [SerializeField] private ParticleSystem _destroyEffect;

    public int HealthCount => _healthCount;

    public void TakeHit(int damage)
    {
        if (_healthCount > 0) { _healthCount -= damage; }
        if (_healthCount < 0) { _healthCount = 0; }
        if (_healthCount == 0) { Die(); }

        OnChangeHealthEvent?.Invoke(-damage);
    }

    public void RestoreHealth(int healthCount)
    {
        _healthCount += healthCount;

        if (_healthCount > 100) { _healthCount = 100; }

        OnChangeHealthEvent?.Invoke(healthCount);
    }

    private void Die()
    {
        if (CompareTag("Enemy")) { Destroy(gameObject); }

        if (_destroyEffect) { _destroyEffect.Play(); }
    }
}