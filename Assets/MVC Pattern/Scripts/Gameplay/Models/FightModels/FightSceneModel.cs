using System;
using System.Collections.Generic;
using MVC_Pattern.Scripts.SettingsMenu.Configs;
using VContainer.Unity;

namespace MVC.Gameplay.Models
{
    public class FightSceneModel
    {
        public event Action OnRoundEnded;
        public event Action OnFightEnded;
        public List<LifetimeScope> PlayerLifetimeScopes { get; } = new List<LifetimeScope>(2);

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