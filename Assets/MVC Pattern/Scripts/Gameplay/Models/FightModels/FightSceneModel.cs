using System;
using System.Collections.Generic;
using MVC.Gameplay.Models.Player;
using VContainer.Unity;

namespace MVC.Gameplay.Models
{
    public class FightSceneModel
    {
        public List<LifetimeScope> PlayerLifetimeScopes { get; } = new List<LifetimeScope>(2);
    }
}