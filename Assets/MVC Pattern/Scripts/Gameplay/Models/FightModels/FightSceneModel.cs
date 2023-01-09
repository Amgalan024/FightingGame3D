using System.Collections.Generic;
using VContainer.Unity;

namespace MVC.Gameplay.Models
{
    public class FightSceneModel
    {
        public List<LifetimeScope> PlayerLifetimeScopes { get; } = new List<LifetimeScope>(2);
    }
}