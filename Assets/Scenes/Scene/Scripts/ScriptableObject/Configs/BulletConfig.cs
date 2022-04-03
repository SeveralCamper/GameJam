using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Config/Bullet", order = 52)]
public class BulletConfig : ScriptableObject
{
    [SerializeField] [Range(0, 100)] private int _damage;
    [SerializeField] [Range(0, 100)] private int _critChance;
    [SerializeField] [Range(0, 10)] private float _flightSpeed;
    [SerializeField] [Range(0, 4)] private float _force;

    public int Damage => _damage;
    public int CritChance => _critChance;
    public float FlightSpeed => _flightSpeed;
    public float Force => _force;
}
