using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using Microsoft.Office.Tools;
using Microsoft.Office.Interop.Word;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using SmartBear.Collab;

namespace CollabAddIn
{
    public partial class SBMainRibbon
    {
        private void MainRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            if (Properties.Settings.Default.settingsValidated)
            {
                this.btnUpload.Enabled = true;
            }
            else
            {
                MessageBox.Show("Your Collaborator server connection is not configured. Please configure it now.",
                    "Collaborator by SmartBear", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOptions_Click(sender, null);
            }

            if (Properties.Settings.Default.actionItemsVisible)
            {

            }
        }

        private void btnOptions_Click(object sender, RibbonControlEventArgs e)
        {
            // Create an instance of the Collab options form
            SBOptionsForm form = new SBOptionsForm();

            // Pre-load the form data from settings
            form.tbServerURL.Text = Properties.Settings.Default.CollabServerURL;
            form.tbUsername.Text = Properties.Settings.Default.CollabUser;
            form.tbPassword.Text = Properties.Settings.Default.CollabPass;

            // If the DialogResult is OK
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Save the form data into settings
                Properties.Settings.Default.CollabServerURL = form.tbServerURL.Text.TrimEnd('/');
                Properties.Settings.Default.CollabUser = form.tbUsername.Text;
                Properties.Settings.Default.CollabPass = form.tbPassword.Text;
                Properties.Settings.Default.CollabLoginTicket = form.loginTicket;
                Properties.Settings.Default.settingsValidated = true;
                this.btnUpload.Enabled = true;
                Properties.Settings.Default.Save();
            }
        }

