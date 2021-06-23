using Somerpg.Common.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Somerpg.Common.Model
{
    public class Inventory : NotifyBase, IEquatable<Inventory>
    {
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        private Material _materials = new Material();
        private long _gold;

        public long Gold
        {
            get => _gold;
            set
            {
                _gold = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public Material Materials
        {
            get => _materials;
            set
            {
                _materials = value;
                OnPropertyChanged();
            }
        }

        public void Merge(Inventory inv_)
        {
            Gold += inv_.Gold;
            Materials.Add(inv_.Materials);
            foreach (var item in inv_.Items)
            {
                Items.Add(item);
            }
        }

        public void Replace(Inventory inv_)
        {
            inv_ = inv_.Copy();
            Gold = inv_.Gold;
            Materials = inv_.Materials;
            Items.Clear();
            foreach (var item in inv_.Items)
            {
                Items.Add(item);
            }
        }

        public Inventory Copy()
        {
            //var temp = new Item[Items.Count];
            //Items.CopyTo(temp, 0);

            var copy = new Inventory
            {
                Gold = Gold,
                Materials = new Material(),
                Items = new ObservableCollection<Item>(Items.Select(x => x.Copy()))
            };
            copy.Materials.Add(Materials);
            return copy;
        }

        public bool Equals([AllowNull] Inventory other)
        {
            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Gold == other.Gold
                && Materials.Equals(other.Materials)
                && Items.Count == other.Items.Count
                && Items.All(x => other.Items.Contains(x));
        }
    }
}