using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RedBotComboList : ComboHandler
{
    private void Update()
    {
        ComboTimeCount();
        if (!player.IsDoingCombo)
        {
            //Сортировать по колву кнопок в комбе, от самых длинных комбинаций до самых коротких 
            ComboCheck(ComboAttacksList[0]);
            ComboCheck(ComboAttacksList[1]);
        }
    }
    public override void ComboListInitialize()
    {
        ComboAttacksList = new ComboAttack[2];
        for (int i = 0; i < ComboAttacksList.Length; i++)
        {
            ComboAttacksList[i] = new ComboAttack();
            ComboAttacksList[i].Count = 0;
        }
        InitializeComboList();
    }
    public override void InitializeComboList()
    {
        base.InitializeComboList();
        ComboAttacksList[0].Name = "Combo1";
        ComboAttacksList[0].Damage = 15;
        ComboAttacksList[0].ControlsList = new KeyCode[4] { playerControls.MoveBackward, playerControls.Crouch, playerControls.MoveForward, playerControls.Punch }; //Left Down Right P
        ComboAttacksList[1].Name = "Combo2";
        ComboAttacksList[1].Damage = 10;
        ComboAttacksList[1].ControlsList = new KeyCode[3] { playerControls.Crouch, playerControls.MoveBackward, playerControls.Punch }; // Down Left P
    }
}
