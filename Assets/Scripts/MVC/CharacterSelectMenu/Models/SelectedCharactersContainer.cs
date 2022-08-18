using System.Collections.Generic;
using MVC.Configs;

namespace MVC.Menu.Models
{
    public class SelectedCharactersContainer
    {
        public List<CharacterConfig> PlayerConfigs { get; } = new List<CharacterConfig>(2);
    }
}