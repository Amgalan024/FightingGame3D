using UnityEngine;

public class ComboHandler : MonoBehaviour
{
    protected PlayerControls playerControls;
    protected StateMachine stateMachine;
    protected PlayerModel PlayerModel;
    protected ComboAttack[] ComboAttacksList { set; get; }
    protected float ComboTimer { set; get; }

    private void OnPlayerTurned()
    {
        InitializeComboList();
    }

    public void InitializeCombo(PlayerModel playerModel, PlayerControls playerControls, StateMachine stateMachine)
    {
        playerModel = playerModel;
        playerControls = playerControls;
        stateMachine = stateMachine;
        playerModel.OnPlayerTurned += OnPlayerTurned;
        ComboListInitialize();
    }

    public virtual void ComboListInitialize()
    {
    }

    public virtual void InitializeComboList()
    {
    }

    public void ComboCheck(ComboAttack comboList)
    {
        if (Input.GetKeyDown(comboList.ControlsList[comboList.Count]))
        {
            if (comboList.Count == 0)
            {
                Debug.Log(comboList.ControlsList[comboList.Count]);
                ComboTimer = 2f;
            }

            if (ComboTimer > 0)
            {
                Debug.Log(comboList.ControlsList[comboList.Count]);
                comboList.Count++;
            }
            else
            {
                ResetComboCounts(ComboAttacksList);
            }
        }
        else if (Input.anyKeyDown)
        {
            comboList.Count = 0;
        }

        if (comboList.Count == comboList.ControlsList.Length)
        {
            ResetComboCounts(ComboAttacksList);
            stateMachine.PlayerStates.Combo.Name = comboList.Name;
            stateMachine.PlayerStates.Combo.Damage = comboList.Damage;
            stateMachine.ChangeState(stateMachine.PlayerStates.Combo);
        }
    }

    public void ComboTimeCount()
    {
        if (ComboTimer >= 0)
        {
            ComboTimer -= Time.deltaTime;
        }
    }

    public void ResetComboCounts(ComboAttack[] comboLists)
    {
        for (int i = 0; i < comboLists.Length; i++)
        {
            comboLists[i].Count = 0;
        }
    }
}