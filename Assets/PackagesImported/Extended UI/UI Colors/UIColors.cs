using UnityEngine;
using UnityEngine.Serialization;

namespace ExtendedUI
{
    [CreateAssetMenu (menuName = "UI Colors", fileName = "New UI Colors")]
    public class UIColors : ScriptableObject
    {
        [SerializeField] private Color idle;
        [FormerlySerializedAs("highlighted")] [SerializeField] private Color hovered;
        [SerializeField] private Color clicked, disabled;

        public Color Idle => idle;
        public Color Hovered => hovered;
        public Color Clicked => clicked;
        public Color Disabled => disabled;
    }   
}
