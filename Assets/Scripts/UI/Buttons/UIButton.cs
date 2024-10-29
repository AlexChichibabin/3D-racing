using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Racing
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] protected bool Interactable;

        private bool focused;
        public bool Focused => focused;

        public UnityEvent OnClick;

        public event UnityAction<UIButton> PointerEnter;
        public event UnityAction<UIButton> PointerExit;
        public event UnityAction<UIButton> PointerClick;

        public virtual void SetFocus()
        {
            if (Interactable == false) return;
            focused = true;
        }
        public virtual void SetUnfocus()
        {
            if (Interactable == false) return;
            focused = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerExit?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerClick?.Invoke(this);
            OnClick?.Invoke();
        }
    }
}