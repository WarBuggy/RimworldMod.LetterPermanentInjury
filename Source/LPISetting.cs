using RimWorld;
using Verse;

namespace LetterPermanentInjury
{
    public class LPISetting : ModSettings
    {
        public bool alertColonist;

        public bool alertPrisoner;

        public bool alertPet;

        public bool debug;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref alertColonist, "alertColonist", true, false);
            Scribe_Values.Look(ref alertPrisoner, "alertPrisoner", true, false);
            Scribe_Values.Look(ref alertPet, "alertPet", true, false);
            Scribe_Values.Look(ref debug, "debug", false, false);
            base.ExposeData();
        }
    }
}
