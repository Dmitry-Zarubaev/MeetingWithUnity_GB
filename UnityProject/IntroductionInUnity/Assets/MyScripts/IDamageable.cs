

namespace EscapeRoom {

    public interface IDamageable {

        #region Methods

        void TakeDamage(float damage, Damagedealers damageType);

        void HealDamage(float heal);

        #endregion
    }
}
