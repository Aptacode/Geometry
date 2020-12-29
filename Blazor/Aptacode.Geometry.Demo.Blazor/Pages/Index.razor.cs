using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;
using Microsoft.AspNetCore.Components;
using Rectangle = Aptacode.Geometry.Primitives.Polygons.Rectangle;

namespace Aptacode.Geometry.Demo.Blazor.Pages
{
    public class ConnectedComponentViewModel : PolygonViewModel
    {
        #region Ctor
        public ConnectedComponentViewModel(Rectangle body) : base(body)
        {
            Body = body;

            ConnectionPoints = new List<ConnectionPointViewModel>();
        }
        #endregion

        #region Prop

        public Rectangle Body { get; private set; }
        public List<ConnectionPointViewModel> ConnectionPoints { get; private set; }

        #endregion

        public ConnectionPointViewModel AddConnectionPoint()
        {
            var ellipse = new Ellipse(Body.TopLeft, new Vector2(10, 10), 0);
            var connectionPoint = new ConnectionPointViewModel(this, ellipse);
            ConnectionPoints.Add(connectionPoint);

            Children.Add(connectionPoint);
            return connectionPoint;
        }
    }

    public class ConnectionPointViewModel : EllipseViewModel
    {
        #region Ctor

        public ConnectionPointViewModel(ConnectedComponentViewModel component, Ellipse ellipse) : base(ellipse)
        {
            this.component = component;
            Ellipse = ellipse;
            Connections = new List<ConnectionViewModel>();
        }
        #endregion

        #region Prop

        public ConnectedComponentViewModel component { get; set; }
        public List<ConnectionViewModel> Connections { get; set; }
        public Ellipse Ellipse { get; set; }

        #endregion

        public override void Translate(Vector2 delta)
        {
            base.Translate(delta);
            foreach (var connectionViewModel in Connections)
            {
                connectionViewModel.Calculate();
            }
        }
    }

    public class ConnectionViewModel : PolylineViewModel
    {
        #region Ctor
        protected ConnectionViewModel(ConnectionPointViewModel connectionPoint1, ConnectionPointViewModel connectionPoint2) : base(new PolyLine(VertexArray.Create(new[]
        {
            connectionPoint1.Ellipse.BoundingCircle.Center,
            connectionPoint2.Ellipse.BoundingCircle.Center
        })))
        {
            ConnectionPoint1 = connectionPoint1;
            ConnectionPoint2 = connectionPoint2;
            CollisionDetectionEnabled = false;
        }
        #endregion

        #region Prop

        public ConnectionPointViewModel ConnectionPoint1 { get; set; }
        public ConnectionPointViewModel ConnectionPoint2 { get; set; }

        public PolyLine Connection { get; set; }

        #endregion

        public static ConnectionViewModel Connect(ConnectionPointViewModel connectionPoint1, ConnectionPointViewModel connectionPoint2)
        {
            var connection = new ConnectionViewModel(connectionPoint1, connectionPoint2);
            connectionPoint1.Connections.Add(connection);
            connectionPoint2.Connections.Add(connection);
            return connection;
        }

        public void Calculate()
        {
            PolyLine.Vertices[0] = ConnectionPoint1.Ellipse.BoundingCircle.Center;
            PolyLine.Vertices[1] = ConnectionPoint2.Ellipse.BoundingCircle.Center;
            PolyLine.ResetCircle();
            PolyLine.ResetRectangle();
            UpdateBoundingRectangle();
            Invalidated = true;
        }
    }

    public class IndexBase : ComponentBase
    {
        public DemoSceneController SceneController { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var sceneBuilder = new SceneBuilder();
            var componentBuilder = new ComponentBuilder();

            sceneBuilder.SetWidth(1000).SetHeight(1000);

            //Polygon
            var polygon = componentBuilder
                .SetBase(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20).ToViewModel()).SetFillColor(Color.Green)
                .SetBorderThickness(1).Build();

            polygon.Scale(new Vector2(4.0f));
            polygon.Rotate((float) Math.PI);
            polygon.Translate(new Vector2(200, 0));
            sceneBuilder.AddComponent(polygon);

            //Polyline
            var polyLine = componentBuilder.SetBase(PolyLine.Create(80, 40, 70, 80, 60, 20).ToViewModel())
                .SetFillColor(Color.Blue).SetBorderThickness(1).Build();

            polyLine.Scale(new Vector2(2.0f));
            polyLine.Rotate(0.5f);
            sceneBuilder.AddComponent(polyLine);

            //Rectangles
            sceneBuilder.AddComponent(
                componentBuilder.SetBase(Rectangle.Create(400, 100, 75, 75).ToViewModel())
                    .SetText("Blue Rectangle")
                    .SetFillColor(Color.Blue).SetBorderThickness(1).Build());

            sceneBuilder.AddComponent(componentBuilder.SetBase(Rectangle.Create(100, 250, 100, 100).ToViewModel())
                .SetFillColor(Color.Red).SetBorderThickness(1).Build());

            //Ellipse


            var child1 = componentBuilder.SetBase(Ellipse.Create(80, 80, 15, 15, 0.0f).ToViewModel())
                .SetFillColor(Color.Orange).SetBorderThickness(1).Build();

            var child2 = componentBuilder.SetBase(Ellipse.Create(300, 180, 15, 10, (float) Math.PI).ToViewModel())
                .SetFillColor(Color.Green).SetBorderThickness(1).Build();

            var group = componentBuilder.AddChild(child1).AddChild(child2).Build();

            sceneBuilder.AddComponent(group);
            
            //PrimitiveGroup
            sceneBuilder.AddComponent(componentBuilder
                .AddChild(Ellipse.Create(400, 200, 15, 15, 0.0f).ToViewModel())
                .AddChild(Rectangle.Create(400, 200, 30, 10).ToViewModel())
                .SetFillColor(Color.Green)
                .SetText("Group")
                .SetBorderThickness(1).Build());

            var component1 = new ConnectedComponentViewModel(Rectangle.Create(20, 20, 100, 50));
            var component2 = new ConnectedComponentViewModel(Rectangle.Create(300, 260, 100, 50));

            var connectionPoint1 = component1.AddConnectionPoint();
            var connectionPoint2 = component2.AddConnectionPoint();

            var connection = ConnectionViewModel.Connect(connectionPoint1, connectionPoint2);

            sceneBuilder.AddComponent(component1);
            sceneBuilder.AddComponent(component2);
            sceneBuilder.AddComponent(connection);

            var scene = sceneBuilder.Build();

            SceneController = new DemoSceneController(scene);

            await base.OnInitializedAsync();
        }
        
    }
}