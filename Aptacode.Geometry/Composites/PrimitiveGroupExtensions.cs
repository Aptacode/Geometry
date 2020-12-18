using System.Linq;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Composites
{
    public static class PrimitiveGroupExtensions
    {
        public static PrimitiveGroup Add(this PrimitiveGroup group, Primitive child)
        {
            var children = group.Children.ToList();
            children.Add(child);
            return new PrimitiveGroup(children);
        }

        public static PrimitiveGroup Remove(this PrimitiveGroup group, Primitive child)
        {
            var children = group.Children.ToList();
            children.Remove(child);
            return new PrimitiveGroup(children);
        }
    }
}