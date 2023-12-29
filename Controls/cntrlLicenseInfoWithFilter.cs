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

namespace DVLD___Driving_Licenses_Managment.Controls
{
    public partial class cntrlLicenseInfoWithFilter : UserControl
    {

        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID); // Raise the event with the parameter
            }
        }

        private clsLicenses _License;
        private int _LicenseID = -1;
        private bool _FilterEnabled = true;

        public clsLicenses SelectedLicenseInfo
        {
            get { return _License; }
        }
        
        public int ID
        {
            get { return _LicenseID; }
            set { _LicenseID = value; }
        }
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set 
            { 
                _FilterEnabled = value;
                gbFilterBox.Enabled = _FilterEnabled;
            }
        }

        public cntrlLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
                return;

            if(int.TryParse(txtInput.Text, out int Result)){
                _LicenseID = Result;
                _License = clsLicenses.Find(_LicenseID); 
            }

            if(_License != null)
            {
                cntrlLicenseInfo1.LoadLicenseInfo(_License.ID);

                if(OnLicenseSelected != null)
                    // Raise the event with a parameter
                    OnLicenseSelected(_LicenseID);
            }
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicenses.Find(_LicenseID);

            if (_License != null)
            {
                FilterEnabled = false;
                cntrlLicenseInfo1.LoadLicenseInfo(_License.ID);

                if (OnLicenseSelected != null)
                    // Raise the event with a parameter
                    OnLicenseSelected(_LicenseID);

            }
        }
        private void cntrlLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {
        }

        private void txtInput_Validating(object sender, CancelEventArgs e)
        {
            if (!clsGlobal.isNumber(txtInput.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtInput, "Invalid Number!");
            }
            else
                errorProvider1.SetError(txtInput, null);
        }
    }
}
