using UnityEngine;

namespace MVC.StateMachine.States
{
    public interface ITriggerEnterHandler
    {
        void OnTriggerEnter(Collider collider);
    }
}