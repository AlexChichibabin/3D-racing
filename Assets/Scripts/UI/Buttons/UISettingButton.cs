using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UISettingButton : UISelectableButton
    {
        [SerializeField] private Setting setting;
        [SerializeField] private Text titleText;
        [SerializeField] private Text valueText;
        [SerializeField] private Image previousImage;
        [SerializeField] private Image nextImage;

        public void SetNextValueSetting() => setting?.SetNextValue();
        public void SetPreviousValueSetting() => setting?.SetPreviousValue();

        public void ApplyProperty(Setting setting)
        {
            if (setting == null) return;

            this.setting = setting;

            titleText.text = setting.Title;
            valueText.text = setting.GetStringValue();

            previousImage.enabled = !setting.isMinValue;
            nextImage.enabled = !setting.isMaxValue;
        }
    }
}