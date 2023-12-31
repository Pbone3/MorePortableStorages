﻿using MorePortableStorages.Core.PortableStorages;
using Terraria.ID;
using Terraria;
using MorePortableStorages.Core;
using Terraria.ModLoader;

namespace MorePortableStorages.Content.Safe;

public class FlyingSafe : BasePortableStorageProjectile<SafePortableStorage> {
    public static readonly ProjectileHoverMovement Movement = new ProjectileMovement();
    
    public override int CursorIcon => ModContent.ItemType<GrotesqueStatuette>();
    
    public override void SetStaticDefaults() {
        Main.projFrames[Type] = 5;
        PortableStorageLoader.RegisterInteractiveOpenCloseSound(Type, SoundID.Tink, SoundID.Tink);
    }

    public override void SetDefaults() {
        Projectile.width = 42;
        Projectile.height = 48;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 10800;
    }

    public const float HOVER_SPEED = 0.008f;
    public const float HOVER_TIME = 40f;

    public override void AI() {
        if (++Projectile.frameCounter % (Movement.HoveringUp(Projectile) > 50f ? 3 : 5) == 0) {
            Projectile.frame++;
            Projectile.frameCounter = 0;

            if (Projectile.frame >= Main.projFrames[Type]) {
                Projectile.frame = 0;
            }
        }
        
        Movement.Move(Projectile);

        base.AI();
    }
    
    public class ProjectileMovement : ProjectileHoverMovement {
        public ProjectileMovement() : base(HOVER_SPEED, HOVER_TIME) { }

        public override ref float FinishedInitialSlowdown(Projectile projectile) {
            return ref projectile.ai[0];
        }

        public override ref float HoverTimer(Projectile projectile) {
            return ref projectile.ai[1];
        }

        public override ref float HoveringUp(Projectile projectile) {
            return ref projectile.ai[2];
        }
    }
}