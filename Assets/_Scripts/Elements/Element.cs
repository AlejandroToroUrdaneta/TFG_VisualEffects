using UnityEngine;
using UnityEngine.VFX;
using StarterAssets;

namespace Elements
{
    /// <summary>
    /// Abstract base class representing an elemental ability in the game.
    /// Defines common properties and behaviors for elemental abilities,
    /// including anticipation, casting, and explosion visual effects.
    /// </summary>
    public abstract class Element : MonoBehaviour
    {
        [SerializeField]
        protected GameObject anticipationPrefab;

        [SerializeField]
        protected GameObject abilityPrefab;

        [SerializeField]
        protected GameObject explosionPrefab;

        [SerializeField]
        protected Transform instantiateTransform;

        protected ThirdPersonController _tpc;
        protected VisualEffect _vfx;
        protected Animator _animator;

        protected int CastAbilityTrigger;

        /// <summary>
        /// Initiates the casting sequence for the elemental ability.
        /// This method Triggers the animation and sets up visual effects according to the player's level.
        /// </summary>
        public virtual void CastAbility()
        {
            _animator.SetTrigger(CastAbilityTrigger);
            SetAbilitySettingsByLevel();
        }

        /// <summary>
        /// Abstract method that defines the specific behavior
        /// of the ability's shoot action.
        /// </summary>
        public abstract void Shoot();

        /// <summary>
        /// Abstract method that configure the visual effects
        ///  of the ability based on the player's level.
        /// </summary>
        protected abstract void SetAbilitySettingsByLevel();
    }
}