using Somerpg.Client.Actions;
using Somerpg.Common.Model;
using Somerpg.Client.Util;
using Somerpg.Common.Util;
using System;
using System.Windows.Input;

namespace Somerpg.Client.ViewModel
{
    public class InventoryViewModel : NotifyBase
    {
        private Player _player;
        public InventoryViewModel(Player player_)
        {
            Player = player_;
            EquipItemCommand = new Command<Item>(item =>
            {
                switch (item)
                {
                    case Armor armor:
                        var oldArmor = Player.Armor.Copy();
                        Player.Armor = armor;
                        Player.Inventory.Items.Remove(armor);
                        Player.Inventory.Items.Add(oldArmor);
                        break;
                    case Weapon weapon:
                        var oldWep = Player.Weapon.Copy();
                        Player.Weapon = weapon;
                        Player.Inventory.Items.Remove(weapon);
                        Player.Inventory.Items.Add(oldWep);
                        break;
                    default:
                        throw new ArgumentException();
                }
            });
            DismantleItemCommand = new Command<Item>(item => DismantleItem(item));
            DismantleAllItemsCommand = new Command(() => DismantleAll());
            PlusItemCommand = new Command<Item>(item =>
            {
                Player.Inventory.Gold -= GameConstants.GetNextPlusCost(item.Tier, item.PlusValue++);
            },
            item => Player.Inventory.Gold >= GameConstants.GetNextPlusCost(item.Tier, item.PlusValue));
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
        public ICommand PlusItemCommand { get; set; }
        public ICommand EquipItemCommand { get; set; }
        public ICommand DismantleItemCommand { get; set; }
        public ICommand DismantleAllItemsCommand { get; set; }

        private void DismantleItem(Item item_)
        {
            Player.Inventory.Materials.AddDiminishing(item_.RequiredMaterialsToCraft);
            Player.Inventory.Items.Remove(item_);
        }

        private void DismantleAll()
        {
            foreach (var item in Player.Inventory.Items)
            {
                Player.Inventory.Materials.AddDiminishing(item.RequiredMaterialsToCraft);
            }
            Player.Inventory.Items.Clear();
        }
    }
}