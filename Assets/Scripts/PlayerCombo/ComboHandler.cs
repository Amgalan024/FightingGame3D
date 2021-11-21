using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ComboHandler : MonoBehaviour
{
    protected PlayerControls playerControls;
    protected StateMachine stateMachine;
    protected Player player;
    public bool IsInitialized { set; get; }
    protected KeyCode[][] ComboList { set; get; }
    protected int[] ComboCount { set; get; }
    protected float ComboTime { set; get; }
    public void InitializeCombo(Player player, PlayerControls playerControls, StateMachine stateMachine)
    {
        this.player = player;
        this.playerControls = playerControls;
        this.stateMachine = stateMachine;
        this.IsInitialized = false;
        this.player.OnPlayerTurned += OnPlayerTurned;
        ComboListInitialize();
    }
    private void OnPlayerTurned()
    {
        ComboOnCharacterTurned();
    }
    public virtual void ComboListInitialize()
    {
    }
    public virtual void ComboOnCharacterTurned()
    {
    }
    public void ComboCheck(KeyCode[] Combo, ref int ComboCount, string ComboName, int Damage)
    {
        if (Input.GetKeyDown(Combo[ComboCount]))
        {
            if (ComboCount == 0)
            {
                Debug.Log(Combo[ComboCount]);
                ComboTime = 2f;
            }
            if (ComboTime > 0)
            {
                Debug.Log(Combo[ComboCount]);
                ComboCount++;
            }
            else
            {
                ResetComboCounts();
            }
        }
        else if (Input.anyKeyDown) // Изменить на проверку на нажатие клавиш входящих в управление персонажем
        {
            ComboCount = 0;
        }
        if (ComboCount == Combo.Length)
        {
            ResetComboCounts();
            stateMachine.PlayerStates.Combo.Name = ComboName;
            stateMachine.PlayerStates.Combo.Damage = Damage;
            stateMachine.ChangeState(stateMachine.PlayerStates.Combo);
        }
    }
    public void ComboTimeCount()
    {
        if (ComboTime >= 0)
        {
            ComboTime -= Time.deltaTime;
        }
    }
    public void ResetComboCounts()
    {
        for (int i = 0; i < this.ComboCount.Length; i++)
        {
            this.ComboCount[i] = 0;
        }
    }
}

