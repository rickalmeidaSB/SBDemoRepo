using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
#pragma warning disable 0649 // Expected warnings in JSON classes
namespace SmartBear.Collab
{
    public class ReviewService
    {
        public class addFiles
        {
            public int reviewId;
            public string zipName;
            public List<changelist> changelists;
        }

        public class changelist
        {
            public commitInfo commitInfo;
            public List<version> versions;
        }

        public class version
        {
            public string md5;
            //public string zipName; // not allowed
            public string localPath;
            public string scmPath;
            public string action;
            public string source;
            public string baseVersion;
        }

        public class commitInfo
        {
            public string scmId;
            public string author;
            public string comment;
            public string date;
            public bool local;
            public string hostGuid;
        }

        public class createReview
        {
            public string creator;
            public string title;
            // Skipping un-used items here to be terse
        }

        public class createReviewResponse
        {
            public int reviewId;
        }
    }
}
#pragma warning restore 0649 // Expected warnings in JSON classes