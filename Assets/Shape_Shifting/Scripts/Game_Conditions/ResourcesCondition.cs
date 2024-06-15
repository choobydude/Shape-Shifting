using UnityEngine;
using Zenject;

namespace WhackAMole
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Resource Condition", menuName = "Conditions/Resource Condition")]
    public class ResourcesCondition : ConditionBase
    {
        [Inject]
        ResourceController m_ResourceController;
        [SerializeField] private eResourceType m_ResourceType;
        [SerializeField] private ComparisonOperation m_CompareOperation;

        public override void Initialize()
        {
            SignalBus.Subscribe<ResourceChangedSignal>(onResourceChanged);
        }
        public override void Release()
        {
            SignalBus.TryUnsubscribe<ResourceChangedSignal>(onResourceChanged);
        }

        private void onResourceChanged(ResourceChangedSignal i_Signal)
        {
            if (i_Signal.ResourceType == m_ResourceType)
            {
                if (CheckCondition(out string o_ErrorMessage))
                    ConditionsMet();
            }
        }

        public override bool CheckCondition(out string o_ErrorMessage)
        {
            if (m_ResourceController.GetResourceValue(m_ResourceType, out int o_Value))
            {
                bool result = m_CompareOperation.Compare(o_Value);
                o_ErrorMessage = result ? "success" : ErrorMessage;
                return result;
            }
            o_ErrorMessage = "value not fount";
            return false;
        }

    }
}

