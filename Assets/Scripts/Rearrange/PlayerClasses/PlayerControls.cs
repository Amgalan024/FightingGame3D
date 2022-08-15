using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerControls
{
    public KeyCode MoveForward { set; get; }
    public KeyCode MoveBackward { set; get; }
    public KeyCode Jump { set; get; }
    public KeyCode Crouch { set; get; }
    public KeyCode Punch { set; get; }
    public KeyCode Kick { set; get; }
    public PlayerControls(KeyCode moveForward, KeyCode moveBackward, KeyCode jump, KeyCode crouch, KeyCode punch, KeyCode kick)
    {
        MoveForward = moveForward;
        MoveBackward = moveBackward;
        Jump = jump;
        Crouch = crouch;
        Punch = punch;
        Kick = kick;
    }
}
