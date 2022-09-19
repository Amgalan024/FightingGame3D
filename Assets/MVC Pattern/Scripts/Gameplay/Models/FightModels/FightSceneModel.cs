using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace MVC.Gameplay.Models
{
    public class FightSceneModel
    {
        public event Action<PlayerModel> OnPlayerSideCheck;
        public List<LifetimeScope> PlayerLifetimeScopes { get; } = new List<LifetimeScope>(2);

        public void InvokePlayerSideCheck(PlayerModel playerModel)
        {
            OnPlayerSideCheck?.Invoke(playerModel);
        }
    }
}