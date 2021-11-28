using System;
using System.Collections.Generic;

namespace Dominos_API.DataEntities
{
    public class Menu
    {
        public MiscClass Misc { get; set; }

        public object Categorization { get; set; }

        public Dictionary<string, CouponsClass> Coupons { get; set; } = new Dictionary<string, CouponsClass>();

        public Dictionary<string, Dictionary<string, FlavorClass>> Flavors { get; set; } = new Dictionary<string, Dictionary<string, FlavorClass>>();

        public Dictionary<string, ProductClass> Products { get; set; } = new Dictionary<string, ProductClass>();

        public Dictionary<string, Dictionary<string, SidesClass>> Sides { get; set; } = new Dictionary<string, Dictionary<string, SidesClass>>();

        public Dictionary<string, Dictionary<string, SizesClass>> Sizes { get; set; } = new Dictionary<string, Dictionary<string, SizesClass>>();

        public Dictionary<string, Dictionary<string, ToppingsClass>> Toppings { get; set; } = new Dictionary<string, Dictionary<string, ToppingsClass>>();

        public object Variants { get; set; }

        public Dictionary<string, PreconfiguredProductsClass> PreconfiguredProducts { get; set; } = new Dictionary<string, PreconfiguredProductsClass>();

        public class PreconfiguredProductsClass
        {
            public string Code { get; set; }

            public string Description { get; set; }

            public string Name { get; set; }

            public string Size { get; set; }

            public string Options { get; set; }

            public string ReferencedProductCode { get; set; }
        }

        public Dictionary<string, ShortProductDescriptionsClass> ShortProductDescriptions { get; set; } = new Dictionary<string, ShortProductDescriptionsClass>();

        public Dictionary<string, CouponTiersClass> CouponTiers { get; set; } = new Dictionary<string, CouponTiersClass>();

        public Dictionary<string, UnsupportedProductsClass> UnsupportedProducts { get; set; } = new Dictionary<string, UnsupportedProductsClass>();

        public Dictionary<string, UnsupportedProductsClass> UnsupportedOptions { get; set; } = new Dictionary<string, UnsupportedProductsClass>();

        public Dictionary<string, CookingInstructionsClass> CookingInstructions { get; set; } = new Dictionary<string, CookingInstructionsClass>();

        public Dictionary<string, CookingInstructionGroupsClass> CookingInstructionGroups { get; set; } = new Dictionary<string, CookingInstructionGroupsClass>();

        public class FlavorClass
        {
            public string Code { get; set; }

            public string Description { get; set; }

            public bool Local { get; set; }

            public string Name { get; set; }

            public string SortSeq { get; set; }
        }

        public class SizesClass
        {
            public string Code { get; set; }

            public string Description { get; set; }

            public bool Local { get; set; }

            public string Name { get; set; }

            public string SortSeq { get; set; }
        }

        public class ToppingsClass
        {
            public object Availability { get; set; }

            public string Code { get; set; }

            public string Description { get; set; }

            public bool Local { get; set; }

            public string Name { get; set; }

            List<TagsClass> Tags { get; set; } = new List<TagsClass>();

            public class TagsClass
            {
                public bool Meat { get; set; }

                public bool NonMeat { get; set; }

                public bool Vege { get; set; }

                public bool WholeOnly { get; set; }

                public bool IgnoreQty { get; set; }

                public string ExclusiveGroup { get; set; }

                public bool Sauce { get; set; }

                public bool Side { get; set; }
            }
        }

        public class ShortProductDescriptionsClass
        {
            public string Code { get; set; }

            public string Description { get; set; }
        }

        public class CouponTiersClass
        {
            public string Code { get; set; }

            public Dictionary<string, CouponsClass> Coupons { get; set; } = new Dictionary<string, CouponsClass>();
        }

        public class UnsupportedProductsClass
        {
            public string PulseCode { get; set; }

            public string Description { get; set; }
        }

        public class CookingInstructionsClass
        {
            public string Code { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string Group { get; set; }
        }

        public class CookingInstructionGroupsClass
        {
            public string Code { get; set; }

            public string Name { get; set; }

            public object Tags //List<TagsClass>
            {
                get; set;
            }

            public class TagsClass
            {
                public string MaxOptions { get; set; }
            }
        }

        public class CouponsClass
        {
            public string Code { get; set; }

            public string ImageCode { get; set; }

            public string Description { get; set; }

            public string Name { get; set; }

            public string Price { get; set; }

            public TagsClass Tags { get; set; }
        }

        public class TagsClass
        {
            List<string> ValidServiceMethods { get; set; } = new List<string>();

            public DateTime? EffectiveOn { get; set; }

            public bool MultiSame { get; set; }

            public string Combine { get; set; }
        }

        public class ProductClass
        {
            public string AvailableToppings { get; set; }

            public string AvailableSides { get; set; }

            public string Code { get; set; }

            public string DefaultToppings { get; set; }

            public string DefaultSides { get; set; }

            public string Description { get; set; }

            public string ImageCode { get; set; }

            public bool Local { get; set; }

            public string Name { get; set; }

            public string ProductType { get; set; }

            public object Tags { get; set; }

            public List<string> Variants { get; set; }

            public class TagsClass
            {
                public bool BazaarVoice { get; set; }
            }
        }

        public class SidesClass
        {
            public object Availability { get; set; }

            public string Code { get; set; }

            public string Description { get; set; }

            public bool Local { get; set; }

            public string Name { get; set; }

            List<TagsClass> Tags { get; set; } = new List<TagsClass>();

            public class TagsClass
            {
                public bool Side { get; set; }

                public DateTime EffectiveOn { get; set; }
            }
        }

        public class MiscClass
        {
            public int Status { get; set; }

            public int StoreID { get; set; }

            public DateTime BusinessDate { get; set; }

            public DateTime StoreAsOfTime { get; set; }

            public string LanguageCode { get; set; }

            public string Version { get; set; }

            public string ExpiresOn { get; set; }
        }
    }
}
