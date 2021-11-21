using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BlueBotComboList : ComboHandler
{
    private void Update()
    {
        ComboTimeCount();
        if (!player.IsDoingCombo)
        {
            //Сортировать по колву кнопок в комбе, от самых длинных комбинаций до самых коротких 
            ComboCheck(ComboList[0], ref ComboCount[0], "Combo1", player.PunchDamage * 2);
            ComboCheck(ComboList[1], ref ComboCount[1], "Combo2", player.PunchDamage * 5);
        }
    }
    public override void ComboListInitialize()
    {
        ComboList = new KeyCode[2][];
        ComboCount = new int[2];
        for (int i = 0; i < ComboCount.Length; i++)
        {
            ComboCount[i] = 0;
        }
        ComboOnCharacterTurned();
    }
    public override void ComboOnCharacterTurned()
    {
        base.ComboOnCharacterTurned();
        ComboList[0] = new KeyCode[4] { playerControls.MoveBackward, playerControls.Crouch, playerControls.MoveForward, playerControls.Punch }; //Left Down Right P
        ComboList[1] = new KeyCode[3] { playerControls.Crouch, playerControls.MoveBackward, playerControls.Punch }; // Down Left P
    }
}
