using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public class RemoveResourceTimerAction : TimerActionBase
    {
        [SerializeField] private eResourceType m_ResourceType;
        [SerializeField] private int m_Amount;
        [Inject]
        ResourceController m_ResourceController;

        protected override void OnTimerOff()
        {
            m_ResourceController.SpendResource(m_ResourceType, m_Amount);
        }
    }
}

