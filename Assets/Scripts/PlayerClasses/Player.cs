using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player 
{
    public const int PLAYER1_NUMBER = 1;
    public const int PLAYER2_NUMBER = 2;
    public event Action OnPlayerTurned;
    public event Action OnPlayerDied;
    public event Action<int> OnHPChanged;
    public event Action<int> OnDamageChanged;
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
    public bool IsCrouching{ set; get; }
    private bool atLeftSide;
    private bool atRightSide;
    public bool AtLeftSide
    {
        set
        {
            OnPlayerTurned?.Invoke();
            atLeftSide = value;
        }
        get
        {
            return atLeftSide;
        }
    }
    public bool AtRightSide
    {
        set
        {
            OnPlayerTurned?.Invoke();
            atRightSide = value;
        }
        get
        {
            return atRightSide;
        }
    }

    public Player(int playerNumber,Sprite icon,int maxHealthPoints,int maxEnergyPoints,int healthPoints, int energyPoints, float movementSpeed, float jumpForce, int punchDamage, int kickDamage)
    {
        this.Number = playerNumber;
        this.Icon = icon;
        this.MaxHealthPoints = maxHealthPoints;
        this.MaxEnergyPoints = maxEnergyPoints;
        this.HealthPoints = healthPoints;
        this.EnergyPoints = energyPoints;
        this.MaxMovementSpeed = movementSpeed;
        this.JumpForce = jumpForce;
        this.PunchDamage = punchDamage;
        this.KickDamage = kickDamage;
    }
    public void TakeDamage(int incomeDamage)
    {
        this.HealthPoints -= incomeDamage;
        if (HealthPoints <= 0)
        {
            OnPlayerDied?.Invoke();
        }
        OnHPChanged?.Invoke(this.HealthPoints);
    }
    public void SetDamage(int damage)
    {
        this.Damage = damage;
        OnDamageChanged?.Invoke(this.Damage);
    }
}
