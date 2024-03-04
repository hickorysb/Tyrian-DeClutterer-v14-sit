using Aki.Reflection.Patching;
using EFT;
using System.Reflection;

namespace Framesaver
{
    // DontSpawnShellsFiringPatch removes the spawning of spent shell casings
    // when firing a gun. Very cool, but it has an expensive update cycle
    // in GameWorld to clean them up.
    class DontSpawnShellsFiringPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(WeaponEffectsManager).GetMethod("method_9", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        [PatchPrefix]
        public static bool Prefix()
        {
            return false;
        }
    }

    // DontSpawnShellsJamPatch does similar to DontSpawnShellsFiringPatch, but
    // removes the processing for clearing a jam.
    class DontSpawnShellsJamPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(WeaponEffectsManager).GetMethod("SpawnShellAfterJam", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        [PatchPrefix]
        public static bool Prefix()
        {
            return false;
        }
    }

    class DontSpawnShellsAtAllReallyPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(WeaponEffectsManager).GetMethod("method_4", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        [PatchPrefix]
        public static bool Prefix(ref bool __result)
        {
            __result = false;
            return false;
        }
    }
}