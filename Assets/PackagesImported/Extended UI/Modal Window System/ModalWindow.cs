using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// +----------------------------+
// | ---- GoragarX GameDev ---- |
// | goragarxgamedev@gmail.com  |
// +----------------------------+

namespace ExtendedUI
{
    public class ModalWindow : MonoBehaviour
    {
        [Header("Header")]
        [SerializeField] private TextMeshProUGUI headerDisplay;

        [Header("Vertical Content")]
        [SerializeField] private GameObject verticalLayout;
        [SerializeField] private Image verticalContentImage;
        [SerializeField] private TextMeshProUGUI verticalContentDisplay;

        [Header("Horizontal Content")]
        [SerializeField] private GameObject horizontalLayout;
        [SerializeField] private Image horizontalContentImage;
        [SerializeField] private TextMeshProUGUI horizontalContentDisplay;
        
        [Header("Vertical Content")]
        [SerializeField] private ExtendedButton confirmButton;
        [SerializeField] private ExtendedButton cancelButton;
        [SerializeField] private ExtendedButton alternativeButton;
        
        // Custom callbacks for more complex behaviours
        private Action onConfirm, onCancel, onAlternative;
        public void SetWindowContent(ModalWindowContentSO content)
        {
            SetHeader(content);
            SetContent(content);
            SetButtons(content);
        }

        private void SetHeader(ModalWindowContentSO content)
        {
            if (string.IsNullOrEmpty(content.HeaderString))
            {
                headerDisplay.gameObject.SetActive(false);
            }
            else
            {
                headerDisplay.gameObject.SetActive(true);
                headerDisplay.text = content.HeaderString;
            }
        }
        
        private void SetContent(ModalWindowContentSO content)
        {
            //Vertical
            if (content.Layout == ModalWindowContentSO.ModalWindowLayout.Vertical)
            {
                //Image
                if (content.ContentImage != null)
                {
                    verticalContentImage.sprite = content.ContentImage;
                    verticalContentImage.gameObject.SetActive(true);
                }
                else
                {
                    verticalContentImage.gameObject.SetActive(false);
                }
                
                horizontalLayout.SetActive(false);
                verticalLayout.SetActive(true);
                verticalContentDisplay.text = content.ContentString;
            }
            
            //Horizontal
            else
            {
                //Image
                if (content.ContentImage != null)
                {
                    horizontalContentImage.sprite = content.ContentImage;
                    horizontalContentImage.gameObject.SetActive(true);
                }
                else
                {
                    horizontalContentImage.gameObject.SetActive(false);
                }
                
                verticalLayout.SetActive(false);
                horizontalLayout.SetActive(true);
                horizontalContentDisplay.text = content.ContentString;
            }
        }

        private void SetButtons(ModalWindowContentSO content)
        {
            confirmButton.gameObject.SetActive(content.HasConfirmButton);
            cancelButton.gameObject.SetActive(content.HasCancelButton);
            alternativeButton.gameObject.SetActive(content.HasAlternativeButton);

            if (!string.IsNullOrEmpty(content.ConfirmString))
            {
                confirmButton.textTMP.text = content.ConfirmString;
            }
            if (!string.IsNullOrEmpty(content.CancelString))
            {
                cancelButton.textTMP.text = content.CancelString;
            }
            if (!string.IsNullOrEmpty(content.AlternativeString))
            {
                alternativeButton.textTMP.text = content.AlternativeString;
            }
        }

        public void Confirm()
        {
            onConfirm?.Invoke();
            CloseWindow();
        }
        
        public void Cancel()
        {
            onCancel?.Invoke();
            CloseWindow();
        }
        
        public void Alternative()
        {
            onAlternative?.Invoke();
            CloseWindow();
        }

        private void CloseWindow()
        {
            ModalWindowManager.Instance.Close();
        }

        public void SetCallbacks(Action confirm, Action cancel, Action alternative)
        {
            onConfirm = confirm;
            onCancel = cancel;
            onAlternative = alternative;
        }
    }
}