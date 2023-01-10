using MVC_Pattern.Scripts.SettingsMenu.Configs;
using UnityEngine;
using VContainer;

namespace MVC_Pattern.Scripts.Services.Settings
{
    public class SettingsBuilder : BaseServiceBuilder
    {
        [SerializeField] private FightSettings _fightSettings;

        public override void Build(IContainerBuilder builder)
        {
            builder.RegisterInstance(_fightSettings);
        }
    }
}