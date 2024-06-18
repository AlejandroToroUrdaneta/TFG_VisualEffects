using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Elements
{
    public class ElementMenu : MonoBehaviour
    {
        [SerializeField] private StarterAssetsInputs input;
        [SerializeField] private GameObject[] elementsActions;
        [SerializeField] GameObject parent;
        private GameObject[] _elementsInstances;
        private ElementManager _elementManager;
        
        

        private bool _hided = true;

        private void Awake()
        {
            //_parent = transform.GetComponent<GameObject>();
            _elementsInstances = new GameObject[elementsActions.Length];
            _elementManager = GetComponent<ElementManager>();
        }

        private void Update()
        {
            if (input.block && _hided)
            {
                ShowElementsMenu();
                _hided = false;
            }
            else if(!input.block && !_hided)
            {
                HideElementsMenu();
                _hided = true;
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void ShowElementsMenu()
        {
            int numActions = elementsActions.Length;
            float angle = 360 / numActions * Mathf.Deg2Rad;
            float startAngle = 90 * Mathf.Deg2Rad;
            float distance = 250f;

            for ( int i = 0; i < numActions; i++)
            {
                float posX = distance * Mathf.Cos(i * angle + startAngle);
                float posY = distance * Mathf.Sin(i * angle + startAngle);
                _elementsInstances[i] = Instantiate(elementsActions[i], new Vector3(960f, 540f, 0f), parent.transform.rotation, parent.transform);
                LeanTween.move(_elementsInstances[i], new Vector3(960f + posX, 540f + posY), 0.2f + 0.05f * i);
                LeanTween.scale(_elementsInstances[i], new Vector3(10, 10, 1), 0.1f + 0.05f * i);
            }

            AssignedFunctions();
        }

        private void AssignedFunctions()
        {
            int numActions = _elementsInstances.Length;

            for (int i = 0; i < _elementsInstances.Length; i++)
            {
                if (i == 0) _elementsInstances[i].GetComponent<Button>().onClick.AddListener(() => _elementManager.ChangeToNature());
                else if( i == 1) _elementsInstances[i].GetComponent<Button>().onClick.AddListener(() => _elementManager.ChangeToFire());
                else _elementsInstances[i].GetComponent<Button>().onClick.AddListener(() => _elementManager.ChangeToEnergy());
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void HideElementsMenu()
        {
            int numActions = _elementsInstances.Length;

            for (int i = 0; i < numActions; i++)
            {
                LeanTween.move(_elementsInstances[i], new Vector3(960f, 540f), 0.2f + 0.05f * i);
                LeanTween.scale(_elementsInstances[i], new Vector3(0, 0, 1), 0.1f + 0.05f * i);
                Destroy(_elementsInstances[i], 0.2f + 0.05f * i);
            }
        }
    }
    
}

