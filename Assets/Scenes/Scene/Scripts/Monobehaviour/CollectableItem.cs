using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CollectableItem : MonoBehaviour
{
    [SerializeField] private LayerMask _objectsLM;

    [SerializeField] private ItemConfig _item;
    [SerializeField] private WeaponConfig _weapon;

    private PlayerController _player;
    private Inventory _inventory;
    private bool _playerInTrigger = false;

    private void OnDisable() { if (_player) _player.OnPickUpActionEvent.RemoveListener(PickUp); }

    private void PickUp()
    {
        if (_inventory == null) { return; }
        if (_item == null && _weapon == null) return;

        if (_playerInTrigger && _player)
        {
            if (_item != null) { _inventory.AddItem(_item); }
            if (_weapon != null) { _inventory.AddWeapon(_weapon); }

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