using DVLD_Buissness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD___Driving_Licenses_Managment.License
{
    public partial class FrmIssueDrivingLicense : Form
    {
        private int local_applicationID = -1; 
        public FrmIssueDrivingLicense(int LocalApplicationID)
        {
            local_applicationID = LocalApplicationID;
            InitializeComponent();
        }

        private void FrmIssueDrivingLicense_Load(object sender, EventArgs e)
        {
            cntrlDrivingLicenseAndApplicationBasicInfo1.LoadInformation(local_applicationID); 
        }

        
        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenses LocalApplication = clsLocalDrivingLicenses.Find(local_applicationID);

            int LicenseID = LocalApplication.IssueLicenseForTheFirstTime(clsGlobal.CurrentUser.ID,txtNotes.Text); 

            if(LicenseID != -1)
            {
                if(MessageBox.Show($"License Issued Successfully with ID = {LicenseID.ToString()}.", "Message Box",
                     MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    btnIssue.Enabled = false;
                    FrmIssueDrivingLicense_Load(null, null); 
                    return;
                }
            }
            else 
                    MessageBox.Show("Something went wrong,Check again later!", "Message Box",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
