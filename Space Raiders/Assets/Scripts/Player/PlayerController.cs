using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent<PlayerController> OnChange { get; private set; }
    [field: SerializeField]
    public UnityEvent<PlayerController> OnDestroyed { get; private set; }
    [field: SerializeField]
    public Sprite LeanLeft { get; private set; }
    [field: SerializeField]
    public Sprite LeanRight { get; private set; }
    [field: SerializeField]
    public Sprite Forward { get; private set; }

    [field: SerializeField]
    public float Speed { get; private set; } = 5;

    [field: SerializeField]
    public float DamageBoost { get; private set; } = 3;
    public bool HasDamageBoost => DamageBoost > 0;
    public bool IsVisible => !HasDamageBoost || Mathf.Sin(Time.time * 20) > 0;
    [field: SerializeField]
    private int _shieldPower = 0;

    /// <summary>
    /// The ship's ShieldPower is a value is clamped between 0 and 5.
    /// </summary>
    public int ShieldPower
    {
        get => _shieldPower;
        set
        {
            _shieldPower = Mathf.Clamp(value, 0, 5);
            OnChange.Invoke(this);
        }
    }

    [field: SerializeField]
    public Weapon Weapon { get; set; }

    [field: SerializeField]
    public Transform FrontLaserSpawn { get; private set; }

    [field: SerializeField]
    public Vector2 Velocity { get; private set; } = new(0, 0);

    [field: SerializeField]
    public Vector2 Min { get; private set; }

    [field: SerializeField]
    public Vector2 Max { get; private set; }

    public static PlayerController Spawn(PlayerController template, GameController gc)
    {
        PlayerController pc = Instantiate(template);
        pc.OnDestroyed.AddListener(gc.DestroyPlayer);
        return pc;
    }

    void Start()
    {
        OnChange.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleFire();
        HandleDamageBoost();
        MoveShip();
        UpdateSprite();
    }

    private void HandleDamageBoost()
    {
        this.GetComponent<Renderer>().enabled = IsVisible;
        DamageBoost -= Time.deltaTime;
        DamageBoost = Mathf.Max(0, DamageBoost);
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0) throw new System.ArgumentException("Cannot take a non-positive amount of damage.");
        if (ShieldPower <= 0)
        {
            OnDestroyed.Invoke(this);
        }
        else
        {
            ShieldPower -= amount;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerImpactor asImpactor = other.GetComponent<PlayerImpactor>();
        if (asImpactor != null && DamageBoost <= 0)
        {
            asImpactor.OnImpact(this);
            TakeDamage(1);
        }
    }

    private void HandleFire()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Weapon.Fire(FrontLaserSpawn);
        }
    }

    private void UpdateSprite()
    {
        if (Velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().sprite = LeanLeft;
        }
        else if (Velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().sprite = LeanRight;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = Forward;
        }
    }

    private void HandleMovement()
    {
        Velocity = HandleHorizontal(Input.GetAxis("Horizontal"));
        Velocity += HandleVertical(Input.GetAxis("Vertical"));
    }

    private Vector2 HandleVertical(float v) => new(0, Mathf.Clamp(v, -1, 1));
    private Vector2 HandleHorizontal(float h) => new(Mathf.Clamp(h, -1, 1), 0);

    private void MoveShip()
    {
        float newX = transform.position.x + (Velocity.x * Speed * Time.deltaTime);
        float newY = transform.position.y + (Velocity.y * Speed * Time.deltaTime);
        newX = Mathf.Clamp(newX, Min.x, Max.x);
        newY = Mathf.Clamp(newY, Min.y, Max.y);
        transform.position = new Vector2(newX, newY);
    }


}
