using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    public class RadioButtonGroupController : MonoBehaviour
    {
        public delegate void SelectedRadioButtonChanged(string radioButtonKey);
        public SelectedRadioButtonChanged selectedRadioButtonChangedHandler;

        [SerializeField]
        RadioButton defaultSelectedButton;

        private RadioButton currentSelectedButton;

        private void Awake()
        {
            TrySelectedButton(defaultSelectedButton);
        }

        public void TrySelectedButton(RadioButton selectedButton)
        {
            if (selectedButton == currentSelectedButton)
                return;

            if (currentSelectedButton != null)
                currentSelectedButton.SetSelected(false);

            if (selectedButton != null)
                selectedButton.SetSelected(true);

            currentSelectedButton = selectedButton;

            if (selectedRadioButtonChangedHandler != null)
                selectedRadioButtonChangedHandler(selectedButton.RadioButtonKey);
        }
    }
}
