using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Config/Weapon config", order = 52)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private PlayerWeapon _prefab;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField][Range(0, 3)] private float _maxDelayAfterShot;

    public int Id => _id;
    public string Name => _name;
    public PlayerWeapon Prefab => _prefab;
    public Bullet BulletPrefab => _bulletPrefab;
    public float MaxDelayAfterShot => _maxDelayAfterShot;
}
