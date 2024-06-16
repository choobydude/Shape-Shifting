using DG.Tweening;
using UnityEngine;
using Zenject;

namespace ShapeShifting
{
    public class Star : MonoBehaviour
    {
        SignalBus m_SignalBus;
        bool m_WasUsed = false;

        public void Setup(SignalBus i_SignalBus)
        {
            m_SignalBus = i_SignalBus;
        }

        private void OnEnable()
        {
            transform.DORotate(Vector3.forward * 360, 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1,LoopType.Restart);
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_WasUsed)
                return;

            m_WasUsed = true;
            DOTween.Kill(transform);
            transform.DOScale(0, 0.5f).SetEase(Ease.InBack);
            Invoke(nameof(delayWin), 1);
        }

        private void delayWin()
        {
            m_SignalBus.TryFire<WinGameCommandSignal>();
        }
    }
}

