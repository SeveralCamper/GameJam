using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CollectableItem : MonoBehaviour
{
    [SerializeField] private LayerMask _objectsLM;

    [SerializeField] private ItemConfig _itemConfig;
    [SerializeField] private WeaponConfig _weaponConfig;

    private PlayerController _player;
    private Inventory _inventory;
    private bool _playerInTrigger = false;

    public WeaponConfig WeaponConfig => _weaponConfig;

    private void OnDisable() { if (_player) _player.OnPickUpActionEvent.RemoveListener(PickUp); }

    private void PickUp()
    {
        if (_inventory == null) { return; }
        if (_itemConfig == null && _weaponConfig == null) return;

        if (_playerInTrigger && _player)
        {
            if (_itemConfig != null) { _inventory.AddItem(_itemConfig); }
            if (_weaponConfig != null) { _inventory.AddWeapon(_weaponConfig); }

            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_objectsLM.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (!_playerInTrigger)
            {
                if (_inventory == null) { _inventory = PlayerController.Instance.Inventory; }

                if (_player == null) 
                {
                    _player = PlayerController.Instance;
                    _player.OnPickUpActionEvent.AddListener(PickUp);
                }

                if (!_playerInTrigger) { _playerInTrigger = true; }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_objectsLM.value & (1 << collision.gameObject.layer)) != 0)
        {
            _playerInTrigger = false;
            _inventory = null;
        }
    }
}