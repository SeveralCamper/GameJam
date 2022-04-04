using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryItemSlot
{
    private ItemConfig _item;
    private int _amount = 0;

    public ItemConfig Item => _item;
    public int Amount { get => _amount; set { if (value > 0) _amount = value; } }

    public InventoryItemSlot(ItemConfig item)
    {
        _item = item;
        _amount += item.Amount;
    }
}

[System.Serializable]
public class InventoryWeaponSlot
{
    public WeaponConfig _weapon;

    public WeaponConfig Weapon { get => _weapon; set { _weapon = value; } }

    public InventoryWeaponSlot(WeaponConfig weapon)
    {
        _weapon = weapon;
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItemSlot> _items = new List<InventoryItemSlot>();
    [SerializeField] private List<InventoryWeaponSlot> _weapons = new List<InventoryWeaponSlot>();
    [SerializeField] private GameObject[] _weaponsObj = new GameObject[6];
    [SerializeField] private SpriteRenderer[] _weaponsRendr = new SpriteRenderer[6];
    private Transform _weaponPosition;
    private Transform _dropPointTransform;
    private const int _sizeInventoryItems = 3;
    private const int _sizeInventoryWeapons = 2;
    [HideInInspector] public UnityEvent<PlayerWeapon> OnEquipWeaponEvent;

    public int CurrentIndexWeapon { get; private set; }

    public Inventory(List<InventoryWeaponSlot> weapons) => _weapons = weapons;
    public PlayerWeapon this[int index]
    {
        get => _weapons[index].Weapon.Prefab;
    }

    private void OnEnable()
    {
        _weaponPosition = transform.GetChild(0);
        _dropPointTransform = transform.GetChild(1);

        CurrentIndexWeapon = 0;
    }

    public void AddItem(ItemConfig item)
    {
        foreach (InventoryItemSlot slot in _items)
        {
            if (slot.Item.name == item.Name)
            {
                slot.Amount += item.Amount;
                return;
            }
        }

        if (_items.Count < _sizeInventoryItems) _items.Add(new InventoryItemSlot(item));
    }

    public void AddWeapon(WeaponConfig weapon)
    {
        WeaponConfig previousWeapon = null;

        if (_weapons.Count == _sizeInventoryWeapons)
        {
            previousWeapon = _weapons[CurrentIndexWeapon].Weapon;
            _weapons[CurrentIndexWeapon].Weapon = weapon;

            DropWeapon(ref previousWeapon);
            EquipWeapon(weapon, true);
        }

        if (_weapons.Count < _sizeInventoryWeapons)
        {
            _weapons.Add(new InventoryWeaponSlot(weapon));
            EquipWeapon(weapon);
        }
    }

    public void TakePreviousWeapon()
    {
        if (CurrentIndexWeapon - 1 != -1)
        {
            _weaponsObj[CurrentIndexWeapon].SetActive(false);

            CurrentIndexWeapon--;

            _weaponsObj[CurrentIndexWeapon].SetActive(true);
            OnEquipWeaponEvent?.Invoke(_weaponsObj[CurrentIndexWeapon].GetComponent<PlayerWeapon>());
        }
    }

    public void TakeNextWeapon()
    {
        if (CurrentIndexWeapon + 1 != _weapons.Count)
        {
            _weaponsObj[CurrentIndexWeapon].SetActive(false);

            CurrentIndexWeapon++;

            _weaponsObj[CurrentIndexWeapon].SetActive(true);
            OnEquipWeaponEvent?.Invoke(_weaponsObj[CurrentIndexWeapon].GetComponent<PlayerWeapon>());
        }
    }

    private void EquipWeapon(WeaponConfig weapon, bool isReplace = false)
    {
        PlayerWeapon weaponObj = Instantiate(weapon.Prefab, _weaponPosition);
        SpriteRenderer renderer = weaponObj.GetComponent<SpriteRenderer>();

        renderer.sortingOrder = 3;

        if (!isReplace)
        {
            weaponObj.gameObject.SetActive(false);

            _weaponsObj[_weapons.Count - 1] = weaponObj.gameObject;
            _weaponsRendr[_weapons.Count - 1] = renderer;
        }
        else
        {
            _weaponsObj[CurrentIndexWeapon] = weaponObj.gameObject;
            _weaponsRendr[CurrentIndexWeapon] = renderer;

            OnEquipWeaponEvent?.Invoke(weaponObj.gameObject.GetComponent<PlayerWeapon>());
        }

        weaponObj.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void DropWeapon(ref WeaponConfig weapon)
    {
        _weaponsRendr[CurrentIndexWeapon].sortingOrder = 1;

        Destroy(_weaponsObj[CurrentIndexWeapon]);
        Instantiate(weapon.Prefab, _dropPointTransform.position, Quaternion.identity);

        weapon = null;
    }
}
