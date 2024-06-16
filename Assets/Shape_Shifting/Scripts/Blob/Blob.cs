using UnityEngine;

namespace ShapeShifting
{
    public class Blob : MonoBehaviour
    {
        [field:SerializeField] public bool CanBeErased { get; private set; }

        public void Erase()
        {
            if (!CanBeErased)
                return;
        }
    }

}
