using System;
using UnityEngine;
using UnityEngine.UI;

// +----------------------------+
// | ---- GoragarX GameDev ---- |
// | goragarxgamedev@gmail.com  |
// +----------------------------+

namespace ExtendedUI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image targetImage;
        [SerializeField] private Color emptyColor;
        [SerializeField] private Color filledColor;

        public event Action ProgressCompleted;

        private void Reset()
        {
            var images = GetComponentsInChildren<Image>();
            targetImage = images[1];
            emptyColor = Color.white;
            filledColor = Color.white;
        }

        /// <summary>
        /// Sets the progress bar's fill image fillAmount to a given value
        /// </summary>
        /// <param name="progress">Fill percentage, normalized (value between 0 and 1)</param>
        /// <param name="barFills">if set to false, bar empties instead</param>
        public void UpdateProgressBar(float progress, bool barFills = true)
        {
            targetImage.fillAmount = progress;
            
            Color color = Color.Lerp(emptyColor, filledColor, progress * 1.2f);
            targetImage.color = color;

            if (progress >= 1)
            {
                OnProgressCompleted();
            } 
        }

        private void OnProgressCompleted()
        {
            ProgressCompleted?.Invoke();
        }
        
        /// <returns>Current progress (between 0 and 1)</returns>
        public float GetProgress()
        {
            return targetImage.fillAmount;
        }
    }
}
