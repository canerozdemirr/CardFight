using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Interfaces.UI;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.UI
{
    [Serializable]
    public class UIManagementSystem : MonoBehaviour, IUIManagementSystem, IInitializable, IDisposable
    {
        [SerializeField] private List<Canvas> _canvases = new();

        private Dictionary<Type, Canvas> _canvasByType;
        
        private HashSet<IUIElement> _allUIElements;
        
        public void Initialize()
        {
            _allUIElements = new HashSet<IUIElement>(_canvases.Capacity);
            InitializeCanvasDictionaries();
            InitializeCanvases();
        }

        public void Dispose()
        {
            foreach (IUIElement uiElement in _allUIElements)
            {
                uiElement.Cleanup();
            }
        }

        private void InitializeCanvasDictionaries()
        {
            _canvasByType = new Dictionary<Type, Canvas>();

            foreach (Canvas canvas in _canvases)
            {
                if (canvas == null)
                    continue;

                Type canvasType = canvas.GetType();
                _canvasByType.TryAdd(canvasType, canvas);
            }
        }

        private void InitializeCanvases()
        {
            foreach (Canvas canvas in _canvases)
            {
                if (canvas == null)
                    continue;

                IUIElement uiElement = canvas.GetComponent<IUIElement>();
                if (uiElement != null)
                {
                    uiElement.Initialize();
                    _allUIElements.Add(uiElement);
                }
                else
                {
                    Debug.LogWarning($"Canvas {canvas.name} does not implement IUIElement interface.");
                }
            }
        }

        public void OpenUI<T>() where T : MonoBehaviour, IUIElement
        {
            Canvas canvas = GetCanvasByType<T>();
            if (canvas != null)
            {
                T uiElement = canvas.GetComponent<T>();
                if (uiElement != null)
                {
                    uiElement.Show();
                }
                else
                {
                    canvas.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogWarning($"Canvas of type {typeof(T).Name} not found in UIManager.");
            }
        }

        public void CloseUI<T>() where T : MonoBehaviour, IUIElement
        {
            Canvas canvas = GetCanvasByType<T>();
            if (canvas != null)
            {
                T uiElement = canvas.GetComponent<T>();
                if (uiElement != null)
                {
                    uiElement.Hide();
                }
                else
                {
                    canvas.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogWarning($"Canvas of type {typeof(T).Name} not found in UIManager.");
            }
        }

        public bool IsUIOpen<T>() where T : MonoBehaviour, IUIElement
        {
            Canvas canvas = GetCanvasByType<T>();
            if (canvas == null) 
                return false;
            
            T uiElement = canvas.GetComponent<T>();
            return uiElement != null ? uiElement.IsVisible : canvas.gameObject.activeSelf;
        }

        public T GetUI<T>() where T : MonoBehaviour, IUIElement
        {
            Canvas canvas = GetCanvasByType<T>();
            return canvas != null ? canvas.GetComponent<T>() : null;
        }

        private Canvas GetCanvasByType<T>() where T : MonoBehaviour, IUIElement
        {
            Type targetType = typeof(T);

            return _canvasByType.TryGetValue(targetType, out Canvas canvas)
                ? canvas
                : _canvasByType.Values.FirstOrDefault(canvasEntry => canvasEntry.GetComponent<T>() != null);
        }
    }
}