using UnityEngine;
using Verse;

namespace LetterPermanentInjury
{
    public class LPIMod : Mod
    {
        private readonly LPISetting LPISettings;

        public LPIMod(ModContentPack content)
            : base(content)
        {
            LPISettings = base.GetSettings<LPISetting>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("LPI_Option".Translate());
            listingStandard.CheckboxLabeled("LPI_Option_Colonist".Translate(), ref LPISettings.alertColonist);
            listingStandard.CheckboxLabeled("LPI_Option_Prisoner".Translate(), ref LPISettings.alertPrisoner);
            listingStandard.CheckboxLabeled("LPI_Option_Pet".Translate(), ref LPISettings.alertPet);
            listingStandard.CheckboxLabeled("LPI_Option_Debug".Translate(), ref LPISettings.debug);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "LPI_Option_Mod_Name".Translate();
        }
    }
}