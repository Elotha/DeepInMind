namespace EraSoren.Player.Interfaces
{
    public interface IParry
    {
        bool IsParry { get; set; }
        void ApplyParry();
        void TakeDamage(float damageAngle);
    }
}