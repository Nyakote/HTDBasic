using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HTDBasic.Content.Projectiles
{
    public class ApplePie : ModProjectile
    {
    
        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.light = 0.5f;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = 0;
        }

        public override void AI()
        {
         
            Projectile.velocity.Y += 0.4f;

            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.YellowStarDust);
        }
    }
}
