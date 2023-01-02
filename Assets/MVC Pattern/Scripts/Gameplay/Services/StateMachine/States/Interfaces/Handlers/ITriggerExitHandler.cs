using UnityEngine;

namespace MVC.StateMachine.States
{
    public interface ITriggerExitHandler
    {
        void OnTriggerExit(Collider collider);
    }
}