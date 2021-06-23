namespace Somerpg.Common.Model
{
    public class Armor : Item
    {
        private const int BASE_STAT = 1000;

        public Armor(int tier_) : base(tier_, BASE_STAT)
        {
        }

        protected override Material GetRequiredCraftMaterials(uint tier_)
        {
            const int baseValue = 5;
            return new Material { Leather = tier_ * baseValue };
        }

        public override Item Copy()
        {
            return new Armor(Tier)
            {
                PlusValue = PlusValue,
                RequiredMaterialsToCraft = RequiredMaterialsToCraft.Copy()
            };
        }
    }
}