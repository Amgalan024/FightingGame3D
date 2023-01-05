using Cysharp.Threading.Tasks;
using MVC.Menu.Models;
using MVC.Menu.Services;
using MVC.Menu.Services.CharacterSelectionStrategy;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace MVC.Menu.Controllers
{
    public class CharacterSelectController : IInitializable, ITickable
    {
        private readonly CharacterSelectMenuFactory _menuFactory;

        private readonly ICharacterSelectionStrategy _characterSelectionStrategy;

        public CharacterSelectController(CharacterSelectMenuFactory menuFactory,
            ICharacterSelectionStrategy characterSelectionStrategy)
        {
            _menuFactory = menuFactory;
            _characterSelectionStrategy = characterSelectionStrategy;
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
                await SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
                await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            }
        }
    }
}