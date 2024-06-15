using UnityEngine;

namespace WhackAMole
{
    public class Mole : SpawnObject, IDamageable, IKillable
    {

        #region Fields
        [SerializeField] private int m_Health;
        float m_CurrentHealth;

        public event IKillable.DeathAction OnDeath;

        #endregion

        #region Lifecycle

        protected override void OnSpawned(Vector3 i_Position, Quaternion i_Rotation, SpawnPoint i_SpawnPoint)
        {
            base.OnSpawned(i_Position, i_Rotation, i_SpawnPoint);
            m_CurrentHealth = m_Health;
        }

        #endregion

        #region Health Methods

        public void TakeDamage(int i_DamageAmount)
        {
            if (m_CurrentHealth <= 0)
                return;

            m_CurrentHealth -= i_DamageAmount;
            if (m_CurrentHealth <= 0)
                die();
        }
        private void die()
        {
            OnDeath?.Invoke();
            Despawn();
        }

        #endregion
    }
}

