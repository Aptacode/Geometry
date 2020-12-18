using System.Linq;
using System.Text;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class PolygonViewModel : ComponentViewModel
    {
        public PolygonViewModel(Polygon polygon) : base(polygon)
        {
            Primitive = polygon;
            OnRedraw();
        }

        #region ComponentViewModel

        protected override void OnRedraw()
        {
            var stringBuilder = new StringBuilder();
            foreach (var vertex in Primitive.Vertices.Select(v => v * Constants.Scale))
            {
                stringBuilder.Append($"{vertex.X},{vertex.Y} ");
            }

            Path = stringBuilder.ToString();
            base.OnRedraw();
        }

        #endregion

        #region Properties

        protected new Polygon _primitive;

        public new Polygon Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }

        public string Path { get; set; }

        #endregion
    }
}