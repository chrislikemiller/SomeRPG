namespace Somerpg.Common.Model
{
    public class Weapon : Item
    {
        private const int BASE_STAT = 100;

        public Weapon(int tier_) : base(tier_, BASE_STAT)
        {
        }

        protected override Material GetRequiredCraftMaterials(uint tier_)
        {
            const int baseValue = 5;
            return new Material
            {
                Metal = tier_ * baseValue,
                Wood = tier_ * baseValue
            };
        }

        public override Item Copy()
        {
            return new Weapon(Tier)
            {
                PlusValue = PlusValue,
                RequiredMaterialsToCraft = RequiredMaterialsToCraft.Copy()
            };
        }
    }
}