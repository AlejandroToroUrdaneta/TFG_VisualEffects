using System;
using System.Collections.Generic;
using UnityEngine;

// +----------------------------+
// | ---- GoragarX GameDev ---- |
// | goragarxgamedev@gmail.com  |
// +----------------------------+

namespace ExtendedUI
{
    
    public class TabGroup : MonoBehaviour
    {
        private enum NavigationMode
        {
            Horizontal,
            Vertical
        }
        
        public List<TabButton> tabs;
        [SerializeField] private bool preselectFirst;
        [SerializeField] private NavigationMode navigationMode;
        
        private TabButton _selectedTabButton;
        private int _index;

        private void Start()
        {
            if (preselectFirst)
            {
                OnTabSelected(tabs[0]);
            }
        }
        
        private void Update()
        {
            NavigateGroup();
        }

       private void NavigateGroup()
        {
            //Horizontal navigation
            if (navigationMode == NavigationMode.Horizontal)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q))
                {
                    _index = (_index - 1 + tabs.Count) % tabs.Count;
                    OnTabSelected(tabs[_index]);
                }
            
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.E))
                {
                    _index = (_index + 1) % tabs.Count;
                    OnTabSelected(tabs[_index]);
                }
            }

            //Vertical navigation
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    _index = (_index - 1 + tabs.Count) % tabs.Count;
                    OnTabSelected(tabs[_index]);
                }
            
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    _index = (_index + 1) % tabs.Count;
                    OnTabSelected(tabs[_index]);
                }
            }
        }

        public void Subscribe(TabButton tabButton)
        {
            tabs ??= new List<TabButton>();
            
            if (!tabs.Contains(tabButton))
            {
                tabs.Add(tabButton);
            }
        }

        public void OnTabEnter(TabButton tabButton)
        {
            ResetTabs();

            if (tabButton == null || tabButton != _selectedTabButton)
            {
                tabButton.OnButtonEnter();
            }
        }

        public void OnTabExit()
        {
            ResetTabs();
        }

        public void OnTabSelected(TabButton tabButton)
        {
            _selectedTabButton = tabButton;
            _index = tabs.IndexOf(_selectedTabButton);
                
            ResetTabs();

            tabButton.OnButtonClick();
        }

        private void ResetTabs()
        {
            foreach (var card in tabs)
            {
                if (_selectedTabButton == null || _selectedTabButton != card)
                {
                    if (card.isActiveAndEnabled)
                    {
                        card.OnButtonExit();
                    }
                }
            }
        }
        
        public void DeselectCurrentTab()
        {
            _selectedTabButton = null;
            ResetTabs();
        } 
    }
}
