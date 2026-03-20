// PulseStrike | InsufficientCreditsException | Phase 6
using System;

namespace PulseStrike.Economy
{
    public sealed class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException() : base("Not enough credits for this transaction.")
        {
        }
    }
}
