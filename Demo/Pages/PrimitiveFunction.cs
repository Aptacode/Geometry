using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Demo.Pages;

public class PrimitiveFunction<T> : ProfileFunction where T : Primitive
{
    private readonly Func<Random, T> _createPrimitive;
    private readonly string _operationName;
    private readonly string _primitiveName;
    private readonly Action<T> _primitiveOperation;
    private T _primitive;
    private Random _random;

    public PrimitiveFunction(Action<T> primitiveOperation, string operationName)
    {
        var type = typeof(T);
        _primitiveName = type.Name;
        _operationName = operationName;

        if (type == typeof(Ellipse))
        {
            _createPrimitive = r => ProfileHelpers.CreateEllipse(r) as T;
        }
        else if (type == typeof(Point))
        {
            _createPrimitive = r => ProfileHelpers.CreatePoint(r) as T;
        }
        else if (type == typeof(Polygon))
        {
            _createPrimitive = r => ProfileHelpers.CreatePolygon(r) as T;
        }
        else if (type == typeof(PolyLine))
        {
            _createPrimitive = r => ProfileHelpers.CreatePolyline(r) as T;
        }

        if (_createPrimitive == null)
        {
            throw new ArgumentNullException();
        }

        _primitiveOperation = primitiveOperation;
    }

    public override string Title()
    {
        return $"{_primitiveName} {_operationName}";
    }

    public override void Setup()
    {
        _random = new Random(0);
    }

    public override void Reset()
    {
        _primitive = _createPrimitive(_random);
    }

    public override void Run()
    {
        _primitiveOperation(_primitive);
    }
}