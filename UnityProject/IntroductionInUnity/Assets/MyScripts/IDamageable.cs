

namespace EscapeRoom {

    public interface IDamageable {

        #region Methods

        void TakeDamage(float damage);

        void HealDamage(float heal);

        #endregion
    }
}
