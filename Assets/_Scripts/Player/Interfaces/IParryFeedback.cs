namespace EraSoren.Player.Interfaces
{
    public interface IParryFeedback
    {
        void ParryStartFeedback();
        void ParryLockEndedFeedback();
        void ParryEndFeedback();
        void ParryCooldownFeedback();
    }
}