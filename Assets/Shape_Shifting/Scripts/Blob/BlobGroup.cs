using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
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
            i_Blob.transform.SetParent(transform);
            m_Blobs.Add(i_Blob);
            calculateCenterOfMass();
        }
        public void RemoveBlob(Blob i_Blob)
        {
            m_Blobs.Remove(i_Blob);
            Destroy(i_Blob.gameObject);
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

        public void EraseBlobsInRadius(Vector2 i_Position, float i_Radius)
        {
            for (int i = 0; i < m_Blobs.Count; i++)
            {
                if (m_Blobs[i].CanBeErased)
                {
                    if (getEffectiveDistance(i_Position, m_Blobs[i].transform.position, i_Radius, m_Blobs[i].transform.localScale.x / 2f) <= 0)
                    {
                        RemoveBlob(m_Blobs[i]);
                        i--;
                    }
                }
            }
        }

        public Blob GetClosestBlob(Vector2 _Source, float i_Radius, out float i_DistanceBetween)
        {
            Blob blob = m_Blobs.OrderBy(blob => getEffectiveDistance(_Source, blob.transform.position, i_Radius, blob.transform.localScale.x / 2f)).FirstOrDefault();
            i_DistanceBetween = getEffectiveDistance(_Source, blob.transform.position, i_Radius, blob.transform.localScale.x / 2);
            return blob;
        }

        float getEffectiveDistance(Vector2 pos1, Vector2 pos2, float rad1, float rad2)
        {
            float centerDistance = Vector3.Distance(pos1, pos2);
            float edgeDistance = centerDistance - (rad1 + rad2);
            return edgeDistance < 0 ? 0 : edgeDistance;
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

