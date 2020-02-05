using System;
using System.Collections.Generic;
using Radyn.Framework;

namespace Radyn.Comments.DataStructure
{
    [Serializable]
    [Schema("Comment")]
    public sealed class Comment : DataStructureBase<Comment>
    {
        private Guid _id;
        [Key(false)]
        [DbType("uniqueidentifier")]
        public Guid Id
        {
            get { return _id; }
            set { base.SetPropertyValue("Id", value); }
        }

      

        private Guid? _senderId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? SenderId
        {
            get
            {
                return this._senderId;
            }
            set
            {
                base.SetPropertyValue("SenderId", value);
                if (value.HasValue)
                    this.SenderEnterpriseNode = new EnterpriseNode.DataStructure.EnterpriseNode { Id = value.Value };
                else
                    this.SenderEnterpriseNode = null;
            }
        }
        [Assosiation(PropName = "SenderId")]
        public EnterpriseNode.DataStructure.EnterpriseNode SenderEnterpriseNode { get; set; }

        private string _senderName;
        [IsNullable]
        [DbType("nvarchar(60)")]
        public string SenderName
        {
            get { return _senderName; }
            set { base.SetPropertyValue("SenderName", value); }
        }

        private string _content;
        [DbType("nvarchar(4000)")]
        public string Content
        {
            get { return _content; }
            set { base.SetPropertyValue("Content", value); }
        }

        private string _subject;
        [IsNullable]
        [DbType("nvarchar(1000)")]
        public string Subject
        {
            get { return _subject; }
            set { base.SetPropertyValue("Subject", value); }
        }

        private string _senderEmail;
        [IsNullable]
        [DbType("nvarchar(500)")]
        public string SenderEmail
        {
            get { return _senderEmail; }
            set { base.SetPropertyValue("SenderEmail", value); }
        }

        private string _sendDate;
        [DbType("char(10)")]
        public string SendDate
        {
            get { return _sendDate; }
            set { base.SetPropertyValue("SendDate", value); }
        }

        private string _sendTime;
        [DbType("char(5)")]
        public string SendTime { get; set; }

        private Guid? _parentCommentId;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? ParentCommentId
        {
            get
            {
                return this._parentCommentId;
            }
            set
            {
                base.SetPropertyValue("ParentCommentId", value);
                if (value.HasValue)
                    this.ParentComment = new Comment { Id = value.Value };
                else
                    this.ParentComment = null;
            }
        }
        [Assosiation(PropName = "ParentCommentId")]
        public Comment ParentComment { get; set; }

        private bool _approved;
        [DbType("bit")]
        public bool Approved
        {
            get { return _approved; }
            set { base.SetPropertyValue("Approved", value); }
        }

        private Guid? _approver;
        [IsNullable]
        [DbType("uniqueidentifier")]
        public Guid? Approver
        {
            get
            {
                return this._approver;
            }
            set
            {
                base.SetPropertyValue("Approver", value);
                if (value.HasValue)
                    this.ApproverEnterpriseNode = new EnterpriseNode.DataStructure.EnterpriseNode { Id = value.Value };
                else
                    this.ApproverEnterpriseNode = null;
            }
        }
        [Assosiation(PropName = "Approver")]
        public EnterpriseNode.DataStructure.EnterpriseNode ApproverEnterpriseNode { get; set; }


        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public List<Comment> Children { get; set; }

        [DisableAction(DisableInsert = true, DisableUpdate = true, DiableSelect = true)]
        public override string DescriptionField
        {
            get { return this.SenderName; }
        }
    }

}