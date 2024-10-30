using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Racing
{
    public class UISelectableButton : UIButton
    {
        [SerializeField] private Image selectedImage;

        public UnityEvent OnSelect;
        public UnityEvent OnUnselect;

        private void Start()
        {
            selectedImage.enabled = false;
        }
        public override void SetFocus()
        {
            base.SetFocus();

            selectedImage.enabled = true;
            OnSelect?.Invoke();
        }
        public override void SetUnfocus()
        {
            base.SetUnfocus();

            selectedImage.enabled = false;
            OnUnselect?.Invoke();
        }
    }
}