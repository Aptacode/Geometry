﻿using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Composites;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public sealed class PrimitiveGroupViewModel : ComponentViewModel
    {
        public PrimitiveGroupViewModel(PrimitiveGroup primitiveGroup) : base(primitiveGroup)
        {
            Children = new List<ComponentViewModel>();
            Primitive = primitiveGroup;
        }

        #region Canvas

        public override async Task Draw(IContext2DWithoutGetters ctx)
        {
            foreach (var child in Children)
            {
                await ctx.SaveAsync();

                await child.Draw(ctx);

                await ctx.RestoreAsync();
            }

            Invalidated = false;
        }

        #endregion


        #region Properties

        public new PrimitiveGroup Primitive
        {
            get => (PrimitiveGroup) _primitive;
            set
            {
                _primitive = value;
                Invalidated = true;
            }
        }

        public List<ComponentViewModel> Children { get; set; }

        #endregion
    }
}