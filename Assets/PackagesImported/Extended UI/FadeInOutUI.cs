using System;
using UnityEngine;

namespace ExtendedUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeInOutUI : MonoBehaviour
    { 
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDurationInSeconds;
        [SerializeField] private bool fadeOnStart;
        
        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Start()
        {
            if (fadeOnStart)
            {
                FadeIn();   
            }
        }

        public void FadeIn()
        {
            Fade(FadeMode.FadeIn);
        }

        public void FadeOut()
        {
            Fade(FadeMode.FadeOut);
        }

        private void Fade(FadeMode fadeMode)
        {
            float startValue, endValue;

            if (fadeMode == FadeMode.FadeIn)
            {
                startValue = 0;
                endValue = 1;
            }
            else
            {
                startValue = 1;
                endValue = 0;
            }

            LeanTween.value(gameObject, startValue, endValue, fadeDurationInSeconds)
                .setOnUpdate(value => canvasGroup.alpha = value);
        }
    }

    public enum FadeMode
    {
        FadeIn,
        FadeOut
    }
}
