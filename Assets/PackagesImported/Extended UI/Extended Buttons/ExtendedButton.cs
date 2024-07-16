using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExtendedUI
{
    /// <summary>
    /// Reveals in the inspector the OnEnter and OnExit events, on top of the OnClick event. This allows for extra custom behaviour.
    /// Uses an auxiliary scriptable object for coloring.
    /// </summary>
    
    [ExecuteInEditMode]
    public class ExtendedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        //Button properties
        public bool interactable = true;
        public bool tweenOnHover = true;
        private ButtonState _state;
        private int _tweenId;
        private RectTransform _rect;
        
        [Header("Background")]
        public Image backgroundImage;
        public UIColors backgroundColors;
        
        [Header("Text")]
        public TextMeshProUGUI textTMP;
        public UIColors textColors;
        
        [Header("Events")]
        public UnityEvent onEnter;
        public UnityEvent onExit;
        public UnityEvent onClick;
        
        private void Reset()
        {
            //Fetch components
            textTMP = GetComponentInChildren<TextMeshProUGUI>();
            backgroundImage = GetComponentInChildren<Image>();
        }

        protected virtual void OnEnable()
        {
            _rect = GetComponent<RectTransform>();
            _state = interactable ? ButtonState.Idle : ButtonState.Disabled;
            
            OnButtonExit();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (interactable)
            {
                OnButtonEnter();
            }
        }
        
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (interactable)
            {
                OnButtonExit();
            }
        }
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (interactable)
            {
                OnButtonClick();
            }
        }

        public void OnButtonEnter()
        {
            _state = ButtonState.Hovered;
            onEnter?.Invoke();

            if (tweenOnHover)
            {
                var targetScale = new Vector3(1.02f, 1.02f, 1);
                var tweenDuration = 0.2f;
                    
                _tweenId = LeanTween.scale(_rect, targetScale, tweenDuration)
                    .setEase(LeanTweenType.easeOutBack)
                    .id;
            }
            
            UpdateButtonAppearance();
        }

        public void OnButtonExit()
        {
            _state = ButtonState.Idle;
            onExit?.Invoke();
            
            if (tweenOnHover)
            {
                _rect.localScale = new Vector3(1, 1, 1);
                LeanTween.cancel(_tweenId);
            }
            
            UpdateButtonAppearance();
        }

        public void OnButtonClick()
        {
            _state = ButtonState.Clicked;
            onClick?.Invoke();
            
            UpdateButtonAppearance();
        }

        private void UpdateButtonAppearance()
        {
            UpdateButtonText();
            UpdateButtonImage();
        }

        private void UpdateButtonImage()
        {
            if (backgroundImage == null)
            {
                return;
            }
            if (backgroundColors == null)
            {
                return;
            }
            backgroundImage.color = _state switch
            {
                ButtonState.Idle => backgroundColors.Idle,
                ButtonState.Hovered => backgroundColors.Hovered,
                ButtonState.Clicked => backgroundColors.Clicked,
                ButtonState.Disabled => backgroundColors.Disabled,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void UpdateButtonText()
        {
            if (textTMP == null)
            {
                return;
            }
            
            textTMP.color = _state switch
            {
                ButtonState.Idle => textColors.Idle,
                ButtonState.Hovered => textColors.Hovered,
                ButtonState.Clicked => textColors.Clicked,
                ButtonState.Disabled => textColors.Disabled,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                _state = interactable ? ButtonState.Idle : ButtonState.Clicked;
                UpdateButtonAppearance();
            }
        }
#endif
    }

    public enum ButtonState
    {
        Idle,
        Hovered,
        Clicked,
        Disabled
    }
}





