﻿using BeatGame.Data;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BeatGame.UI.Components.Tabs
{
    [Serializable]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public UnityEvent OnTabSelected;
        public UnityEvent OnTabDeselected;

        public TransitionInfo[] Transitions;

        [SerializeField]
        private TabGroup tabGroup;

        private void Awake()
        {
            if (OnTabSelected == null)
                OnTabSelected = new UnityEvent();

            if (OnTabDeselected == null)
                OnTabDeselected = new UnityEvent();
        }

        void OnEnable()
        {
            if (tabGroup != null)
                tabGroup.Subscribe(this);
        }

        public void SetTabGroup(TabGroup tabGroup)
        {
            this.tabGroup = tabGroup;
            OnEnable();
        }

        public void SetState(UIPointerEvent pointerEvent)
        {
            foreach (var item in Transitions)
            {
                switch (pointerEvent)
                {
                    case UIPointerEvent.Idle:
                        item.TargetGraphic.color = item.IdleColor;
                        break;
                    case UIPointerEvent.Hover:
                        item.TargetGraphic.color = item.HoverColor;
                        break;
                    case UIPointerEvent.Pressed:
                        break;
                    case UIPointerEvent.Selected:
                        item.TargetGraphic.color = item.SelectedColor;
                        break;
                    default:
                        break;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabSelected(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.OnTabExit(this);
        }

        public void Select()
        {
            OnTabSelected?.Invoke();
        }

        public void Deselect()
        {
            OnTabDeselected?.Invoke();
        }

        private void OnDestroy()
        {
            if (tabGroup != null)
                tabGroup.Unbscribe(this);
        }
    }
}