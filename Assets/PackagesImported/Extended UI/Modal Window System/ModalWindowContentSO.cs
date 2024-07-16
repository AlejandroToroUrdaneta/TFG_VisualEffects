using UnityEngine;

namespace ExtendedUI
{
    [CreateAssetMenu (menuName = "Modal Window Content", fileName = "New Modal Window Content")]
    public class ModalWindowContentSO : ScriptableObject
    {
        [Header("Header")]
        [SerializeField] private string headerString;
        
        [Header("Content")]
        [SerializeField] private ModalWindowLayout layout;
        [SerializeField] [TextArea (8, 10)] private string contentString;
        [SerializeField] private Sprite image;

        [Header("Buttons")]
        [SerializeField] private bool hasConfirmButton;
        [SerializeField] private string confirmString;
        [Space(10)]
        [SerializeField] private bool hasCancelButton;
        [SerializeField] private string cancelString;
        [Space(10)]
        [SerializeField] private bool hasAlternativeButton;
        [SerializeField] private string alternativeString;

        public string HeaderString => headerString;
        public string ContentString => contentString;
        public ModalWindowLayout Layout => layout;
        public Sprite ContentImage => image;
        public bool HasConfirmButton => hasConfirmButton;
        public bool HasCancelButton => hasCancelButton;
        public bool HasAlternativeButton => hasAlternativeButton;

        public string ConfirmString => confirmString;
        public string CancelString => cancelString;
        public string AlternativeString => alternativeString;

        public enum ModalWindowLayout
        {
            Vertical,
            Horizontal
        }
    }
}