using System.Collections.Generic;
using MVC.Configs;
using MVC.Menu.Views;

namespace MVC.Menu.Services
{
    public class CharacterSelectMenuStorage
    {
        public Dictionary<CharacterSelectButtonView, CharacterConfig> CharacterConfigsByButtons { get; } =
            new Dictionary<CharacterSelectButtonView, CharacterConfig>(20);

        public List<CharacterSelectButtonView> CharacterButtonViews { get; } = new List<CharacterSelectButtonView>(20);

        public CharacterSelectMenuView MenuView { get; set; }
    }
}