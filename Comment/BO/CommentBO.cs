using Radyn.Comments.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Utility;
using System;
using System.Collections.Generic;

namespace Radyn.Comments.BO
{
    public class CommentBO : BusinessBase<Comment>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Comment obj)
        {

            Guid id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            obj.SendDate = DateTime.Now.ShamsiDate();
            obj.SendTime = DateTime.Now.GetTime();
            return base.Insert(connectionHandler, obj);
        }


        public override bool Delete(IConnectionHandler connectionHandler, Comment obj)
        {
            List<Comment> children = base.Where(connectionHandler, x => x.ParentCommentId == obj.Id);
            foreach (var comment in children)
            {
                this.Delete(connectionHandler, comment);
            }
            return base.Delete(connectionHandler, obj);
        }





    }
}
