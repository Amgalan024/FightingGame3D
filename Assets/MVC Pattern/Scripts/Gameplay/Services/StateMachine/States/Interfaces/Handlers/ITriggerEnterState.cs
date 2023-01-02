using UnityEngine;

namespace MVC.StateMachine.States
{
    public interface ITriggerEnterState
    {
        abstract void OnTriggerEnter(Collider collider);
    }
}