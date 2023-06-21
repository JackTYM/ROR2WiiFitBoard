using BepInEx;
using BepInEx.Logging;
using R2API;
using R2API.Utils;

namespace WiiFitBoard
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI))]
    [BepInPlugin(ModGuid, ModName, ModVer)]
    public class WiiFitBoard : BaseUnityPlugin
    {
        private const string ModVer = "1.1.0";
        private const string ModName = "WiiFitBoard";
        public const string ModGuid = "me.jacktym.WiiFitBoard";

        internal new static ManualLogSource Logger; // allow access to the logger across the plugin classes

        public void Awake()
        {
            Logger = base.Logger;

            Assets.Init();
        }
    }
}