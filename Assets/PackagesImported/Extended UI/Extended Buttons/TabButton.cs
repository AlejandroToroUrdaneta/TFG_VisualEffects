using UnityEngine.EventSystems;

// +----------------------------+
// | ---- GoragarX GameDev ---- |
// | goragarxgamedev@gmail.com  |
// +----------------------------+

namespace ExtendedUI
{
    public class TabButton : ExtendedButton
    {
        public TabGroup tabGroup;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            tabGroup.Subscribe(this);
        }
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.OnTabEnter(this);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.OnTabExit();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabSelected(this);
        }
    }
}