using UnityEngine;
using UnityEngine.EventSystems;

// +----------------------------+
// | ---- GoragarX GameDev ---- |
// | goragarxgamedev@gmail.com  |
// +----------------------------+

namespace GoragarXGameDev
{
    /// <summary>
    /// Tweens the scale of a UI element
    /// </summary>
    public class ScaleTweenUI : BaseTween, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ScaleMode scaleMode;
        [SerializeField] private Vector3 initialScale = new (1,1,1);
        [SerializeField] private Vector3 to;
        [Tooltip("Used only for percentage scaling")][SerializeField] private float scaleFactor;
        
        private int _id;
        private RectTransform _rect;

        private void OnEnable()
        {
            _rect = GetComponent<RectTransform>();
            _rect.localScale = initialScale;
            
            if (scaleMode == ScaleMode.Percentage)
            {
                to = initialScale * (scaleFactor / 100 + 1);
            }

            if (tweenOnEnable)
            {
                StartTween();
            }
        }
        
        private void OnDisable()
        {
            CancelTween();
        }
        
        public override void StartTween()
        {
            if (!isTweening)
            {
                _id = LeanTween.scale(_rect, to, durationInSeconds)
                    .setOnStart(() =>
                    {
                        isTweening = true;
                    })
                    .setDelay(delayInSeconds)
                    .setOnComplete(OnTweenCompleted)
                    .setEase(easeType)
                    .setIgnoreTimeScale(ignoresTimeScale)
                    .setRepeat(repeats)
                    .id;
            }
        }

        public override void CancelTween()
        {
            _rect.localScale = initialScale;
            isTweening = false;
            LeanTween.cancel(_id);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!tweenOnHover)
            {
                return;
            }
            StartTween();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!tweenOnHover)
            {
                return;
            }
            CancelTween();
        }
    }
}
