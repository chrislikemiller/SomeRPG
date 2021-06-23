using Somerpg.Common.Model;
using Somerpg.Client.Util;
using Somerpg.Common.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Somerpg.Client.ViewModel
{
    public class CraftViewModel : NotifyBase
    {
        private const string ARMOR_STRING = "Armor";
        private const string WEAPON_STRING = "Weapon";

        private readonly IEnumerable<Item> _allPossibleItems;
        private Item _craftableItem;
        private string _selectedItem;
        private string _selectedTier;
        private Player _player;

        public CraftViewModel(Player player_)
        {
            Player = player_;
            _allPossibleItems = GetAllPossibleItems(Enumerable.Range(1, 3));

            Tiers = Enumerable.Range(1, GameConstants.HIGHEST_TIER).Select(x => $"T{x}").ToList();
            Items = new List<string>
            {
                ARMOR_STRING,
                WEAPON_STRING
            };
            _selectedItem = Items.First();
            _selectedTier = Tiers.First();
            UpdateSelectedItem();

            CraftCommand = new Command(() =>
            {
                Player.Inventory.Materials.Consume(_craftableItem.RequiredMaterialsToCraft);
                Player.Inventory.Items.Add(_craftableItem.Copy());
                OnPropertyChanged(nameof(Materials));
                OnPropertyChanged(nameof(HasEnoughMaterials));
            });
        }

        public ICommand CraftCommand { get; set; }
        public List<string> Items { get; }
        public List<string> Tiers { get; }

        public bool HasEnoughMaterials => Materials.Current.IsEnough(Materials.Required);
        public (Material Required, Material Current) Materials => (_craftableItem.RequiredMaterialsToCraft, Player.Inventory.Materials);

        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                UpdateSelectedItem();
            }
        }

        public string SelectedTier
        {
            get => _selectedTier;
            set
            {
                _selectedTier = value;
                OnPropertyChanged();
                UpdateSelectedItem();
            }
        }


        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                OnPropertyChanged();
            }
        }


        private Type GetItemType(string selectedItem)
        {
            return selectedItem switch
            {
                ARMOR_STRING => typeof(Armor),
                WEAPON_STRING => typeof(Weapon),
                _ => throw new ArgumentException()
            };
        }

        private IEnumerable<Item> GetAllPossibleItems(IEnumerable<int> tiers_)
        {
            foreach (var tier in tiers_)
            {
                yield return new Armor(tier);
                yield return new Weapon(tier);
            }
        }
        private void UpdateSelectedItem()
        {
            _craftableItem = _allPossibleItems
                .Where(x => x.Tier == int.Parse(SelectedTier.Substring(1, 1)) && x.GetType() == GetItemType(SelectedItem))
                .First();
            OnPropertyChanged(nameof(Materials));
            OnPropertyChanged(nameof(HasEnoughMaterials));
        }

    }
}