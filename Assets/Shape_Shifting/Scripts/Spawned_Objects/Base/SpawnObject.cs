using System.Linq;
using UnityEngine;
using Zenject;

namespace WhackAMole
{
    public class SpawnObject : MonoBehaviour
    {
        protected SpawnPoint SpawnPoint;
        [Inject]
        SignalBus m_SignalBus;
        [Inject]
        Pool m_Pool;

        protected virtual void OnSpawned(Vector3 i_Position, Quaternion i_Rotation, SpawnPoint i_SpawnPoint)
        {
            transform.position = i_Position;
            transform.rotation = i_Rotation;
            subscribeSignals();
            SpawnPoint = i_SpawnPoint;
            SpawnPoint.Occupy(this);
        }
        protected virtual void OnDespawned()
        {
            unsubscribeSignals();
            FactoryReset();
        }
        protected virtual void FactoryReset()
        {
            SpawnPoint?.Release();
            SpawnPoint = null;
        }

        private void subscribeSignals()
        {
            m_SignalBus.Subscribe<GameUnloadedSignal>(Despawn);
        }
        private void unsubscribeSignals()
        {
            m_SignalBus.TryUnsubscribe<GameUnloadedSignal>(Despawn);
        }

        public virtual void Despawn()
        {
            if (!m_Pool.InactiveItems.Contains(this))
            {
                m_Pool.Despawn(this);
            }
        }

        #region Pooling

        public class Pool : MonoMemoryPool<Vector3, Quaternion, SpawnPoint, SpawnObject>
        {
            protected override void Reinitialize(Vector3 i_Position, Quaternion i_Rotation, SpawnPoint i_SpawnPoint, SpawnObject i_Object)
            {
                base.Reinitialize(i_Position, i_Rotation, i_SpawnPoint, i_Object);
                i_Object.OnSpawned(i_Position, i_Rotation, i_SpawnPoint);
            }
            protected override void OnDespawned(SpawnObject i_Object)
            {
                base.OnDespawned(i_Object);
                i_Object.OnDespawned();
            }
        }
        #endregion
    }
}

