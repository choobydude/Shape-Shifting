using System.Collections;
using UnityEngine;

namespace WhackAMole
{
    public interface IKillable 
    {
        public delegate void DeathAction();
        public abstract event DeathAction OnDeath;
    }
}