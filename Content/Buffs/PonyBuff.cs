using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HTDBasic.Content.Projectiles;
using HTDBasic.Content.Items.Summon;
using HTDBasic.Content.Projectiles.Minions;

namespace HTDBasic.Content.Buffs
{
    public class PonyBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<PonyMinion_RD>()] > 0 ||
                player.ownedProjectileCounts[ModContent.ProjectileType<PonyMinion_RR>()] > 0 ||
                player.ownedProjectileCounts[ModContent.ProjectileType<PonyMinion_PP>()] > 0 ||
                player.ownedProjectileCounts[ModContent.ProjectileType<PonyMinion_F>()] > 0 ||
                player.ownedProjectileCounts[ModContent.ProjectileType<PonyMinion_TW>()] > 0 ||
                player.ownedProjectileCounts[ModContent.ProjectileType<PonyMinion_AJ>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

    }
}
