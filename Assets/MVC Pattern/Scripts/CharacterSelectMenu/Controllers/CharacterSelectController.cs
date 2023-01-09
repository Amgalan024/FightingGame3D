using Cysharp.Threading.Tasks;
using MVC.Configs;
using MVC.Menu.Models;
using MVC.Menu.Services;
using MVC.Menu.Services.CharacterSelectionStrategy;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Services;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace MVC.Menu.Controllers
{
    public class CharacterSelectController : IInitializable, ITickable
    {
        private readonly CharacterSelectMenuConfig _characterSelectMenuConfig;
        private readonly CharacterSelectMenuFactory _menuFactory;

        private readonly ICharacterSelectionStrategy _characterSelectionStrategy;
        private readonly ILoadingScreenService _loadingScreenService;

        public CharacterSelectController(CharacterSelectMenuFactory menuFactory,
            ICharacterSelectionStrategy characterSelectionStrategy, ILoadingScreenService loadingScreenService,
            CharacterSelectMenuConfig characterSelectMenuConfig)
        {
            _menuFactory = menuFactory;
            _characterSelectionStrategy = characterSelectionStrategy;
            _loadingScreenService = loadingScreenService;
            _characterSelectMenuConfig = characterSelectMenuConfig;
        }

        void IInitializable.Initialize()
        {
            _menuFactory.CreateMenuView();
            _menuFactory.CreateCharacterSelectButtons();

            _characterSelectionStrategy.Initialize();

            _characterSelectionStrategy.OnCharactersSelected += OnCharactersSelected;
        }

        void ITickable.Tick()
        {
            _characterSelectionStrategy.HandlePlayerSelection();
        }

        private void OnCharactersSelected(SelectedCharactersContainer charactersContainer)
        {
            LoadFightSceneAsync(charactersContainer).Forget();
        }

        private async UniTaskVoid LoadFightSceneAsync(SelectedCharactersContainer charactersContainer)
        {
            using (LifetimeScope.Enqueue(builder => builder.RegisterInstance(charactersContainer)))
            {
                await _loadingScreenService.ShowAsync<MainMenuLoadingScreenView>();

                await Addressables.LoadSceneAsync(_characterSelectMenuConfig.FightScene);

                await _loadingScreenService.HideAsync<MainMenuLoadingScreenView>();
            }
        }
    }
}