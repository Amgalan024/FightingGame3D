using System;

namespace MVC.Gameplay.Models
{
    public class FightSceneModel
    {
        public event Action OnRoundEnded;
        public event Action OnFightEnded;

        public void InvokeRoundEnd()
        {
            OnRoundEnded?.Invoke();
        }

        public void InvokeFightEnd()
        {
            OnFightEnded?.Invoke();
        }
    }
}