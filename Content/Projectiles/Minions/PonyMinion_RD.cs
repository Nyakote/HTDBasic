using HTDBasic.Content.Buffs;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
namespace HTDBasic.Content.Projectiles.Minions
{
    public class PonyMinion_RD : ModProjectile
    {
        public override void SetStaticDefaults()
        {

            Main.projPet[Projectile.type] = true; // Makes the projectile a pet
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; // Targeting support
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.minion = true; // Mark it as a minion
            Projectile.DamageType = DamageClass.Summon; // Use summon damage
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1; // Infinite penetration
            Projectile.minionSlots = 1f; // Occupies 1 minion slot
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<PonyBuff>());
                return;
            }

            if (owner.HasBuff(ModContent.BuffType<PonyBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            // Basic idle behavior (hover near player)
            Vector2 targetPosition = owner.Center + new Vector2(0, -50);
            Vector2 direction = targetPosition - Projectile.Center;
            float speed = 10f;
            Projectile.velocity = direction * (speed / direction.Length());
        }
    }
}