using System;
using System.Web.UI;
using Sitecore.Diagnostics;
using Control = Sitecore.Web.UI.HtmlControls.Control;

namespace Fishtank.SharedSource.CustomFields.Fields
{
    public class UnboundDroplist : Control
    {
        public UnboundDroplist()
        {
            Class = "scContentControl";
            Activation = true;
        }

        public string ItemId { get; set; }

        public string Source { get; set; }

        public bool HasPostData { get; set; }

        protected override void DoRender(HtmlTextWriter output)
        {
            string error = null;

            output.Write("<select" + GetControlAttributes() + ">");
            output.Write("<option value = \"\"></option>");

            if (String.IsNullOrEmpty(Source))
            {
                error = "No source list specified for field";
            }
            else
            {
                bool valueFound = String.IsNullOrEmpty(Value);
                foreach (string s in Source.Split('|'))
                {
                    valueFound = valueFound || s == Value;

                    output.Write(String.Format(@"<option value=""{0}"" {1}>{0}</option>", s,
                                               Value == s ? " selected=\"selected\"" : String.Empty));
                }

                if (!valueFound)
                {
                    error = "Value not in the selection list.";
                }
            }
            if (error != null)
            {
                output.Write("<optgroup label=\"" + error + "\">");
                output.Write("<option value=\"" + Value + "\" selected=\"selected\">" + Value + "</option>");
                output.Write("</optgroup>");
            }

            output.Write("</select>");

            if (error != null)
            {
                output.Write("<div style=\"color:#999999;padding:2px 0px 0px 0px\">{0}</div>", error);
            }
        }

        protected override bool LoadPostData(string value)
        {
            HasPostData = true;

            if (value == null)
            {
                return false;
            }

            Log.Info(this + " : Field : " + GetViewStateString("Field"), this);
            Log.Info(this + " : FieldName : " + GetViewStateString("FieldName"), this);

            if (GetViewStateString("Value") != value)
            {
                SetModified();
            }

            SetViewStateString("Value", value);
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);

            if (!HasPostData)
            {
                LoadPostData(string.Empty);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnPreRender(e);
            ServerProperties["Value"] = ServerProperties["Value"];
        }

        private static void SetModified()
        {
            Sitecore.Context.ClientPage.Modified = true;
        }
    }
}