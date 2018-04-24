
#region Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO; //Include this namespace
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
#endregion


public partial class Welcome : System.Web.UI.Page
{
    #region Variable
    private static ArrayList Files = new ArrayList();
    private static string upLoadedFiles = String.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Get Ip address
        string hostName = Dns.GetHostName();
        string ipAddess = Dns.GetHostByName(hostName).AddressList[0].ToString();
        lblHeader.Text = "Welcome IP Address:" + ipAddess + " Please complete this form and select files to upload";
        #endregion
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int folderCount = GetFolderNo();
        string folderName = FolderCreation();
        string strcon = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(strcon);
        SqlCommand cmd = new SqlCommand("sp_insert", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Company_Name", txtCompany.Text);
        cmd.Parameters.AddWithValue("@Contact_Name", txtContact.Text);
        cmd.Parameters.AddWithValue("@Phone_Number", txtContactNo.Text);
        cmd.Parameters.AddWithValue("@Contact_Email", txtEmail.Text);
        cmd.Parameters.AddWithValue("@IP_Address", txtIPAddress.Text);
        cmd.Parameters.AddWithValue("@Files", upLoadedFiles);
        //cmd.Parameters.AddWithValue("@FolderCreated", folderCount);
        cmd.Parameters.AddWithValue("@FolderName", folderName);
        con.Open();
        int k = cmd.ExecuteNonQuery();
        if (k != 0)
        {
            lblSuccess.ForeColor = System.Drawing.Color.CornflowerBlue;
        }
        con.Close();
        Sendmail();
        Clear();
    }
    private int GetFolderNo()
    {
        int folderCount=0;
        string strcon = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(strcon);
        SqlCommand cmd = new SqlCommand("GetFolderNo", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Contact_Name", txtContact.Text);
        //cmd.Parameters.AddWithValue("@FolderName", folderCount);

        SqlDataReader reader;
        con.Open();
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            folderCount = (int)reader.GetValue(0);

        }
        con.Close();
        return folderCount;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        txtCompany.Text = String.Empty;
        txtContactNo.Text = String.Empty;
        txtContact.Text = String.Empty;
        txtEmail.Text = String.Empty;
        txtIPAddress.Text = String.Empty;
        lblMess.Text = String.Empty;
        lstFile.Items.Clear();
        lblSuccess.Text = "Users files have been submitted successfully.";

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string tempPath = ConfigurationManager.AppSettings["tempPath"];

        try
        {
            if (fldUpload.HasFile)
            {
                int filecount = 0;
                if (fldUpload.PostedFile.ContentLength > 0)
                {
                    if (lstFile.Items.Contains(new ListItem(System.IO.Path.GetFileName(fldUpload.PostedFile.FileName))))
                    {
                        lblMess.Text = "File already in the ListBox";
                    }
                    else
                    {
                        filecount = fldUpload.PostedFiles.Count();
                        if (filecount <= 10)
                        {
                            foreach (HttpPostedFile postfiles in fldUpload.PostedFiles)
                            {

                                Files.Add(fldUpload);
                                lstFile.Items.Add(System.IO.Path.GetFileName(postfiles.FileName));
                                upLoadedFiles += postfiles.FileName + ";";
                                fldUpload.SaveAs(tempPath + "/" + postfiles.FileName);
                            }
                        }
                        lblMess.Text = "Add another file or click Upload to save them all";
                    }

                }

                else
                {
                    lblMess.Text = "File size cannot be 0";
                }
            }
            else
            {
                lblMess.Text = "Please select a file to add";
            }
        }

        catch (Exception ex)
        {
        }

    }


    protected void btnDel_Click(object sender, EventArgs e)
    {

        if (lstFile.Items.Count > 0)
        {

            if (lstFile.SelectedIndex < 0)
            {
                lblMess.Text = "Please select a file to remove";
            }

            else
            {
                Files.RemoveAt(lstFile.SelectedIndex);
                lstFile.Items.Remove(lstFile.SelectedItem.Text);
                lblMess.Text = "File removed";

            }
        }

    }

    private void CreateTextFile(string pathString)
    {
        string fileName = pathString + "/Info.txt";
        try
        {
            // Check if file already exists. If yes, delete it. 
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file 
            using (FileStream fs = File.Create(fileName))
            {
                // Add some text to file
                byte[] company_name = new UTF8Encoding(true).GetBytes("Company Name :" + txtCompany.Text + "\r\n");
                fs.Write(company_name, 0, company_name.Length);
                byte[] contact_name = new UTF8Encoding(true).GetBytes("Contact name :" + txtContact.Text + "\r\n");
                fs.Write(contact_name, 0, contact_name.Length);
                byte[] contact_no = new UTF8Encoding(true).GetBytes("Contact No. :" + txtContactNo.Text + "\r\n");
                fs.Write(contact_no, 0, contact_no.Length);
                byte[] contact_Email = new UTF8Encoding(true).GetBytes("Email :" + txtEmail.Text + "\r\n");
                fs.Write(contact_Email, 0, contact_Email.Length);
                byte[] IP_Address = new UTF8Encoding(true).GetBytes("IP Address :" + txtIPAddress.Text + "\r\n");
                fs.Write(IP_Address, 0, IP_Address.Length);
                byte[] Files = new UTF8Encoding(true).GetBytes("Uploaded File :\r\n");
                fs.Write(Files, 0, Files.Length);
                foreach (var objFile in upLoadedFiles.Split(';'))
                {
                    byte[] obj = new UTF8Encoding(true).GetBytes("\r\r" + objFile + "\r\n");
                    fs.Write(obj, 0, obj.Length);
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }

    private string FolderCreation()
    {
        string root = ConfigurationManager.AppSettings["root"];
        string pathString;
       int folderCount = GetFolderNo();
        if (folderCount == 0)
        {
          pathString = Path.Combine(root, txtContact.Text + "-" + DateTime.Now.ToString("yyyy-MM-dd"));
        }
        else
        {
            pathString = Path.Combine(root, txtContact.Text + "-" + DateTime.Now.ToString("yyyy-MM-dd")) + "(" + folderCount.ToString() + ")";
        }

        if (!Directory.Exists(pathString))
            Directory.CreateDirectory(pathString);
        CreateTextFile(pathString);
        MoveFiles(pathString);
        return pathString;
    }

    private void MoveFiles(string pathString)
    {

        string sourcePath = ConfigurationManager.AppSettings["tempPath"];
        List<string> lstFilesname = new List<string>();
        for (int i = 0; i < lstFile.Items.Count; i++)
            lstFilesname.Add(lstFile.Items[i].Text);

        foreach (var file in Directory.GetFiles(sourcePath, "*.*"))
        {
            string FileName = Path.GetFileName(file);
            if (lstFilesname.Contains(FileName))
                File.Move(file, Path.Combine(pathString, FileName));
        }
        foreach (var objfile in Directory.GetFiles(sourcePath, "*.*"))
        {
            File.Delete(Path.GetFileName(objfile));
        }
    }

    private void Sendmail()
    {
        try
        {
            MailMessage message = new MailMessage();
            MailAddress Sender = new MailAddress(ConfigurationManager.AppSettings["smtpUser"]);
            MailAddress receiver = new MailAddress(txtEmail.Text);
            SmtpClient smtp = new SmtpClient()
            {
                Host = ConfigurationManager.AppSettings["smtpServer"],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]),
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpUser"], ConfigurationManager.AppSettings["smtpPass"])

            };
            message.From = Sender;
            message.To.Add(receiver);
            message.Body = "";
            message.IsBodyHtml = true;
            smtp.Send(message);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

}