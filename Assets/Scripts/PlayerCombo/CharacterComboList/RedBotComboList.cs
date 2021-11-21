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
            ComboCheck(ComboList[0]);
            ComboCheck(ComboList[1]);
        }
    }
    public override void ComboListInitialize()
    {
        ComboList = new ComboAttack[2];
        for (int i = 0; i < ComboList.Length; i++)
        {
            ComboList[i] = new ComboAttack();
            ComboList[i].Count = 0;
        }
        InitializeComboList();
    }
    public override void InitializeComboList()
    {
        base.InitializeComboList();
        ComboList[0].Name = "Combo1";
        ComboList[0].Damage = 15;
        ComboList[0].ControlsList = new KeyCode[4] { playerControls.MoveBackward, playerControls.Crouch, playerControls.MoveForward, playerControls.Punch }; //Left Down Right P
        ComboList[1].Name = "Combo2";
        ComboList[1].Damage = 10;
        ComboList[1].ControlsList = new KeyCode[3] { playerControls.Crouch, playerControls.MoveBackward, playerControls.Punch }; // Down Left P
    }
}
