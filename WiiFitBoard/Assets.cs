using System.Reflection;
using R2API;
using RoR2;
using UnityEngine;
using System.Collections.ObjectModel;
using static R2API.RecalculateStatsAPI;

namespace WiiFitBoard
{
    internal static class Assets
    {
        internal static GameObject WiiFitBoardPrefab;
        internal static Sprite WiiFitBoardIcon;

        internal static ItemDef WiiFitBoardItemDef;
        internal static EquipmentDef WiiFitBoardEquipmentDef;

        private const string ModPrefix = "@WiiFitBoard:";

        internal static BuffDef wiiFitBuff {get;private set;}

        internal static void Init()
        {
            // First registering your AssetBundle into the ResourcesAPI with a modPrefix that'll also be used for your prefab and icon paths
            // note that the string parameter of this GetManifestResourceStream call will change depending on
            // your namespace and file name
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WiiFitBoard.wii_fit_item")) 
            {
                var bundle = AssetBundle.LoadFromStream(stream);

                WiiFitBoardPrefab = bundle.LoadAsset<GameObject>("Assets/Import/wii_fit_board/wii_fit_board.prefab");
                WiiFitBoardIcon = bundle.LoadAsset<Sprite>("Assets/Import/wii_fit_board_icon/wii_fit_board_icon.png");
            }

            WiiFitBoardAsGreenTierItem();

            AddLanguageTokens();

            RecalculateStatsAPI.GetStatCoefficients += OnGetStatCoefficients;
        }

        private static void OnGetStatCoefficients(CharacterBody body, RecalculateStatsAPI.StatHookEventArgs args)
        {
            args.moveSpeedMultAdd += 2 * body.inventory.GetItemCount(WiiFitBoardItemDef);
        }

        private static void WiiFitBoardAsGreenTierItem()
        {
            WiiFitBoardItemDef = new ItemDef
            {
                name = "WiiFitBoard", // its the internal name, no spaces, apostrophes and stuff like that
                tier = ItemTier.Tier3,
                pickupModelPrefab = WiiFitBoardPrefab,
                pickupIconSprite = WiiFitBoardIcon,
                nameToken = "WIIFITBOARD_NAME", // stylised name
                pickupToken = "WIIFITBOARD_PICKUP",
                descriptionToken = "WIIFITBOARD_DESC",
                loreToken = "WIIFITBOARD_LORE",
                tags = new[]
                {
                    ItemTag.Utility
                }
            };
            var itemDisplayRules = new ItemDisplayRule[0];

            var wiiFitBoard = new R2API.CustomItem(WiiFitBoardItemDef, itemDisplayRules);

            ItemAPI.Add(wiiFitBoard); // ItemAPI sends back the ItemIndex of your item
        }

        private static void AddLanguageTokens()
        {
            //The Name should be self explanatory
            LanguageAPI.Add("WIIFITBOARD_NAME", "Wii Fit Board");
            //The Pickup is the short text that appears when you first pick this up. This text should be short and to the point, nuimbers are generally ommited.
            LanguageAPI.Add("WIIFITBOARD_PICKUP", "Increases movement speed.");
            //The Description is where you put the actual numbers and give an advanced description.
            //CHANGE ME!!!
            LanguageAPI.Add("WIIFITBOARD_DESC",
                "Increases your movement speed by <style=cIsUtility>40%</style> <style=cStack>(+40% per stack)</style>.");
            //The Lore is, well, flavor. You can write pretty much whatever you want here.
            LanguageAPI.Add("WIIFITBOARD_LORE",
                "You're a fucking fatass but you did some WII FIT!!!! NOW YOU ARE SKINNY!!");
        }
    }
}
