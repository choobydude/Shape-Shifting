using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace ShapeShifting
{
    public class BlobGroup : MonoBehaviour
    {
        [SerializeField] Rigidbody2D m_Rigidbody;
        [ShowInInspector, ReadOnly] private List<Blob> m_Blobs = new List<Blob>();
        [SerializeField] private Blob m_DefaultBlobPrefab;

        private void OnEnable()
        {
            spawnDefaultBlob();
            DisablePhysics();
        }

        private void spawnDefaultBlob()
        {
            Blob blob = Instantiate(m_DefaultBlobPrefab, transform);
            blob.transform.localPosition = Vector3.zero;
            blob.transform.localRotation = Quaternion.identity;

            AddBlob(blob);
        }

        public void AddBlob(Blob i_Blob)
        {
            m_Blobs.Add(i_Blob);
            calculateCenterOfMass();
        }
        public void RemoveBlob(Blob i_Blob)
        {
            m_Blobs.Remove(i_Blob);
            calculateCenterOfMass();
        }

        public void EnablePhysics()
        {
            m_Rigidbody.isKinematic = false;
            calculateCenterOfMass();
        }
        public void DisablePhysics()
        {
            m_Rigidbody.isKinematic = true;
        }

        private void calculateCenterOfMass()
        {

        }

        private void OnDrawGizmos()
        {
            if (m_Rigidbody)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(m_Rigidbody.worldCenterOfMass, 0.3f);
            }
        }
    }
}

