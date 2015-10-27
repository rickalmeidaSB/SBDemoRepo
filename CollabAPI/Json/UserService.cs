using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
#pragma warning disable 0649 // Expected warnings in JSON classes
namespace SmartBear.Collab
{
    public class UserService
    {
        public class getActionItems
        {
            // no arguments needed, runs in context of current user
        }

        public class getActionItemsResponse
        {
            public List<ActionItem> actionItems;
        }

        public class ActionItem
        {
            public string text;
            public string nextActionText;
        }

        /* not currently used
        public class findUserByGuid
        {
            public string guid;
        }

        public class findUserByLogin
        {
            public string login;
        }
        */

        public enum SuggestionType
        {
            UPLOAD,
            COMMIT
        }

        public class getSuggestedReviews
        {
            public string changelistId;
            public SuggestionType suggestionType;
            public int maxResults;
        }

        public class SuggestedReviewsResponse
        {
            public List<SuggestedReview> suggestedReviews;
        }

        public class SuggestedReview
        {
            // This needs getters and setters because we use reviewId with lync
            public int reviewId { get; set; }
            public bool containsChangelist;
            public DateTime lastModified;

            // This needs getters and setters because we use displayText with lync
            public string displayText { get; set; }
        }
    }
}
#pragma warning restore 0649 // Expected warnings in JSON classes