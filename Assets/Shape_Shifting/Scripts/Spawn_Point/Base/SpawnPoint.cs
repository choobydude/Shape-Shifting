using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public class SpawnPoint : MonoBehaviour
    {
        #region Fields
        public bool IsOccupied { get; private set; }

        protected SpawnObject Occupier;
        #endregion

        #region Interaction Methods
        public virtual void Occupy(SpawnObject i_Occupier)
        {
            Occupier = i_Occupier;
        }
        public virtual void Release()
        {
            Occupier = null;
        }
        #endregion

        #region Lifecycle Methods
        private void onSpawned(Vector3 i_Position, Quaternion i_Rotation)
        {
            transform.position = i_Position;
            transform.rotation = i_Rotation;
        }
        private void onDespawned()
        {
            factoryReset();
        }

        private void factoryReset()
        {
            Release();
        }

        #endregion

        #region Pooling

        public class Pool : MonoMemoryPool<Vector3, Quaternion, SpawnPoint>
        {
            protected override void Reinitialize(Vector3 i_Position, Quaternion i_Rotation, SpawnPoint i_Object)
            {
                base.Reinitialize(i_Position, i_Rotation, i_Object);
                i_Object.onSpawned(i_Position, i_Rotation);
            }
            protected override void OnDespawned(SpawnPoint i_Object)
            {
                base.OnDespawned(i_Object);
                i_Object.onDespawned();
            }
        }

        #endregion
    }
}

