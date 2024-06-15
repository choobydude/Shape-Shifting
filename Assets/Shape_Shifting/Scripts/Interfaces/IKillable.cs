using System.Collections;
using UnityEngine;

namespace ShapeShifting
{
    public interface IKillable 
    {
        public delegate void DeathAction();
        public abstract event DeathAction OnDeath;
    }
}