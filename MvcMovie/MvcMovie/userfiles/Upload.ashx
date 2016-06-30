 <%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;

public class Upload : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        try
        {

            HttpPostedFile uploads = context.Request.Files["upload"];

            string CKEditorFuncNum = context.Request["CKEditorFuncNum"];

            string fn = System.IO.Path.GetFileName(uploads.FileName);
            string url = "";

            if(string.Compare(Path.GetExtension(fn),".jpg",true) == 0
                ||string.Compare(Path.GetExtension(fn),".png",true) == 0
                ||string.Compare(Path.GetExtension(fn),".gif",true) == 0
                ||string.Compare(Path.GetExtension(fn),".jpeg",true) == 0)
            { 
                fn = Path.GetFileNameWithoutExtension(fn)+"-"+DateTime.Now.ToString("yyyyMMddHHmmss")+Path.GetExtension(fn);
                uploads.SaveAs(context.Server.MapPath(".") + "\\images\\" + fn);
                url = "/userfiles/images/" + fn;
            }
            else
            {
                fn = Path.GetFileNameWithoutExtension(fn)+"-"+DateTime.Now.ToString("yyyyMMddHHmmss")+Path.GetExtension(fn);
                uploads.SaveAs(context.Server.MapPath(".") + "\\docs\\" + fn);
                url = "/userfiles/docs/" + fn;
            }
            context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\""+");</script>");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            try
            {
                string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
                string url = "Failed to upload file for: " + ex.ToString();
                context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\""+");</script>");
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex1)
            { }
        }
    }

    public bool IsReusable {

        get {

            return false;

        }

    }



}