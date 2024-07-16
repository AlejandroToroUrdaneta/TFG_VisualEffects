using System;
using GoragarXGameDev.Utils;
using UnityEngine;

namespace ExtendedUI
{
    public class ModalWindowManager : Singleton<ModalWindowManager>
    {
        [SerializeField] private ModalWindow modalWindow;
        [SerializeField] private GameObject panel; //Raycast blocker

        /// <summary>
        /// Turns on the Modal Window, updating it's content.
        /// </summary>
        /// <param name="content">A ModalWindowContentSO data asset that provides all the information</param>
        /// <param name="confirm">Confirm callback</param>
        /// <param name="cancel">Cancel callback</param>
        /// <param name="alternative">Alternative callback</param>
        public void Show(ModalWindowContentSO content, Action confirm, Action cancel, Action alternative)
        {
            if (modalWindow == null)
            {
                Debug.LogError("Error, No Modal Window could be found");
                return;
            }
            
            modalWindow.SetWindowContent(content);
            modalWindow.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
        }

        /// <summary>
        /// Turns off the Modal Window.
        /// </summary>
        public void Close()
        {
            if (modalWindow == null)
            {
                Debug.LogError("Error, No Modal Window could be found");
                return;
            }
            
            modalWindow.gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
        }
    }
}
