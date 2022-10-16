namespace EraSoren.Enemies.Interfaces
{
    public interface IEnemyAttack
    {
        void Attack();

        delegate void AttackHandler();

        event AttackHandler OnAttackEnd;
    }
}