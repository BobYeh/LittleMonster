using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    public class RadioButton : Button
    {
        [SerializeField]
        public string RadioButtonKey;

        /// <summary>
        /// radion button is selected
        /// </summary>
        protected bool selected;

        public bool Selected
        {
            get
            {
                return selected;
            }
        }

        public void SetSelected(bool selected)
        {
            if (this.selected == selected)
            {
                return;
            }

            this.selected = selected;

            if (selected)
                OnSelected();
            else
                OnUnSelected();
        }

        protected virtual void OnSelected()
        {
            //Handle ui transition
        }

        protected virtual void OnUnSelected()
        {
            //Handle ui transition
        }
    }
}
