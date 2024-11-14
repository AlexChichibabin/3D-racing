using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Racing
{
    public class UISelectableButton : UIButton
    {
        [SerializeField] private Image[] selectedImage;

        public UnityEvent OnSelect;
        public UnityEvent OnUnselect;

        private void Start()
        {
            for(int i = 0; i < selectedImage.Length; i++)
            {
                selectedImage[i].enabled = false;
            }
        }
        public override void SetFocus()
        {
            base.SetFocus();

            for (int i = 0; i < selectedImage.Length; i++)
            {
                selectedImage[i].enabled = true;
            }
            OnSelect?.Invoke();
        }
        public override void SetUnfocus()
        {
            base.SetUnfocus();

            for (int i = 0; i < selectedImage.Length; i++)
            {
                selectedImage[i].enabled = false;
            }
            OnUnselect?.Invoke();
        }
    }
}