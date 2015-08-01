using UnityEngine;

namespace Candlelight.UI.HyperTextDemo
{
	public class Popup : MonoBehaviour
	{
		[SerializeField]
		private Animator m_Animator;
		[SerializeField]
		private HyperText m_TextBox;

		public Animator Animator { get { return m_Animator; } }

		public bool IsRevealed { get { return m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Revealed"); } }

		public RectTransform RectTransform { get { return transform as RectTransform; } }

		public void Dismiss()
		{
			m_Animator.SetTrigger("Dismiss");
			m_Animator.ResetTrigger("Reveal");
		}

		public void Reveal()
		{
			m_Animator.SetTrigger("Reveal");
			m_Animator.ResetTrigger("Dismiss");
		}

		public void SetText(string text)
		{
			if (text != m_TextBox.text)
			{
				m_TextBox.text = text;
			}
		}
	}
}