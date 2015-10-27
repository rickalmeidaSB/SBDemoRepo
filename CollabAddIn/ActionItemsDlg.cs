using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartBear.Collab;

namespace CollabAddIn
{
    public partial class ActionItemsDlg : UserControl
    {
        public ActionItemsDlg()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // The lists that store our request and response batches
            List<JsonCommand> requestList = new List<JsonCommand>();
            List<JsonResult> responseList = new List<JsonResult>();

            // Create the API request objects
            // Authenticate with our token
            requestList.Add(new JsonCommand(
                new SessionService.authenticate
                {
                    login = Properties.Settings.Default.CollabUser,
                    ticket = Properties.Settings.Default.CollabLoginTicket
                }
            ));
            // Get action items
            requestList.Add(new JsonCommand(
                new UserService.getActionItems
                {
                    // no args
                }
            ));

            try
            {
                // Send the requests, it's our responsibility to trap errors here, they should be friendly
                responseList = CollabServerRequest.Execute(Properties.Settings.Default.CollabServerURL, requestList);
            }
            catch (Exception ex)
            {
                // TODO: Do something better with exceptions
                // Tell the user about the low level communication error. e.g. connection refused
                MessageBox.Show(ex.Message, "Error sending request", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (responseList != null && responseList.Count() > 0) // Execute block if we didn't error above
            {
                // Get the items in the list, in order
                List<JsonResult>.Enumerator resultEnum = responseList.GetEnumerator();
                resultEnum.MoveNext(); // get the first response

                try
                {
                    // first command is authenticate, make sure there's no error.
                    if (resultEnum.Current.isError())
                    {
                        // Assume the ticket is not valid, and clear out the stored one
                        Properties.Settings.Default.CollabLoginTicket = "";
                        Properties.Settings.Default.settingsValidated = false;
                        Properties.Settings.Default.Save();
                        // Stop execution, and display an error
                        throw (new Exception(resultEnum.Current.GetErrorString()));
                    }

                    resultEnum.MoveNext(); // get next command response

                    // make sure we didn't error on action item retrieval
                    if (resultEnum.Current.isError())
                        throw (new Exception(resultEnum.Current.GetErrorString()));

                    // deserialize action items
                    UserService.getActionItemsResponse actItems = resultEnum.Current.GetResponse
                        <UserService.getActionItemsResponse>();

                    listBox1.BeginUpdate();
                    listBox1.Items.Clear();

                    foreach (UserService.ActionItem act in actItems.actionItems)
                    {
                        listBox1.Items.Add(act.text);
                    }
                    listBox1.EndUpdate();

                }
                catch (Exception ex)
                {
                    // TODO: Add different exception types to differentiate between api errors and potential deserialization explosions
                    MessageBox.Show(ex.Message, "Error returned from server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
                System.Diagnostics.Process.Start( Properties.Settings.Default.CollabServerURL + @"/ui#review:id=" +
                    listBox1.GetItemText(listBox1.SelectedItem).Split(':').ToArray()[1].Substring(9));
        }
    }
}
