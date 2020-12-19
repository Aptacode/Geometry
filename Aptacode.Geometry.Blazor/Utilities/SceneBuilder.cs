using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public class SceneBuilder
    {
        private readonly List<Primitive> _primitives = new();
        private readonly ViewModelFactory _viewModelFactory = new();
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

        public SceneBuilder AddPrimitive(Primitive primitive)
        {
            _primitives.Add(primitive);
            return this;
        }

        public SceneViewModel Build()
        {
            return new()
            {
                Size = new Vector2(_width, _height).ToScale(),
                Components = _primitives.Select(p => _viewModelFactory.ToViewModel(p)).ToList()
            };
        }

        public void Reset()
        {
            _width = 0.0f;
            _height = 0.0f;
            _primitives.Clear();
            ;
        }
    }
}