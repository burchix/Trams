using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tram.Common.Consts;
using Tram.Common.Enums;
using Tram.Common.Helpers;
using Tram.Common.Models;

namespace Tram.Controller.Controllers
{
    public class DirectxController
    {
        private bool isDeviceInit;

        private MainController mainController;
        private List<CustomVertex.PositionColored[]> vertexes;
        private List<CustomVertex.PositionColored[]> edges;

        private Microsoft.DirectX.Direct3D.Font text;
        private Line line;
        private Vector2[] lineVertexes;
        private float minX, maxX, minY, maxY;

        public DirectxController()
        {
            isDeviceInit = false;
            vertexes = new List<CustomVertex.PositionColored[]>();
            edges = new List<CustomVertex.PositionColored[]>();
        }

        #region Public Methods

        public void InitMap()
        {
            if (mainController == null)
            {
                mainController = Kernel.Get<MainController>();
            }

            minX = mainController.Map.Min(n => n.Coordinates.X);
            maxX = mainController.Map.Max(n => n.Coordinates.X);
            minY = mainController.Map.Min(n => n.Coordinates.Y);
            maxY = mainController.Map.Max(n => n.Coordinates.Y);

            foreach (var node in mainController.Map)
            {
                float pX = CalculateXPosition(node.Coordinates.X); 
                float pY = CalculateYPosition(node.Coordinates.Y);
                if (node.Type != NodeType.CarCross)
                {
                    vertexes.Add(
                        DirectxHelper.CreateCircle(
                            pX,
                            pY,
                            node.Type == NodeType.CarCross ? ViewConsts.GREEN_LIGHT_COLOR.ToArgb() :
                                node.Type == NodeType.TramStop ? ViewConsts.STOP_COLOR.ToArgb() : ViewConsts.POINT_NORMAL_COLOR.ToArgb(),
                            ViewConsts.POINT_RADIUS,
                            ViewConsts.POINT_PRECISION));
                }

                if (node.Child != null)
                {
                    float pX2 = CalculateXPosition(node.Child.Node.Coordinates.X);
                    float pY2 = CalculateYPosition(node.Child.Node.Coordinates.Y);
                    edges.Add(DirectxHelper.CreateLine(pX, pY, pX2, pY2, GetLineColor(node, node.Child.Node).ToArgb(), ViewConsts.POINT_RADIUS));
                }
                else if (node.Children != null)
                {
                    foreach (var child in node.Children)
                    {
                        float pX2 = CalculateXPosition(child.Node.Coordinates.X);
                        float pY2 = CalculateYPosition(child.Node.Coordinates.Y);
                        edges.Add(DirectxHelper.CreateLine(pX, pY, pX2, pY2, GetLineColor(node, child.Node).ToArgb(), ViewConsts.POINT_RADIUS));
                    }
                }
            }
        }

        public void Render(Device device, Vector3 cameraPosition, string time)
        {
            if (!isDeviceInit)
            {
                System.Drawing.Font systemfont = new System.Drawing.Font("Arial", 12f, System.Drawing.FontStyle.Regular);
                text = new Microsoft.DirectX.Direct3D.Font(device, systemfont);
                line = new Line(device);
                lineVertexes = new Vector2[] { new Vector2(8, 8), new Vector2(55, 8), new Vector2(55, 31), new Vector2(8, 31), new Vector2(8, 8) };
                isDeviceInit = true;
            }

            //DRAW EDGES
            foreach (var edge in edges)
            {
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, edge);
            }

            //DRAW POINTS
            foreach (var vertex in vertexes)
            {
                device.DrawUserPrimitives(PrimitiveType.TriangleFan, ViewConsts.POINT_PRECISION, vertex);
            }

            //DRAW VEHICLES
            foreach (var vehicle in mainController.Vehicles)
            {
                float x = CalculateXPosition(vehicle.Position.Coordinates.X);
                float y = CalculateYPosition(vehicle.Position.Coordinates.Y);

                device.DrawUserPrimitives(
                    PrimitiveType.TriangleFan,
                    ViewConsts.POINT_PRECISION,
                    DirectxHelper.CreateCircle(x, y, Color.Red.ToArgb(), GetPointRadius(cameraPosition.Z), ViewConsts.POINT_PRECISION));

                //float pX2 = CalculateXPosition(vehicle.Position.Node1.Coordinates.X);
                //float pY2 = CalculateYPosition(vehicle.Position.Node1.Coordinates.Y);
                //device.DrawUserPrimitives(
                //    PrimitiveType.TriangleStrip, 
                //    2,
                //    DirectxHelper.CreateLine(x, y, pX2, pY2, Color.Red.ToArgb(), GetPointRadius(cameraPosition.Z)));

                float pX2 = CalculateXPosition(vehicle.Position.Node1.Coordinates.X);
                float pY2 = CalculateYPosition(vehicle.Position.Node1.Coordinates.Y);
                float pX3 = CalculateXPosition(vehicle.Position.Node2.Coordinates.X);
                float pY3 = CalculateYPosition(vehicle.Position.Node2.Coordinates.Y);

                device.DrawUserPrimitives(
                    PrimitiveType.TriangleStrip,
                    2,
                    DirectxHelper.CreateLine(pX2, pY2, pX3, pY3, Color.Red.ToArgb(), ViewConsts.POINT_RADIUS));
            }

            //DRAW CAR INTERSECTIONS
            foreach (var intersection in mainController.CarIntersections)
            {
                float x = CalculateXPosition(intersection.Node.Coordinates.X);
                float y = CalculateYPosition(intersection.Node.Coordinates.Y);

                device.DrawUserPrimitives(
                    PrimitiveType.TriangleFan,
                    ViewConsts.POINT_PRECISION,
                    DirectxHelper.CreateCircle(
                        x, 
                        y, 
                        intersection.Node.LightState == LightState.Green ? Color.Green.ToArgb() : Color.Red.ToArgb(),
                        ViewConsts.POINT_RADIUS * 2, 
                        ViewConsts.POINT_PRECISION));
            }

            //DRAW TIME
            text.DrawText(null, time, new Point(12, 11), Color.Black);
            line.Draw(lineVertexes, Color.Black);
        }

        #endregion Public Methods

        #region Private Methods

        private float CalculateXPosition(float originalX)
        {
            return (100 - (originalX - minX) * 100 / (maxX - minX)) - 50; // X axis is swapped
        }

        private float CalculateYPosition(float originalY)
        {
            return (originalY - minY) * 100 / (maxX - minX) - (50 * (minY - maxY)) / (minX - maxX);
        }

        private float GetPointRadius(float cameraHeight)
        {
            return (cameraHeight * (19f / 99) + (80f / 99)) * ViewConsts.POINT_RADIUS;
        }
        
        private static Color GetLineColor(Node node1, Node node2)
        {
            return node1 != null && node1.IsUnderground && node2 != null && node2.IsUnderground ? ViewConsts.LINE_UNDERGROUND_COLOR : ViewConsts.LINE_BASIC_COLOR;
        }

        #endregion Private Methods
    }
}
