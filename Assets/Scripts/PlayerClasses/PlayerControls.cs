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
        this.MoveForward = moveForward;
        this.MoveBackward = moveBackward;
        this.Jump = jump;
        this.Crouch = crouch;
        this.Punch = punch;
        this.Kick = kick;
    }
}
