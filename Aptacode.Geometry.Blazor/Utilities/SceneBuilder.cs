using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Components.Views;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public class SceneBuilder
    {
        private float _width;
        private float _height;
        private List<Primitive> _primitives = new List<Primitive>();
        private ViewModelFactory _viewModelFactory = new ViewModelFactory();
        
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
                Size = new Vector2(_width, _height),
                Components =  _primitives.Select(p => _viewModelFactory.ToViewModel(p)).ToList()
            };
        }
        
        public void Reset()
        {
            _width = 0.0f;
            _height = 0.0f;
            _primitives.Clear();;
        }
    }
}
