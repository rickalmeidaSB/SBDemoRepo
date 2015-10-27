using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartBear.Collab;
using System.Windows.Forms;

namespace APITest
{
    class Program
    {
        static string serverURL = "http://localhost:8080";
        static string userName = "01";
        static string userPass = "";
        
        static void Main(string[] args)
        {
            WL("Please select an option:");
            WL("1. Review Selection Dialog Test");
            WL("2. API Test");
            WL();
            WL("Press any other key to exit.");
            switch (Console.ReadKey(true).KeyChar)
            {
                case '1':
                    DoDialogTest();
                    break;
                case '2':
                    DoAPITest();
                    break;
                default:
                    break;
            }
        }
        static void DoDialogTest()
        {
            WL("Creating CollabAPI() instance");
            WL("Server URL: " + serverURL);
            WL("User Name: " + userName);
            CollabAPI api = new CollabAPI(serverURL, userName);
            try
            {
                api.getLoginTicket(userPass);
            } catch {}

            // Create the dialog, pass it our API instance
            CollabAddIn.ReviewSelectDlg dlg = new CollabAddIn.ReviewSelectDlg(api);
            dlg.ShowDialog();
        }
        static void DoAPITest()
        {
            JsonResult result; // used repeatedly

            WL("Creating CollabAPI() instance");
            WL("Server URL: " + serverURL);
            WL("User Name: " + userName);
            CollabAPI api = new CollabAPI(serverURL, userName);

            try
            {
                WL("Calling getServerVersion()");
                WL("Result: " + api.getServerVersion());

                WL("Calling getServerBuild()");
                WL("Result: " + api.getServerBuild());

                WL("Calling getMinimumJavaClientBuild()");
                WL("Result: " + api.getMinimumJavaClientBuild());

                WL("Calling checkTicketValidity()");
                WL("Result: " + api.checkTicketValidity().ToString());

                WL("Calling getLoginTicket()");
                WL("Password: " + userPass);
                result = api.getLoginTicket(userPass);
                if (!result.isError())
                    WL("Received Login Ticket: " + result.GetResponse<SessionService.getLoginTicketResponse>().loginTicket);
                else
                    WL(result.GetErrorString());

                WL("Calling checkTicketValidity()");
                WL("Result: " + api.checkTicketValidity().ToString());
                
                WL("Calling clearLoginTicket()");
                api.clearLoginTicket();

                WL("Calling checkTicketValidity()");
                WL("Result: " + api.checkTicketValidity().ToString());

                WL("Calling getLoginTicket()");
                result = api.getLoginTicket(userPass);
                WL("Password: " + userPass);
                if (!result.isError())
                    WL("Received Login Ticket: " + result.GetResponse<SessionService.getLoginTicketResponse>().loginTicket);
                else
                    WL(result.GetErrorString());

                WL("Calling createReview()");
                result = api.createReview();

                // ========================================================================================================
                // This block creates 500 reviews in a single request
                // ========================================================================================================
#if false
                
                List<JsonCommand> requestList = new List<JsonCommand>();

                // Add the API authenticate command
                requestList.Add(new JsonCommand(new SessionService.authenticate()
                {
                    login = api.userName,
                    ticket = api.userTicket
                }));

                // Create tons of reviews!
                for (int i = 1; i < 500; i++)
                    requestList.Add(new JsonCommand(new ReviewService.createReview { creator = api.userName, title = "Test Review " + i.ToString() }));

                
                CollabServerRequest.Execute(api.serverURL, requestList);
                
#endif
                // ========================================================================================================
                // ========================================================================================================
                // ========================================================================================================

                if (!result.isError())
                    WL("New Review Id: " + result.GetResponse<ReviewService.createReviewResponse>().reviewId.ToString());
                else
                    WL(result.GetErrorString());

                WL("Calling createReview(\"API Test Review\")");
                result = api.createReview("API Test Review");
                if (!result.isError())
                    WL("New Review Id: " + result.GetResponse<ReviewService.createReviewResponse>().reviewId.ToString());
                else
                    WL(result.GetErrorString());

                WL("Calling getLoginTicket()");
                result = api.getLoginTicket(userPass);
                WL("Password: " + userPass);
                if (!result.isError())
                    WL("Received Login Ticket: " + result.GetResponse<SessionService.getLoginTicketResponse>().loginTicket);
                else
                    WL(result.GetErrorString());

                WL("Calling getSuggestedReviews()");
                result = api.getSuggestedReviews(UserService.SuggestionType.UPLOAD,"",5000);
                if (!result.isError())
                {
                    UserService.SuggestedReviewsResponse response = result.GetResponse<UserService.SuggestedReviewsResponse>();
                    WL("Received " + response.suggestedReviews.Count().ToString() + " results:");
                    foreach (UserService.SuggestedReview sug in response.suggestedReviews)
                    {
                        WL("     Review Id: " + sug.reviewId.ToString());
                        WL("     Description: " + sug.displayText);
                        WL("     Modified: " + sug.lastModified.ToString());
                        WL("     Has Changelist: " + sug.containsChangelist.ToString());
                        WL("");
                    }
                }
                else
                    WL(result.GetErrorString());
                

            }
            catch (Exception ex)
            {
                WL(ex.Message);
            }


            // We're done
            WL();
            WL("Press any key to exit.");
            Console.ReadKey(true);
        }
        static void WL (string line = "")
        {
            Console.WriteLine(line);
        }
    }
}
