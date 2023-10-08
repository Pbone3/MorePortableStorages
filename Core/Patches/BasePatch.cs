﻿using Terraria.ModLoader;

namespace MorePortableStorages.Core.Patches;

internal abstract class BasePatch : ILoadable {
    internal abstract void Patch(Mod mod);
    internal virtual void Unpatch() { }

    void ILoadable.Load(Mod mod) {
        Patch(mod);
    }

    void ILoadable.Unload() {
        Unpatch();
    }
}