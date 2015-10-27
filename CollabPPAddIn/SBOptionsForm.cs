using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartBear.Collab;

namespace CollabPPAddIn
{
    public partial class SBOptionsForm : Form
    {
        public SBOptionsForm()
        {
            InitializeComponent();
        }

        public string loginTicket = "";

        // Happens when the user presses the save button on the dialog
        // We validate our server connection before allowing DialogResult.OK
        private void btnSave_Click(object sender, EventArgs e)
        {
            // The lists that store our request and response batches
            List<JsonCommand> requestList = new List<JsonCommand>();
            List<JsonResult> responseList = new List<JsonResult>();

            // Create the API get login ticket request object
            SessionService.getLoginTicket request = new SessionService.getLoginTicket();
            // Populate the request fields with our form data
            request.login = tbUsername.Text;
            request.password = tbPassword.Text;

            // Queue up the above request, wrapping it in a JsonCommand object
            requestList.Add(new JsonCommand(request));

            try
            {
                // Send the request, it's our responsibility to trap errors here, they should be friendly
                responseList = CollabServerRequest.Execute(tbServerURL.Text, requestList);
            }
            catch (Exception ex)
            {
                // TODO: Do something better with exceptions
                // Tell the user about the low level communication error. e.g. connection refused
                MessageBox.Show(ex.Message, "Error sending request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None; // There was an error, don't close the dialog
            }

            if (responseList != null && responseList.Count() > 0) // Execute block if we didn't error above
            {
                foreach (JsonResult obj in responseList) // There should only be 1 response here
                {
                    // This block catches errors returned as an error object by the Collab API
                    if (!obj.isError()) // Did the user authenticate successfully?
                    { 
                        // Authenticated!
                        // Put the login ticket in a handy place, where the caller can get it
                        this.loginTicket = obj.GetResponse<SessionService.getLoginTicketResponse>().loginTicket;
                        // Let the dialog close with DialogResult.OK, the caller will save form data
                    }
                    else
                    {
                        // Authentication error, tell the user what happened
                        MessageBox.Show(obj.GetErrorString(), "Error returned from server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.None; // There was an error, don't close the dialog
                    }
                }
            }
        }
    }
}
