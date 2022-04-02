using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    private Camera _camera;
    private Rigidbody2D _rigidbody;
    [SerializeField] private Text text;

    [SerializeField][Range(3f, 7f)] private float _moveSpeed = 7f;

    private void Awake()
    {
        _input = new PlayerInput();
        _camera = Camera.main;

        _input.Player.Roll.performed += _ => { Roll(); };

        _input.Player.ReadText.performed += _ => { TextRead(); };
    }

    private void OnEnable() { _input.Enable(); }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void OnDisable() { _input.Disable(); }

    private void Rotate()
    {
        Vector2 mousePosition = _input.Player.Rotate.ReadValue<Vector2>();
        Vector2 worldMousePosition = _camera.ScreenToWorldPoint(mousePosition);
        Vector2 difference = worldMousePosition - GetSelfPositionXY();

        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotateZ);
    }

    private void Move()
    {
        if (_rigidbody == null) { return; }

        Vector2 direction = _input.Player.Movement.ReadValue<Vector2>();
        _rigidbody.velocity = new Vector2(direction.x * _moveSpeed, direction.y * _moveSpeed);
    }

    private void Roll()
    {
        // TO DO
    }

    private void TextRead()
    {
        text.GetComponent<TextScript>().say("hello from player");
    }

    private Vector2 GetSelfPositionXY() => new Vector2(transform.position.x, transform.position.y);
}
// if (_input.Player.Roll.ReadValue<float>() > 0) {} // pressed button check
