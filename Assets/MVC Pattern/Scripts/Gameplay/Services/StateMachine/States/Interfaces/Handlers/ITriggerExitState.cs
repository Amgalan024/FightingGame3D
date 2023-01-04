using UnityEngine;

namespace MVC.StateMachine.States
{
    public interface ITriggerExitState
    {
        void OnTriggerExit(Collider collider);
    }
}