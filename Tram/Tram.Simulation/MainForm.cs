using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tram.Common.Consts;
using Tram.Simulation.Properties;
using Tram.Controller.Controllers;
using System.Linq;
using System.Collections.Generic;

namespace Tram.Simulation
{
    public partial class MainForm : Form
    {
        private Device device;

        private Vector3 cameraPosition, cameraTarget;
        private Point lastClickedMouseLocation;

        private DateTime lastUpdateTime;
        private long lastVehiclesHashCode;
        private List<string> vehiclesIds;
        private List<string> removedVehiclesIds;

        public MainForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            SetLanguage();

            // Set handlers
            renderPanel.MouseWheel += RenderPanel_MouseWheel;

            // Set variables
            cameraPosition = new Vector3(0, 0, ViewConsts.START_CAMERA_Z);
            cameraTarget = new Vector3(0, 0, 0);

            lastUpdateTime = DateTime.Now;
            vehiclesIds = new List<string>();
            removedVehiclesIds = new List<string>();
        }

        public void SetLanguage()
        {
            Text = Resources.Window_Title;
        }

        public bool InitializeGraphics()
        {
            try
            {
                PresentParameters presentParams = new PresentParameters();
                presentParams.Windowed = true;
                presentParams.SwapEffect = SwapEffect.Discard;
                device = new Device(0, DeviceType.Hardware, renderPanel, CreateFlags.MixedVertexProcessing, presentParams);
                return true;
            }
            catch (DirectXException)
            {
                return false;
            }
        }

        public void Update(MainController controller)
        {
            if ((DateTime.Now - lastUpdateTime).TotalSeconds > CalculationConsts.INTERFACE_REFRESH_TIME_INTERVAL)
            {
                lastUpdateTime = DateTime.Now;

                var ids = controller.Vehicles.Select(v => v.Id);
                var hashcode = ids.Select(id => (long)id.GetHashCode()).Sum();
                if (lastVehiclesHashCode != hashcode)
                {
                    removedVehiclesIds.AddRange(vehiclesIds.Where(v => !ids.Contains(v)));
                    vehiclesIds.Clear();
                    vehiclesIds.AddRange(ids);
                    lastVehiclesHashCode = hashcode;

                    listView1.Items.Clear();
                    vehiclesIds.ForEach(v => listView1.Items.Add(v));
                    removedVehiclesIds.ForEach(v => listView1.Items.Add(v));
                    for (int i = vehiclesIds.Count; i < removedVehiclesIds.Count + vehiclesIds.Count; i++)
                    {
                        listView1.Items[i].ForeColor = Color.Red;
                    }
                }
            }
        }

        public void Render(Action<Device, Vector3> renderAction)
        {
            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, renderPanel.Width / renderPanel.Height, 1f, 1000f);
            device.Transform.View = Matrix.LookAtLH(cameraPosition, cameraTarget, new Vector3(0, 1, 0));
            device.RenderState.Lighting = false;
            device.RenderState.CullMode = Cull.None;

            device.Clear(ClearFlags.Target, Color.WhiteSmoke, 1.0f, 0);
            
            device.BeginScene();

            device.VertexFormat = CustomVertex.PositionColored.Format;

            //Invoke render action
            renderAction(device, cameraPosition);
            
            device.EndScene();
            device.Present();
            Invalidate();
        }

        #region Map Transformation Methods

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            if (cameraPosition.Z > ViewConsts.ZOOM_OFFSET)
            {
                cameraPosition.Z -= ViewConsts.ZOOM_OFFSET;
            }
            else if (cameraPosition.Z > 1 + (ViewConsts.ZOOM_OFFSET / 20))
            {
                cameraPosition.Z -= ViewConsts.ZOOM_OFFSET / 20;
            }
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            if (cameraPosition.Z < ViewConsts.ZOOM_OFFSET)
            {
                cameraPosition.Z += ViewConsts.ZOOM_OFFSET / 20;
            }
            else if (cameraPosition.Z < ViewConsts.START_CAMERA_Z * 1.5)
            {
                cameraPosition.Z += ViewConsts.ZOOM_OFFSET;
            }

            if (cameraPosition.Z > ViewConsts.START_CAMERA_Z * 1.5)
            {
                cameraPosition.Z = ViewConsts.START_CAMERA_Z * 1.5f;
            }
        }

        private void RenderPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomInButton_Click(this, new EventArgs());
            }
            else if (e.Delta < 0)
            {
                ZoomOutButton_Click(this, new EventArgs());
            }
        }

        private void zoomOriginalButton_Click(object sender, EventArgs e)
        {
            cameraPosition.Z = ViewConsts.START_CAMERA_Z;
        }

        private void centerScreenButton_Click(object sender, EventArgs e)
        {
            cameraPosition.X = cameraPosition.Y = cameraTarget.X = cameraTarget.Y = 0;
        }

        private void renderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                if (!lastClickedMouseLocation.IsEmpty && lastClickedMouseLocation != e.Location)
                {
                    float xDiff = Math.Abs(lastClickedMouseLocation.X - e.Location.X);
                    if (lastClickedMouseLocation.X > e.Location.X)
                    {
                        cameraPosition.X -= ViewConsts.SWIPE_OFFSET * cameraPosition.Z * xDiff;
                    }
                    else if (lastClickedMouseLocation.X < e.Location.X)
                    {
                        cameraPosition.X += ViewConsts.SWIPE_OFFSET * cameraPosition.Z * xDiff;
                    }

                    float yDiff = Math.Abs(lastClickedMouseLocation.Y - e.Location.Y);
                    if (lastClickedMouseLocation.Y > e.Location.Y)
                    {
                        cameraPosition.Y -= ViewConsts.SWIPE_OFFSET * cameraPosition.Z * yDiff;
                    }
                    else if (lastClickedMouseLocation.Y < e.Location.Y)
                    {
                        cameraPosition.Y += ViewConsts.SWIPE_OFFSET * cameraPosition.Z * yDiff;
                    }

                    cameraTarget.X = cameraPosition.X;
                    cameraTarget.Y = cameraPosition.Y;
                }

                lastClickedMouseLocation = e.Location;
            }
            else
            {
                lastClickedMouseLocation = Point.Empty;
            }
        }

        #endregion Map Transformation Methods
    }
}
