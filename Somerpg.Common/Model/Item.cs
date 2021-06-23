using System;
using System.Diagnostics.CodeAnalysis;
using Somerpg.Common.Util;

namespace Somerpg.Common.Model
{
    public abstract class Item : NotifyBase, IEquatable<Item>
    {
        protected Material _requiredMaterial;
        protected int _baseStat;
        protected int _tier;
        protected int _plusValue;

        protected Item(int tier_, int baseStat_)
        {
            Tier = tier_;
            _baseStat = baseStat_;
            RequiredMaterialsToCraft = GetRequiredCraftMaterials((uint)tier_);
        }

        public int PlusValue
        {
            get => _plusValue;
            set
            {
                _plusValue = value;
                OnPropertyChanged();
            }
        }

        public int Tier
        {
            get => _tier;
            set
            {
                _tier = value;
                OnPropertyChanged();
            }
        }

        public int Stat
        {
            get
            {
                return (GetStatModifier(Tier) * _baseStat) 
                    + (PlusValue * _baseStat);
            }
        }

        public Material RequiredMaterialsToCraft
        {
            get => _requiredMaterial;
            set
            {
                _requiredMaterial = value;
                OnPropertyChanged();
            }
        }

        protected static int GetStatModifier(int tier_)
        {
            return tier_ == 1 
                ? 1
                : (tier_ - 1) * 5; // todo: make it so that it increases as the tier gets higher
        }

        protected abstract Material GetRequiredCraftMaterials(uint tier_);

        public abstract Item Copy();

        public override string ToString()
        {
            return $"T{Tier} +{PlusValue}";
        }

        public bool Equals([AllowNull] Item other)
        {
            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (GetType() != other.GetType())
            {
                return false;
            }

            return _baseStat == other._baseStat
                && Stat == other.Stat
                && Tier == other.Tier
                && PlusValue == other.PlusValue
                && RequiredMaterialsToCraft.Equals(other.RequiredMaterialsToCraft);
        }
    }
}