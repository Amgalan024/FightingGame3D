using System;
using Cysharp.Threading.Tasks;
using MVC.Configs;
using MVC.Views;
using UnityEngine;

public class PlayerModel
{
    public const int PLAYER1_NUMBER = 1;
    public const int PLAYER2_NUMBER = 2;
    public event Action<PlayerModel, PlayerAttackHitBoxView> OnPlayerAttacked;
    public event Action OnPlayerTurned;
    public event Action OnLose;
    public event Action OnPlayerRefreshed;
    public event Action<int> OnHPChanged;
    public event Action<int> OnDamageChanged;
    public event Action OnWin;

    public AsyncReactiveProperty<bool> IsGrounded { get; } = new AsyncReactiveProperty<bool>(true);
    public AsyncReactiveProperty<bool> IsAttacking { get; } = new AsyncReactiveProperty<bool>(false);
    public AsyncReactiveProperty<bool> IsDoingCombo { get; } = new AsyncReactiveProperty<bool>(false);
    public AsyncReactiveProperty<bool> IsBlocking { get; } = new AsyncReactiveProperty<bool>(false);
    public AsyncReactiveProperty<bool> IsCrouching { get; } = new AsyncReactiveProperty<bool>(false);
    public int Number { get; }
    public Sprite Icon { get; }
    public int MaxHealthPoints { get; }
    public int MaxEnergyPoints { get; }
    public float MaxMovementSpeed { get; }
    public float JumpForce {  get; }
    public float MovementSpeed { set; get; }
    public int HealthPoints { private set; get; }
    public int EnergyPoints { private set; get; }
    public int RoundScore { private set; get; }
    public int PunchDamage { private set; get; }
    public int KickDamage { private set; get; }

    public int CurrentJumpCount { set; get; }

    public bool AtLeftSide { set; get; } = true;

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

    public PlayerModel(int playerNumber, CharacterConfig characterConfig)
    {
        Number = playerNumber;
        Icon = characterConfig.Icon;
        MaxHealthPoints = characterConfig.MaxHealthPoints;
        MaxEnergyPoints = characterConfig.MaxEnergyPoints;
        HealthPoints = characterConfig.MaxHealthPoints;
        EnergyPoints = characterConfig.MaxEnergyPoints;
        MaxMovementSpeed = characterConfig.MovementSpeed;
        JumpForce = characterConfig.JumpForce;
        PunchDamage = characterConfig.PunchDamage;
        KickDamage = characterConfig.KickDamage;
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
            OnLose?.Invoke();
        }

        OnHPChanged?.Invoke(HealthPoints);
    }

    public void ScoreWin()
    {
        RoundScore++;
        OnWin?.Invoke();
    }

    public void TurnPlayer()
    {
        OnPlayerTurned?.Invoke();
    }

    public void InvokePlayerAttacked(PlayerAttackHitBoxView playerAttackHitBoxView)
    {
        OnPlayerAttacked?.Invoke(this, playerAttackHitBoxView);
    }
}