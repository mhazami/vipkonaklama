using Radyn.Comments.Facade;


namespace Radyn.Comments
{
    public class CommentsComponent
    {
        private CommentsComponent()
        {

        }

        private static CommentsComponent _instance;
        public static CommentsComponent Instance
        {
            get { return _instance ?? (_instance = new CommentsComponent()); }
        }


        public ICommentFacade CommentFacade
        {
            get
            {
                return new CommentFacade();
            }
        }


       
    }
}
