using System;
using UnityEngine;

public class PlayerModel
{
    public const int PLAYER1_NUMBER = 1;
    public const int PLAYER2_NUMBER = 2;
    public event Action OnPlayerTurned;
    public event Action OnPlayerDied;
    public event Action OnPlayerRefreshed;

    public event Action<int> OnHPChanged;
    public event Action<int> OnDamageChanged;
    public event Action<int> OnPlayerWonRound;

    public int RoundScore { private set; get; }
    public int Number { private set; get; }
    public Sprite Icon { private set; get; }
    public int HealthPoints { private set; get; }
    public int MaxHealthPoints { private set; get; }
    public int EnergyPoints { private set; get; }
    public int MaxEnergyPoints { private set; get; }
    public float MaxMovementSpeed { private set; get; }
    public float MovementSpeed { set; get; }
    public float JumpForce { private set; get; }
    public int Damage { private set; get; }
    public int PunchDamage { private set; get; }
    public int KickDamage { private set; get; }
    public bool IsGrounded { set; get; }
    public bool IsAttacking { set; get; }
    public bool IsDoingCombo { set; get; }
    public bool IsBlocking { set; get; }
    public bool IsCrouching { set; get; }

    private bool atLeftSide;
    private bool atRightSide;

    public bool AtLeftSide
    {
        set
        {
            OnPlayerTurned?.Invoke();
            atLeftSide = value;
        }
        get => atLeftSide;
    }

    public bool AtRightSide
    {
        set
        {
            OnPlayerTurned?.Invoke();
            atRightSide = value;
        }
        get => atRightSide;
    }

    public PlayerModel(int playerNumber, Sprite icon, int maxHealthPoints, int maxEnergyPoints, int healthPoints,
        int energyPoints, float movementSpeed, float jumpForce, int punchDamage, int kickDamage)
    {
        Number = playerNumber;
        Icon = icon;
        MaxHealthPoints = maxHealthPoints;
        MaxEnergyPoints = maxEnergyPoints;
        HealthPoints = healthPoints;
        EnergyPoints = energyPoints;
        MaxMovementSpeed = movementSpeed;
        JumpForce = jumpForce;
        PunchDamage = punchDamage;
        KickDamage = kickDamage;
    }

    public void RefreshPlayer()
    {
        HealthPoints = MaxHealthPoints;
        MovementSpeed = 0;
        OnHPChanged?.Invoke(HealthPoints);
        OnPlayerRefreshed?.Invoke();
    }

    public void TakeDamage(int incomeDamage)
    {
        HealthPoints -= incomeDamage;
        if (HealthPoints <= 0)
        {
            OnPlayerDied?.Invoke();
        }

        OnHPChanged?.Invoke(HealthPoints);
    }

    public void SetDamage(int damage)
    {
        Damage = damage;
        OnDamageChanged?.Invoke(Damage);
    }

    public void AddRoundScore()
    {
        RoundScore++;
        OnPlayerWonRound?.Invoke(RoundScore);
    }
}