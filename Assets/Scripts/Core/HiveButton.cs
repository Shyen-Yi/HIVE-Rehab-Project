using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace com.hive.projectr
{
    public class HiveButton : Button
    {
        public static readonly float PressedSize = .95f;
        public static readonly Vector3 DefaultScale = Vector3.zero;

        private Vector3 _initialScale = DefaultScale;
        private bool _isPointerOnTop;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerOnTop = true;

            base.OnPointerEnter(eventData);

            if (image != null)
            {
                image.sprite = spriteState.highlightedSprite;
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            _isPointerOnTop = false;

            base.OnPointerExit(eventData);

            if (image != null)
            {
                image.sprite = spriteState.disabledSprite;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            _initialScale = transform.localScale;
            transform.localScale = _initialScale * PressedSize;
            
            if (image != null)
            {
                image.sprite = spriteState.pressedSprite;
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            transform.localScale = _initialScale;

            if (image != null)
            {
                if (_isPointerOnTop)
                {
                    image.sprite = spriteState.highlightedSprite;
                }
                else
                {
                    image.sprite = spriteState.disabledSprite;
                }
            }
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/UI/Hive Button")]
        static void AddHiveButton()
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas == null)
            {
                Logger.LogError($"No Canvas can be found!");
                return;
            }

            var buttonObj = new GameObject("HiveButton");
            var parent = Selection.activeGameObject != null ? Selection.activeGameObject.transform : canvas.transform;
            Undo.RegisterCreatedObjectUndo(buttonObj, "Create HiveButton"); // Register the undo action
            Undo.SetTransformParent(buttonObj.transform, parent, "Parent HiveButton"); // Register the undo action for parenting

            var img = Undo.AddComponent<Image>(buttonObj); // Register the undo action for adding a component
            var button = Undo.AddComponent<HiveButton>(buttonObj); // Register the undo action for adding a component
            button.transform.localPosition = Vector3.zero;
            button.transform.localRotation = Quaternion.identity;
            button.transform.localScale = Vector3.one;

            Selection.activeGameObject = buttonObj;
        }
#endif
    }
}