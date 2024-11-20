using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIExitButton : UISelectableButton
    {
        [SerializeField] private Text questionText;
        [SerializeField] private string[] questionString = new string[]
        {
            "Are you sure?",
            "Really?"
        };
        private int yesCount = 0;
        private void Start()
        {
            questionText.text = questionString[yesCount];
        }
        public void No()
        {
            yesCount = 0;
            questionText.text = questionString[yesCount];
            SetUnfocus();
        }
        public void Yes()
        {
            if (yesCount == questionString.Length - 1) ExitGame();
            else NextText();
        }
        private void NextText()
        {
            yesCount++;
            questionText.text = questionString[yesCount];
        }
        private void ExitGame()
        {
            Application.Quit();
        }
    }
}