using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;

namespace Radyn.WebApp.AppCode.Filter
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class SourceCodeFileAttribute : FilterAttribute, IResultFilter
    {
        public string Caption
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public string RazorFileName
        {
            get;
            set;
        }

        public SourceCodeFileAttribute()
        {
        }

        public SourceCodeFileAttribute(string caption, string filename)
        {
            Caption = caption;
            FileName = filename;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
            var codeFiles = filterContext.Controller.ViewData["codeFiles"] as Dictionary<string, string>;//.Get<Dictionary<string, string>>("codeFiles");
            Dictionary<string, string> reza = new Dictionary<string, string>( );
            reza.Add("0", "0");
            if (codeFiles == null)
                codeFiles = reza;
            var currentFileName = GetCurrentFileName(filterContext);
            var currentCaption = Caption;
            if (string.IsNullOrEmpty(currentCaption))
            {
                currentCaption = Path.GetFileName(currentFileName);
            }

            codeFiles[currentCaption] = currentFileName;
            codeFiles.Remove("0");
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // Do Nothing
        }

        private string GetCurrentFileName(ResultExecutingContext filterContext)
        {
            return string.IsNullOrEmpty(RazorFileName) ? FileName : RazorFileName;
        }
    }
}