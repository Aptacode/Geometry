using System.Text;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public sealed class PolylineViewModel : ComponentViewModel
    {

        public PolylineViewModel(PolyLine polyLine) : base(polyLine)
        {
            Primitive = polyLine;
            OnRedraw();
        }

        #region Properties

        private new PolyLine _primitive;
        public new PolyLine Primitive
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
