using System;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.CSharp.Common.Utilities.Mvvm;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class Scene : BindableBase
    {
        #region Ctor

        public Scene(Vector2 size)
        {
            Size = size;
        }

        #endregion

        #region Properties

        private readonly List<ComponentViewModel> _components = new List<ComponentViewModel>();

        public Vector2 Size { get; init; }

        public bool ShowGrid { get; set; } = true;

        #endregion

        #region Components

        public void Add(ComponentViewModel component)
        {
            _components.Add(component);
            OnComponentAdded?.Invoke(this,component);
        }
        
        public void AddRange(IEnumerable<ComponentViewModel> components)
        {
            foreach (var component in components)
            {
                Add(component);
            }
        }

        public bool Remove(ComponentViewModel component)
        {
            var success = _components.Remove(component);
            OnComponentRemoved?.Invoke(this, component);
            return success;
        }

        public IEnumerable<ComponentViewModel> Components => _components;

        #endregion

        #region Events

        public event EventHandler<ComponentViewModel> OnComponentAdded;
        public event EventHandler<ComponentViewModel> OnComponentRemoved;

        #endregion

        #region Layering

        public void BringToFront(ComponentViewModel componentViewModel)
        {
            if (!_components.Remove(componentViewModel))
            {
                return;
            }

            _components.Add(componentViewModel);
        }

        public void SendToBack(ComponentViewModel componentViewModel)
        {
            if (!_components.Remove(componentViewModel))
            {
                return;
            }

            _components.Insert(0, componentViewModel);
        }

        public void BringForward(ComponentViewModel componentViewModel)
        {
            var index = _components.IndexOf(componentViewModel);
            if (index == _components.Count - 1)
            {
                return;
            }

            _components.RemoveAt(index);
            _components.Insert(index + 1, componentViewModel);
        }

        public void SendBackward(ComponentViewModel componentViewModel)
        {
            var index = _components.IndexOf(componentViewModel);
            if (index == 0)
            {
                return;
            }

            _components.RemoveAt(index);
            _components.Insert(index - 1, componentViewModel);
        }

        #endregion
    }
}