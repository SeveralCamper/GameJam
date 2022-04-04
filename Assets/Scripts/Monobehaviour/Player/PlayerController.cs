using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(3f, 7f)] private float _moveSpeed = 7f;

    private Camera _camera;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Inventory _inventory;
    private PlayerWeapon _weapon;
    private PlayerInput _input;
    [HideInInspector] public UnityEvent OnPickUpActionEvent;
    
    public static PlayerController Instance { get; private set; }
    public Inventory Inventory => _inventory;
    public PlayerWeapon Weapon => _weapon;
    public Vector2 WorldMousePosition { get; private set; }

    private void Awake()
    {
        InputInitialize();
    }

    private void Start()
    {
        if (Instance == null) { Instance = this; }
        else if (Instance == this) { Destroy(gameObject); }

        GetComponents();

        _camera = Camera.main;
        _inventory.OnEquipWeaponEvent.AddListener(ChangeWeapon);
    }

    private void OnEnable() { _input.Enable(); }

    private void FixedUpdate()
    {
        Rotate();
        Move();

        if (_input.Player.Shoot.ReadValue<float>() > 0)
        {
            _weapon.Shoot();
        }
    }

    private void OnDisable() { _input.Disable(); }

    private void ChangeWeapon(PlayerWeapon newWeapon) { _weapon = newWeapon; }

    private void Rotate()
    {
        Vector2 mousePosition = _input.Player.Rotate.ReadValue<Vector2>();
        WorldMousePosition = _camera.ScreenToWorldPoint(mousePosition);
        Vector2 difference = WorldMousePosition - GetSelfPositionXY();

        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float rotateY = 0;

        if (rotateZ > 90 || rotateZ < -90)
        {
            rotateY = 180;
        }

        transform.rotation = Quaternion.Euler(0, rotateY, 0);

        if (_animator != null) { _animator.SetFloat("RotateZ", rotateZ); }
    }

    private void Move()
    {
        if (_rigidbody == null) { return; }

        Vector2 direction = _input.Player.Movement.ReadValue<Vector2>();
        float movementSpeed = Mathf.Abs(direction.x + direction.y);

        _rigidbody.velocity = new Vector2(direction.x * _moveSpeed, direction.y * _moveSpeed);

        if (_animator != null) { _animator.SetFloat("MovementSpeed", movementSpeed); }
    }

    private Vector2 GetSelfPositionXY() => new Vector2(transform.position.x, transform.position.y);

    private void InputInitialize()
    {
        _input = new PlayerInput();

        _input.Player.PickUp.performed += _ => { OnPickUpActionEvent?.Invoke(); };
        _input.Player.TakePreviousWeapon.performed += _ => { _inventory.TakePreviousWeapon(); };
        _input.Player.TakeNextWeapon.performed += _ => { _inventory.TakeNextWeapon(); };
    }

    private void GetComponents()
    {
        _weapon = transform.GetChild(0).transform.GetChild(0).GetComponent<PlayerWeapon>();
        _inventory = GetComponent<Inventory>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
}
// if (_input.Player.Roll.ReadValue<float>() > 0) {} // pressed button check
