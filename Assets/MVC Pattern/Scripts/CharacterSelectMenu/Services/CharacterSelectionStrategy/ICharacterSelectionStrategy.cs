using System;
using MVC.Menu.Models;

namespace MVC.Menu.Services.CharacterSelectionStrategy
{
    public interface ICharacterSelectionStrategy
    {
        event Action<SelectedCharactersContainer> OnCharactersSelected;
        void Initialize();
        void HandlePlayerSelection();
    }
}