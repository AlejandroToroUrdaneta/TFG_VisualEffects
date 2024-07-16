using ExtendedUI;
using UnityEngine;
using UnityEngine.Events;

public class ModalWindowTrigger : MonoBehaviour
{
    [SerializeField] private ModalWindowContentSO content;
    [SerializeField] private UnityEvent onConfirm;
    [SerializeField] private UnityEvent onCancel;
    [SerializeField] private UnityEvent onAlternative;
    
    /// <summary>
    /// Calls the Show method of the window manager.
    /// Events will be passed as onClick callbacks for the buttons.
    /// </summary>
    public void Show()
    {
        ModalWindowManager.Instance.Show(content, onConfirm.Invoke, onCancel.Invoke, onAlternative.Invoke);
    }
}
