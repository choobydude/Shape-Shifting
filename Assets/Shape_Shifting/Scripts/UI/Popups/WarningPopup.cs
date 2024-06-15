using DG.Tweening;
using TMPro;
using UnityEngine;

namespace WhackAMole
{
    public class WarningPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_Text;

        Sequence m_Sequence;

        private void OnDisable()
        {
            killTweens();
        }
        private void OnDestroy()
        {
            killTweens();
        }

        public void Pop(string i_WarningText)
        {
            m_Text.text = i_WarningText;

            killTweens();

            m_Text.alpha = 0;
            transform.localScale = Vector3.zero;

            m_Sequence = DOTween.Sequence();
            m_Sequence.Insert(0, m_Text.DOFade(1, 0.2f));
            m_Sequence.Insert(0, transform.DOScale(1, 0.4f).SetEase(Ease.OutElastic));
            m_Sequence.Append(m_Text.DOFade(0, 0.5f));
        }


        private void killTweens()
        {
            DOTween.Kill(transform);
            DOTween.Kill(m_Text);
            DOTween.Kill(m_Sequence);
        }
    }
}

