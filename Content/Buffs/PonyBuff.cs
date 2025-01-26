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
            if (player.ownedProjectileCounts[ModContent.ProjectileType<PonyMinion>()] > 0) 
            {
                player.buffTime[buffIndex] = 10000;
                return;
            }

            player.DelBuff(buffIndex);
            buffIndex--;
        }

    }
}
