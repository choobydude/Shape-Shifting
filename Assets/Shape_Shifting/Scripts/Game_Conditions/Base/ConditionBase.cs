using Zenject;
using UnityEngine;

namespace WhackAMole
{
    [System.Serializable]
    public abstract class ConditionBase : ScriptableObject
    {
        [Inject]
        protected SignalBus SignalBus;
        [SerializeField]
        protected string ErrorMessage;


        public delegate void ConditionMeetAction();
        public event ConditionMeetAction OnConditionMet;

        public abstract bool CheckCondition(out string o_ErrorMessage);
        protected void ConditionsMet()
        {
            OnConditionMet?.Invoke();
        }
        public abstract void Initialize();
        public abstract void Release();
    }
}