        private void btnUpload_Click(object sender, RibbonControlEventArgs e)
        {
            Document curDoc = Globals.ThisAddIn.Application.ActiveDocument;
            string zipFileName = Path.GetTempPath().TrimEnd('\\') + "\\temp_ccollabwordaddinupload.zip";
            string docFileName = Path.GetTempPath().TrimEnd('\\') + "\\temp_ccollabwordaddinupload.docx";
            string curDocMD5 = "";
            string oldFilePath = "";
            string oldFileName = "";
            int revID = 0;
            string uploadComment = "";
            string revTitle = "";

            // Make sure the current document is saved before we do anything
            while (!curDoc.Saved || (curDoc.Path.Length < 1))
            {
                // User hasn't saved the current document, prompt them to save it!
                if (DialogResult.Yes == MessageBox.Show(
                    "You must save the current document before you can upload it to Collaborator for review. Would you like to save now?",
                    "Collaborator by SmartBear", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    curDoc.Save();
                }
                else
                    return; // User didn't say yes to save, cancel the upload request

                // Run in a loop to make sure the document has no pending changes. Maybe the save or save as failed.
            }
            // Document is saved, next get the review that we want to upload to
            ReviewSelectDlg revSelectDlg = new ReviewSelectDlg();
            if (DialogResult.OK == revSelectDlg.ShowDialog())
            {
                revID = revSelectDlg.selectedReviewId;
                revTitle = revSelectDlg.tbReviewTitle.Text;
                uploadComment = revSelectDlg.tbComment.Text;
            }
            else
                return; // User hit cancel

            // Now save an extra copy
            oldFileName = curDoc.Name;
            oldFilePath = curDoc.FullName;

            try
            {
                // Quickly save to a temp directory
                curDoc.SaveAs2(docFileName);
                // Now re-save back to the original location to release the file lock on the temp file
                curDoc.SaveAs2(oldFilePath);

                if (File.Exists(zipFileName)) // If we've got an old zip file lying around
                    File.Delete(zipFileName); // then delete it

                // Get the MD5SUM for the document
                using (var md5 = MD5.Create())
                {
                    // Make sure we open with the lowest acces rights possible
                    using (var stream = File.Open(docFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        curDocMD5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                    }
                }

                // Create a new zip archive
                using (ZipArchive newFile = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry newEntry = newFile.CreateEntryFromFile(docFileName, curDocMD5, CompressionLevel.Optimal);
                }
            }
            catch (Exception ex)
            {
                // Something bad happened
                MessageBox.Show(ex.Message, "Collaborator by SmartBear", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Abort the whole operation
            }

            // Everything is zipped, let's upload it
            try
            {
                CollabServerRequest.UploadZipFile(
                        Properties.Settings.Default.CollabServerURL,
                        zipFileName,
                        new SessionService.authenticate
                        {
                            login = Properties.Settings.Default.CollabUser,
                            ticket = Properties.Settings.Default.CollabLoginTicket
                        });
            }
            catch (Exception ex)
            {
                // TODO: Do something better with exceptions
                // Tell the user about the low level communication error. e.g. connection refused
                MessageBox.Show(ex.Message, "Collaborator by SmartBear", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Abort the whole operation
            }


            // The lists that store our request and response batches
            List<JsonCommand> requestList = new List<JsonCommand>();
            List<JsonResult> responseList = new List<JsonResult>();

            // If we need to create a new review
            if (revID == 0)
            {
                // Authenticate with our token
                requestList.Add(new JsonCommand(
                    new SessionService.authenticate
                    {
                        login = Properties.Settings.Default.CollabUser,
                        ticket = Properties.Settings.Default.CollabLoginTicket
                    }
                ));

                // Review creation request
                requestList.Add(new JsonCommand(
                    new ReviewService.createReview
                    {
                        creator = Properties.Settings.Default.CollabUser,
                        title = revTitle
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
                    MessageBox.Show(ex.Message, "Collaborator by SmartBear", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (responseList != null && responseList.Count() > 0) // Execute block if we didn't error above
                {
                    // Get the items in the list, in order
                    List<JsonResult>.Enumerator resultEnum = responseList.GetEnumerator();

                    try
                    {
                        resultEnum.MoveNext(); // get the first command response

                        // first command is authenticate, make sure there's no error.
                        if (resultEnum.Current.isError())
                        {
                            // Error!
                            // Assume the ticket is not valid, and clear out the stored one
                            Properties.Settings.Default.CollabLoginTicket = "";
                            Properties.Settings.Default.settingsValidated = false;
                            Properties.Settings.Default.Save();
                            // Stop execution, and display an error
                            throw (new Exception(resultEnum.Current.GetErrorString()));
                        }

                        resultEnum.MoveNext(); // get next command response

                        // get the review ID of the new review
                        if (resultEnum.Current.isError())
                            throw (new Exception(resultEnum.Current.GetErrorString())); // Error!
                        else
                            revID = resultEnum.Current.GetResponse<ReviewService.createReviewResponse>().reviewId;

                    }
                    catch (Exception ex)
                    {
                        // TODO: Add different exception types to differentiate between api errors and potential deserialization explosions
                        MessageBox.Show(ex.Message, "Collaborator by SmartBear", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Clean up before our next API call
                requestList.Clear();
                responseList.Clear();

            }



            // Create the API request objects
            // Authenticate with our token
            requestList.Add(new JsonCommand(
                new SessionService.authenticate
                {
                    login = Properties.Settings.Default.CollabUser,
                    ticket = Properties.Settings.Default.CollabLoginTicket
                }
            ));

            // Create version descriptor
            List<ReviewService.version> vrsns = new List<ReviewService.version>();
            vrsns.Add(new ReviewService.version
                {
                    md5 = curDocMD5,
                    localPath = oldFileName
                });

            // Create changelist descriptor
            List<ReviewService.changelist> chgLists = new List<ReviewService.changelist>();
            chgLists.Add(new ReviewService.changelist
                {
                    versions = vrsns,
                    commitInfo = new ReviewService.commitInfo
                        {
                               comment = uploadComment
                        }
                });

            // Create addFiles request
            requestList.Add(new JsonCommand(
                new ReviewService.addFiles
                {
                    reviewId = revID,
                    changelists = chgLists,
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
                MessageBox.Show(ex.Message, "Collaborator by SmartBear", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (responseList != null && responseList.Count() > 0) // Execute block if we didn't error above
            {
                // Get the items in the list, in order
                List<JsonResult>.Enumerator resultEnum = responseList.GetEnumerator();

                try
                {
                    resultEnum.MoveNext(); // get the first command response

                    // first command is authenticate, make sure there's no error.
                    if (resultEnum.Current.isError())
                    {
                        // Error!
                        // Assume the ticket is not valid, and clear out the stored one
                        Properties.Settings.Default.CollabLoginTicket = "";
                        Properties.Settings.Default.settingsValidated = false;
                        Properties.Settings.Default.Save();
                        // Stop execution, and display an error
                        throw (new Exception(resultEnum.Current.GetErrorString()));
                    }
                        
                    resultEnum.MoveNext(); // get next command response

                    // make sure we didn't error on addFiles
                    if (resultEnum.Current.isError())
                        throw (new Exception(resultEnum.Current.GetErrorString())); // Error!

                    // Success! Offer to open up the review.
                    if (DialogResult.Yes == MessageBox.Show(
                    "We've successfully attached \"" + oldFileName + "\" to Review " + revID.ToString() + "!\r\n\r\n" + "Would you like to open it in your browser?" ,
                    "Collaborator by SmartBear", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        System.Diagnostics.Process.Start(Properties.Settings.Default.CollabServerURL + @"/ui#review:id=" + revID.ToString());
                    }

                }
                catch (Exception ex)
                {
                    // TODO: Add different exception types to differentiate between api errors and potential deserialization explosions
                    MessageBox.Show(ex.Message, "Collaborator by SmartBear", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
