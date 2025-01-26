using HTDBasic.Content.Items.Range;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HTDBasic.Content.Items.Range
{
    internal class AK47 : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 30;

            Item.useTime = 1;
            Item.useAnimation = 60;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = false;

            Item.UseSound = new SoundStyle($"{nameof(HTDBasic)}/Content/Sounds/AK47")
            {
                Volume = 0.9f,
                PitchVariance = 0.2f,
                MaxInstances = 3,
            };

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 5;
            Item.knockBack = 3f;
            Item.noMelee = true;

            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 12f;

            Item.useAmmo = AmmoID.Bullet;
            Item.reuseDelay = 90;


        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0f, 0f);
        }


        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(20));
            if (type == ProjectileID.Bullet || type == ProjectileID.SilverBullet || type == ProjectileID.ChlorophyteBullet || type == ProjectileID.CursedBullet)
            { // or ProjectileID.WoodenArrowFriendly
                type = ProjectileID.ExplosiveBullet; // or ProjectileID.FireArrow;  
            }
            //ojectile.scale = 20;
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            player.velocity -= velocity / 100;


        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ExplosiveBunny, 20);
            recipe.AddIngredient(ItemID.Wood, 100);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(ItemID.ClockworkAssaultRifle, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

    }
}
  