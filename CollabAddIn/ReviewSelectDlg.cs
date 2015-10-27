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

namespace CollabAddIn
{
    public partial class ReviewSelectDlg : Form
    {
        private List<UserService.SuggestedReview> suggestedReviews = new List<UserService.SuggestedReview>();
        private BindingSource bsReviewSuggestions = new BindingSource();
        private CollabAPI api;

        private bool isReviewSuggestionError = false;

        public int selectedReviewId { get; private set; }

        public ReviewSelectDlg() : this (null)
        {}

        public ReviewSelectDlg(CollabAPI collabAPI)
        {
            this.api = collabAPI;

            InitializeComponent();
        }

        public void SetDialogState()
        {
                        
        }

        private void ReviewSelectDlg_Load(object sender, EventArgs e)
        {
            // No review selected
            selectedReviewId = 0;

            // Configure the binding source
            bsReviewSuggestions.DataSource = suggestedReviews;

            // Default to create new review
            rBtnNewReview.Checked = true;

            // Set the server hyperlink up
            lnkServerLink.Text = ((api == null) ? Properties.Settings.Default.CollabServerURL : api.serverURL);
                
            lbSuggestedReviews.DisplayMember = "displayText";    // Use the displayText field of the suggestedReview class
            lbSuggestedReviews.ValueMember = "reviewId";         // Use the reviewId field of the suggestedReview class
            lbSuggestedReviews.DataSource = bsReviewSuggestions; // Get data from the binding source

            // Center the loading panel over the listbox control
            pnlLoading.Top = groupBox1.Top + lbSuggestedReviews.Top + (lbSuggestedReviews.Height / 2) - (pnlLoading.Height / 2);
            pnlLoading.Left = groupBox1.Left + lbSuggestedReviews.Left + (lbSuggestedReviews.Width / 2) - (pnlLoading.Width / 2);

            // Show the loading panel
            pnlLoading.Visible = true;

            // Start background thread to fill the suggested review listbox
            if (!wrkrSuggestedReviews.IsBusy)
                wrkrSuggestedReviews.RunWorkerAsync();

            // Give the new review title textbox focus
            this.ActiveControl = tbReviewTitle;
            
            // Enable/Disable controls as appropriate
            CheckForm();
        }

        private void wrkrSuggestedReviews_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Get the API wrapper object, if we don't already have one
                if (api == null)
                {
                    api = new CollabAPI(Properties.Settings.Default.CollabServerURL,
                       Properties.Settings.Default.CollabUser,
                       Properties.Settings.Default.CollabLoginTicket
                       );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                // Get up to 5000 review suggestions
                JsonResult response = api.getSuggestedReviews(UserService.SuggestionType.UPLOAD, "", 5000);

                // Return the response to the UI thread
                e.Result = response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void wrkrSuggestedReviews_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Wipe out our list
            suggestedReviews.Clear();

            if (e.Error != null) // We threw an exception in the worker thread
            {
                suggestedReviews.Add(new UserService.SuggestedReview() { displayText = "Error retrieving review suggestions!" });
                suggestedReviews.Add(new UserService.SuggestedReview() { displayText = e.Error.Message });
                isReviewSuggestionError = true;
            }
            else // Worker thread returned a JsonResult
            {
                // Get the response from the background thread
                JsonResult response = (JsonResult)e.Result;

                if (!response.isError()) // Check for server side errors
                {
                    // Get the review suggestions
                    suggestedReviews.AddRange(response.GetResponse<UserService.SuggestedReviewsResponse>().suggestedReviews);

                    if (suggestedReviews.Count < 1)
                    {
                        // There are no review suggestions, tell the user, and treat it like an error
                        suggestedReviews.Add(new UserService.SuggestedReview() { displayText = "There are no existing reviews that you can upload to." });
                        isReviewSuggestionError = true;
                    }
                }
                else
                {
                    suggestedReviews.Add(new UserService.SuggestedReview() { displayText = "Error retrieving review suggestions!" });
                    suggestedReviews.Add(new UserService.SuggestedReview() { displayText = response.GetErrorString() });

                    isReviewSuggestionError = true;
                }
            }
            // Hide the loading panel, even if we got an error
            pnlLoading.Visible = false;

            // Populate the list box with the response data
            bsReviewSuggestions.ResetBindings(false);
            // Clear currently selected review
            lbSuggestedReviews.SelectedIndex = -1;

            CheckForm();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            // Do a case insensitive search with linq <3
            bsReviewSuggestions.DataSource = suggestedReviews.Where(r => r.displayText.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            // Update the list view
            bsReviewSuggestions.ResetBindings(false);
            // Clear currently selected review
            lbSuggestedReviews.SelectedIndex = -1;
        }

        private void lbSuggestedReviews_EnabledChanged(object sender, EventArgs e)
        {
            // Sync the search text box enabled/disabled flag with the list box one.
            tbSearch.Enabled = lbSuggestedReviews.Enabled;
        }

        private void lnkServerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Start background thread to fill the suggested review listbox
            if (!wrkrSuggestedReviews.IsBusy)
                wrkrSuggestedReviews.RunWorkerAsync();
            // Navigate to the URL
            System.Diagnostics.Process.Start(lnkServerLink.Text);
        }
        
        private void CheckForm()
        {
            // Is this a new review?
            bool isNewReview = rBtnNewReview.Checked;
            // Are the suggestions loaded?
            bool isSugLoaded = (!isReviewSuggestionError && !wrkrSuggestedReviews.IsBusy);

            // Set enabled flags for controls that depend on the radio buttons
            tbReviewTitle.Enabled = isNewReview;
            lblTitle.Enabled = isNewReview;

            // Upload button is enabled for all new reviews, or if suggestions are loaded and something is selected
            btnUpload.Enabled = (isNewReview || (isSugLoaded && lbSuggestedReviews.SelectedItems.Count > 0));

            // Search is enabled when it's an existing review, and suggestions are loaded
            tbSearch.Enabled = (!isNewReview && isSugLoaded);
            lblSearch.Enabled = (!isNewReview && isSugLoaded); // label

            // Suggested review list is enabled when it's an existing review, and suggestions are loaded
            lbSuggestedReviews.Enabled = (!isNewReview && isSugLoaded);
            lblExistingReviews.Enabled = (!isNewReview && isSugLoaded); // label

            // Existing review option is enabled as long as the list loads
            rBtnExistingReview.Enabled = isSugLoaded;

            // Set the review ID for the caller
            selectedReviewId = 0; // Default to a new review
            
            if (!isNewReview && lbSuggestedReviews.SelectedItem != null ) // If we want an existing review, and we have a selection
            {
                 selectedReviewId = (((UserService.SuggestedReview)lbSuggestedReviews.SelectedItem)).reviewId;
            }

        }

        private void tbReviewTitle_TextChanged(object sender, EventArgs e)
        {
            CheckForm();
        }
        private void rBtnNewReview_CheckedChanged(object sender, EventArgs e)
        {
            CheckForm();
        }
        private void rBtnExistingReview_CheckedChanged(object sender, EventArgs e)
        {
            CheckForm();
        }

        private void lbSuggestedReviews_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckForm();
        }
    }
}
