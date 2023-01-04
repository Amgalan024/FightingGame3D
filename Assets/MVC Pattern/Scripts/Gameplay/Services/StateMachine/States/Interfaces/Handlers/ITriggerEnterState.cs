using UnityEngine;

namespace MVC.StateMachine.States
{
    public interface ITriggerEnterState
    {
        void OnTriggerEnter(Collider collider);
    }
}