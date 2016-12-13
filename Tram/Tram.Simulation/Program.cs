using System;
using System.Windows.Forms;
using Tram.Common.Helpers;
using Tram.Controller;
using Tram.Controller.Controllers;

namespace Tram.Simulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainController controller = Kernel.Get<MainController>();
            controller.StartApplication(TimeHelper.GetTime("05:08"), 1);

            using (MainForm form = new MainForm())
            {
                //int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height - 60;
                form.Size = new System.Drawing.Size(screenHeight * form.Width / form.Height, screenHeight );
                if (!form.InitializeGraphics())
                {
                    MessageBox.Show("Could not initialize Direct3D.");
                    return;
                }

                form.Show();

                while (form.Created)
                {
                    controller.Update();               
                    form.Render(controller.Render);
                    Application.DoEvents();
                }
            }
        }
    }
}
