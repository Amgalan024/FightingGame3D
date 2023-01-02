using UnityEngine;

namespace MVC.StateMachine.States
{
    public interface ITriggerExitState
    {
        abstract void OnTriggerExit(Collider collider);
    }
}