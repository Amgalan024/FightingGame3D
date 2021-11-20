using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlayerStates
{
    public Idle Idle { set; get; }
    public RunForward RunForward { set; get; }
    public RunBackward RunBackward { set; get; }
    public Jump Jump { set; get; }
    public Fall Fall { set; get; }
    public Crouch Crouch { set; get; }
    public Punch Punch { set; get; }
    public Kick Kick { set; get; }
}
