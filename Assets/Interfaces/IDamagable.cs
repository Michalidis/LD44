namespace Assets.Interfaces
{
    interface IDamagable
    {
        void TakeDamage(int amount, bool apply_on_hit_effects = true);
    }
}
