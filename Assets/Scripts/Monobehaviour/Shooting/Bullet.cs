using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private BulletConfig _config;

    public BulletConfig Config => _config;

    protected abstract void Move();
    protected abstract void Destroy();
}
