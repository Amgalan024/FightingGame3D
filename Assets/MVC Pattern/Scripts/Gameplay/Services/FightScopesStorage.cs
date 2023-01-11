using System.Collections.Generic;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.Gameplay.Services
{
    public class FightScopesStorage
    {
        public List<LifetimeScope> PlayerLifetimeScopes { get; } = new List<LifetimeScope>(2);

        public LifetimeScope CameraControllerScope { get; set; }
    }
}