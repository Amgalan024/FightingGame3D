using System.Collections.Generic;
using MVC.Configs;

namespace MVC.Menu.Models
{
    public class SelectedCharactersContainer
    {
        public Dictionary<int, CharacterConfig> CharacterConfigsByPlayer { get; } =
            new Dictionary<int, CharacterConfig>();
    }
}