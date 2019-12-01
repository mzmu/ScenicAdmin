using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace ScenicAdmin
{
    static class Program
    {
        private static LicenseInitializer m_AOLicenseInitializer = new ScenicAdmin.LicenseInitializer();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ESRI License Initializer generated code.
            m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngine },
            new esriLicenseExtensionCode[] { esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork, esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst });
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            welcomForm welcomeForm = new welcomForm();
            welcomeForm.Show();
            System.Threading.Thread.Sleep(2000);  //The welcome window stays for two seconds.
            welcomeForm.Close();
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new ScenicAdminMainForm());
            }
            
            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.ShutdownApplication();
        }
    }
}