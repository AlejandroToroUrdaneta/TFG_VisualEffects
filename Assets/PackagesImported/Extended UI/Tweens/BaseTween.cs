using UnityEngine;
using UnityEngine.Events;

// +----------------------------+
// | ---- GoragarX GameDev ---- |
// | goragarxgamedev@gmail.com  |
// +----------------------------+

namespace GoragarXGameDev
{
    /// <summary>
    /// Inherit from this class to create new tweens. Uses LeanTween library
    /// </summary>
    public abstract class BaseTween : MonoBehaviour
    {
        [SerializeField] protected LeanTweenType easeType;
        [SerializeField] protected float durationInSeconds;
        [SerializeField] protected float delayInSeconds;
        [SerializeField] protected bool ignoresTimeScale;
        [SerializeField] protected bool tweenOnEnable;
        [SerializeField] protected bool tweenOnHover;
        [Tooltip("-1 for infinite repeats")]
        [SerializeField] protected int repeats;
        [SerializeField] protected UnityEvent tweenCompleted;

        public bool isTweening;
        
        public abstract void StartTween();
        
        public abstract void CancelTween();

        protected void OnTweenCompleted()
        {
            isTweening = false;
            tweenCompleted?.Invoke();
        }
    }
    
    public enum ScaleMode
    {
        Percentage,
        ExactValue
    }
    
    public enum DisplacementMode
    {
        Relative,
        ExactCoordinates
    }
}
