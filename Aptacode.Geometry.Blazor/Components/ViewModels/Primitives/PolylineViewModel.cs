using System.Linq;
using System.Text;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public sealed class PolylineViewModel : ComponentViewModel
    {
        public PolylineViewModel(PolyLine polyLine) : base(polyLine)
        {
            Primitive = polyLine;
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

        private new PolyLine _primitive;

        public new PolyLine Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }

        public string Path { get; set; }

        #endregion
    }
}