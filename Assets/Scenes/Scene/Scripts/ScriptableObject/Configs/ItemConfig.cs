using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Config/Item config", order = 52)]
public class ItemConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField][Range(0, 100)] private int _amount;
    [SerializeField] private GameObject _prefab;

    public string Name => _name;
    public int Amount => _amount;
    public GameObject Prefab => _prefab;
}
