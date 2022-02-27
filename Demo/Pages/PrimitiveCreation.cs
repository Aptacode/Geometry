using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Demo.Pages;

public class PrimitiveCreation<T> : ProfileFunction where T : Primitive
{
    private readonly Func<Random, T> _createPrimitive;
    private readonly string _primitiveName;
    private Random _random;

    public PrimitiveCreation()
    {
        var type = typeof(T);
        _primitiveName = type.Name;

        if (type == typeof(Ellipse))
            _createPrimitive = r => ProfileHelpers.CreateEllipse(r) as T;
        else if (type == typeof(Point))
            _createPrimitive = r => ProfileHelpers.CreatePoint(r) as T;
        else if (type == typeof(Polygon))
            _createPrimitive = r => ProfileHelpers.CreatePolygon(r) as T;
        else if (type == typeof(PolyLine)) _createPrimitive = r => ProfileHelpers.CreatePolyline(r) as T;

        if (_createPrimitive == null) throw new ArgumentNullException();
    }

    public override string Title()
    {
        return $"{_primitiveName} creation";
    }

    public override void Setup()
    {
        _random = new Random(0);
    }

    public override void Run()
    {
        _createPrimitive(_random);
    }
}