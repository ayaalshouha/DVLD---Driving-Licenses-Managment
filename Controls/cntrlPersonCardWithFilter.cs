using DVLD_Buissness;
using System;
using System.Windows.Forms;

namespace DVLD___Driving_Licenses_Managment
{
    public partial class cntrlPersonCardWithFilter : UserControl
    {
        //Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;

        //Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                //Raise the event with the parameter
                handler(PersonID);
            }
        }
        public cntrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabeled = true;
        public bool FilterEnabeled
        {
            get { return _FilterEnabeled;}
                
            set{
                _FilterEnabeled = value;
                gbFilterBox.Enabled = _FilterEnabeled;
            }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return cntrPersonCard1.SelectedPersonInfo; }
        }
        public void UpdateData(object sender, int PersonID)
        {
            // Handle the data received
            if (PersonID != -1)
            {
                cntrPersonCard1.LoadPersonInfo(PersonID);
                FilterEnabeled = false;
                txtInput.Text = PersonID.ToString();
                cbFindby.Text = "PersonID";
            }
        }
        public int ID
        {
            get { return cntrPersonCard1.ID; }
        }

        public void FilterFocused()
        {
            txtInput.Focus();
        }
        private void cntrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFindby.SelectedIndex = 1;
            txtInput.Focus();
        }

        public void LoadPersonInfo(int personID)
        {
            if (clsPerson.isExist(personID))
            {
                cntrPersonCard1.LoadPersonInfo(personID);
                txtInput.Text = personID.ToString();
                cbFindby.Text = "PersonID"; 
            }
               

            if (OnPersonSelected != null)
            {
                PersonSelected(personID);
            }
            return;
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                return; 
            }

            if(cbFindby.SelectedItem.ToString() == "PersonID")
            {
                if(int.TryParse(txtInput.Text, out int PeronID)){
                    cntrPersonCard1.LoadPersonInfo(PeronID);

                    if (OnPersonSelected != null)
                    {
                        PersonSelected(PeronID);
                    }
                }
            }
            else
            {
                cntrPersonCard1.LoadPersonInfo(txtInput.Text);
            }
        }

        public bool isNull()
        {
            return cntrPersonCard1.ID <= 0;  
        }
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            //person ID back form adding person form to the control and control be filled with its info
            FrmAddEditPerson Form = new FrmAddEditPerson();
            Form.DataBack += UpdateData;
            Form.ShowDialog();
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if user press enter then perform find event
            if (e.KeyChar == (char)13)
                btnFind.PerformClick(); 


            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
