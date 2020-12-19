using System.Linq;
using System.Numerics;
using System.Text;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public sealed class PolylineViewModel : ComponentViewModel
    {
        public PolylineViewModel(PolyLine polyLine) : base(polyLine)
        {
            Primitive = polyLine;
        }


        #region ComponentViewModel

        protected override void Redraw()
        {
            var stringBuilder = new StringBuilder();
            foreach (var vertex in Primitive.Vertices.Select(v => v.ToScale()))
            {
                stringBuilder.Append($"{vertex.X},{vertex.Y} ");
            }

            Path = stringBuilder.ToString();
            base.Redraw();
        }

        #endregion

        #region Properties

        public new PolyLine Primitive
        {
            get => (PolyLine) _primitive;
            set
            {
                SetProperty(ref _primitive, value);
                Redraw();
            }
        }

        public string Path { get; set; }

        #endregion

        #region Transformation

        public override void Translate(Vector2 delta)
        {
            Primitive = Primitive.Translate(delta);
        }

        public override void Rotate(float theta)
        {
            Primitive = Primitive.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Primitive = Primitive.Rotate(rotationCenter, theta);
        }

        public override void Scale(Vector2 delta)
        {
            Primitive = Primitive.Scale(delta);
        }

        public override void Skew(Vector2 delta)
        {
            Primitive = Primitive.Skew(delta);
        }

        #endregion
    }
}