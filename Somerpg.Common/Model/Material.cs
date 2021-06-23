using Somerpg.Common.Util;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Somerpg.Common.Model
{
    public class Material : NotifyBase, IEquatable<Material>
    {
        public const double DIMINISHING_FACTOR = 0.75;

        private uint _metal;
        private uint _wood;
        private uint _leather;

        public uint Metal
        {
            get => _metal;
            set
            {
                _metal = value;
                OnPropertyChanged();
            }
        }

        public uint Wood
        {
            get => _wood;
            set
            {
                _wood = value;
                OnPropertyChanged();
            }
        }

        public uint Leather
        {
            get => _leather;
            set
            {
                _leather = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnough(Material mats_)
        {
            return Wood >= mats_.Wood
                && Metal >= mats_.Metal
                && Leather >= mats_.Leather;
        }

        public void Add(Material mats_)
        {
            Leather += mats_.Leather;
            Metal += mats_.Metal;
            Wood += mats_.Wood;
        }

        public bool Consume(Material mats_)
        {
            if (!IsEnough(mats_))
            {
                return false;
            }
            
            Leather -= mats_.Leather;
            Metal -= mats_.Metal;
            Wood -= mats_.Wood;
            Debug.Assert(Wood <= mats_.Wood && Metal <= mats_.Metal && Leather <= mats_.Leather);
            return true;
        }

        public void AddDiminishing(Material mats_)
        {
            Leather += (uint)(mats_.Leather * DIMINISHING_FACTOR);
            Metal += (uint)(mats_.Metal * DIMINISHING_FACTOR);
            Wood += (uint)(mats_.Wood * DIMINISHING_FACTOR);
        }

        public Material Copy()
        {
            return new Material
            {
                Leather = Leather,
                Metal = Metal,
                Wood = Wood
            };
        }

        public bool Equals([AllowNull] Material other)
        {
            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Leather == other.Leather
                && Metal == other.Metal
                && Wood == other.Wood;
        }
    }
}