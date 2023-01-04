using System;
using System.Collections.Generic;
using MVC.Gameplay.Models.Player;
using VContainer.Unity;

namespace MVC.Gameplay.Models
{
    public class FightSceneModel
    {
        public event Action<PlayerContainer> OnPlayerSideCheck;
        public List<LifetimeScope> PlayerLifetimeScopes { get; } = new List<LifetimeScope>(2);

        public void InvokePlayerSideCheck(PlayerContainer playerModel)
        {
            OnPlayerSideCheck?.Invoke(playerModel);
        }
    }
}