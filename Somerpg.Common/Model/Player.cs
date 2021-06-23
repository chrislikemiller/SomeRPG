using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Somerpg.Common.Model
{
    public class Player : INotifyPropertyChanged
    {
        private string _name = "default";
        private int _level;
        private long _xp;
        private int _baseAttack = 0;
        private int _baseHP = 500;
        private Inventory _inventory;
        private Weapon _weapon;
        private Armor _armor;

        public Player()
        {
            Level = 1;
            XP = 0;
            Inventory = new Inventory { Gold = 0 };
            Armor = new Armor(tier_: 1);
            Weapon = new Weapon(tier_: 1);
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged();
            }
        }
        public long XP
        {
            get => _xp; set
            {
                _xp = value;
                OnPropertyChanged();
            }
        }

        public Armor Armor
        {
            get { return _armor; }
            set
            {
                _armor = value;
                OnPropertyChanged();
            }
        }
        public Weapon Weapon
        {
            get { return _weapon; }
            set
            {
                _weapon = value;
                OnPropertyChanged();
            }
        }
        public Inventory Inventory
        {
            get => _inventory;
            set
            {
                _inventory = value;
                OnPropertyChanged();
            }
        }
        public int BaseHP
        {
            get { return _baseHP; }
            set
            {
                _baseHP = value;
                OnPropertyChanged();
            }
        }
        public int BaseAttack
        {
            get { return _baseAttack; }
            set
            {
                _baseAttack = value;
                OnPropertyChanged();
            }
        }
        public int Attack => _baseAttack + Weapon.Stat;
        public int HP => _baseHP + Armor.Stat;

        public void AddXP(long addedXP_)
        {
            (Level, XP) = AddXPInner(Level, XP, addedXP_);
        }

        private (int, long) AddXPInner(int currentLevel_, long currentXP_, long addedXP_)
        {
            var nextlvlXP = GetXPForNextLevel(currentLevel_);
            if (currentXP_ + addedXP_ < nextlvlXP)
            {
                return (currentLevel_, currentXP_ + addedXP_);
            }
            return AddXPInner(currentLevel_ + 1, currentXP_ + addedXP_ - nextlvlXP, Math.Max(0, addedXP_ - nextlvlXP));
        }

        public long GetXPForNextLevel(int currentLevel_)
        {
            const double baseXP = 1000;
            const double exponent = 1.5;
            return (long)Math.Floor(baseXP * (Math.Pow(currentLevel_, exponent)));
        }


        public Player WithXP(long xp)
        {
            var copy = Copy();
            copy.AddXP(xp);
            return copy;
        }
        public Player WithLevel(int level)
        {
            var copy = Copy();
            copy.Level = level;
            return copy;
        }
        public Player WithRewards(ActionReward actionReward)
        {
            var copy = Copy();
            copy.AddXP(actionReward.XP);
            copy.Inventory.Merge(actionReward.Inventory);
            return copy;
        }

        public Player Copy()
        {
            return new Player
            {
                Name = Name,
                Level = Level,
                XP = XP,
                Armor = (Armor)Armor.Copy(),
                Weapon = (Weapon)Weapon.Copy(),
                Inventory = Inventory.Copy()
            };
        }
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
