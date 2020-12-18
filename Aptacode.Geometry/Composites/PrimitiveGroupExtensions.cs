using System.Linq;
using System.Numerics;
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

        public static PrimitiveGroup Translate(this PrimitiveGroup group, Primitive child, Vector2 delta)
        {
            var children = group.Children.ToList();
            var childIndex = children.IndexOf(child);
            if (childIndex == -1)
            {
                return group;
            }
            
            children.RemoveAt(childIndex);
            children.Insert(childIndex, child.Translate(delta));
            return new PrimitiveGroup(children);
        }

        public static PrimitiveGroup Rotate(this PrimitiveGroup group, Primitive child, Vector2 rotationCenter, float theta)
        {
            var children = group.Children.ToList();
            var childIndex = children.IndexOf(child);
            if (childIndex == -1)
            {
                return group;
            }

            children.RemoveAt(childIndex);
            children.Insert(childIndex, child.Rotate(rotationCenter, theta));
            return new PrimitiveGroup(children);
        }

        public static PrimitiveGroup Scale(this PrimitiveGroup group, Primitive child, Vector2 delta)
        {
            var children = group.Children.ToList();
            var childIndex = children.IndexOf(child);
            if (childIndex == -1)
            {
                return group;
            }

            children.RemoveAt(childIndex);
            children.Insert(childIndex, child.Scale(delta));
            return new PrimitiveGroup(children);
        }
        
        public static PrimitiveGroup Skew(this PrimitiveGroup group, Primitive child, Vector2 delta)
        {
            var children = group.Children.ToList();
            var childIndex = children.IndexOf(child);
            if (childIndex == -1)
            {
                return group;
            }

            children.RemoveAt(childIndex);
            children.Insert(childIndex, child.Skew(delta)));
            return new PrimitiveGroup(children);
        }
    }
}