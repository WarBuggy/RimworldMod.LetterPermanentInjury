using Verse;

namespace LetterPermanentInjury
{
    public readonly struct LPIData
    {
        public Pawn Pawn { get; }

        public Hediff_Injury Injury { get; }

        public LPIData(ref Pawn pawn, ref Hediff_Injury injury)
        {
            Pawn = pawn;
            Injury = injury;
        }
    }
}
