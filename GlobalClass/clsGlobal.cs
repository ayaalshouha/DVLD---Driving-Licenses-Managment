using DVLD_Buissness;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD___Driving_Licenses_Managment
{
    public class Logger
    {
        public delegate bool LogAction (clsUser loggedInUser);
        private LogAction _LogAction; 

        public Logger(LogAction Action)
        {
            _LogAction = Action;
        }

        public bool Log(clsUser loggedInUser)
        {
            return _LogAction(loggedInUser);
        }
    }

    internal class clsGlobal
    {
        public static clsUser CurrentUser;
       
        //save the last username and password to a file 
        public static bool RememberUsernameAndPassword(clsUser user = null)
        {
            string username, password;

            if (user == null)
            {
                username = "";
                password = "";

            }
            else
            {
                username = user.username; 
                password = user.password;
            }


            try
            {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();
                string filePath = currentDirectory + "\\login.txt";

                if (username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }

                string DataLine = username + "#//#" + password;
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(DataLine);
                    return true; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return false;
        }

        //get the stored username and password
        public static bool getStoredUsernameAndPassword(ref string Username, ref string Password)
        {
            try
            {
                //get current directory 
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                //filePath 
                string FilePath = CurrentDirectory + "\\login.txt";

                if (File.Exists(FilePath))
                {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string line;

                        while((line = reader.ReadLine()) != null) 
                        {
                            Console.WriteLine(line); 
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true; 
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message); 
                
            }
            return false; 
        }

        //validate a number 
        public static bool ValidateInteger(string Number)
        {
            var pattern = "^[0-9].$";
            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }
        public static bool ValidateFloat(string Number)
        {
            var pattern = @"^[0-9]*(?:\.[0-9]*)?$";
            var regex = new Regex(pattern);
            return regex.IsMatch(Number);
        }
        public static bool isNumber(string Number)
        {
            return ValidateFloat(Number) || ValidateInteger(Number); 
        }
        public static bool ValidateEmail(string email)
        {
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            var regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }
            return true;
        }
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }
        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            FileInfo fileinfo = new FileInfo(sourceFile);
            string extn = fileinfo.Extension;
            return GenerateGUID() + extn;

        }
        public static bool CopyImageToProjectFolder(ref string SourceFile)
        {
            string DestinationFolder = @"C:\DVLD - Driving Licenses Managment-People-Images\"; 

            if (!CreateFolderIfDoesNotExist(DestinationFolder))
                return false;



            string DestinationFile = DestinationFolder + ReplaceFileNameWithGUID(SourceFile);
            try
            {
                File.Copy(SourceFile, DestinationFile, true);

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SourceFile = DestinationFile; 
            return true;
        }
    }

}
