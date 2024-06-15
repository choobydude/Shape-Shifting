using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public class ResourceGainTimer : Timer
    {
        [SerializeField] TextMeshProUGUI m_Text;
        [SerializeField] eResourceType m_ResourceType;
        [SerializeField] int m_Amount;

        [Inject]
        ResourceController m_ResourceController;

        private void Awake()
        {
            StartTimer();
        }

        protected override void Tick(float i_RemainingTime)
        {
            base.Tick(i_RemainingTime);
            m_Text.text = $"gain {m_Amount} {m_ResourceType} in : <br>{TimeSpan.FromSeconds(i_RemainingTime).ToString("hh':'mm':'ss")}";
        }
        protected override void TimesOff()
        {
            base.TimesOff();
            m_ResourceController.AddResource(m_ResourceType, m_Amount);
        }
    }
}

