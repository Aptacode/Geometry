using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class PolygonViewModel : ComponentViewModel
    {

        public PolygonViewModel(Polygon polygon) : base(polygon)
        {
            Primitive = polygon;
            OnRedraw();
        }

        #region Properties

        protected new Polygon _primitive;
        public new Polygon Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }
        public string Path { get; set; }

        #endregion

        #region ComponentViewModel

        protected override void OnRedraw()
        {
            var stringBuilder = new StringBuilder();
            foreach (var vertex in Primitive.Vertices)
            {
                stringBuilder.Append($"{vertex.X},{vertex.Y} ");
            }

            Path = stringBuilder.ToString();
            base.OnRedraw();
        }

        #endregion
    }
}
