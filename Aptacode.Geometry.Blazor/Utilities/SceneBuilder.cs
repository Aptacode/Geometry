using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public class SceneBuilder
    {
        private readonly List<ComponentViewModel> _components = new();
        private float _height;
        private float _width;

        public SceneBuilder SetWidth(float width)
        {
            _width = width;
            return this;
        }

        public SceneBuilder SetHeight(float height)
        {
            _height = height;
            return this;
        }

        public SceneBuilder AddComponent(ComponentViewModel component)
        {
            _components.Add(component);
            return this;
        }

        public Scene Build()
        {
            var scene = new Scene(new Vector2(_width, _height));

            foreach (var componentViewModel in _components)
            {
                scene.Add(componentViewModel);
            }

            Reset();
            return scene;
        }

        public void Reset()
        {
            _width = 0.0f;
            _height = 0.0f;
            _components.Clear();
        }
    }
}