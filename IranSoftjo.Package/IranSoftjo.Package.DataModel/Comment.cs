//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IranSoftjo.Package.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int CommentID { get; set; }
        public int SubID { get; set; }
        public string CommentText { get; set; }
        public System.DateTime CommentDate { get; set; }
        public Nullable<int> ReplyCommentId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserSite { get; set; }
        public Nullable<byte> TypeComment { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> LikeComment { get; set; }
        public Nullable<long> DisLikeComment { get; set; }
    }
}
