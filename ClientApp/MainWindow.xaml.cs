using MediationServerSrevices;
using Newtonsoft.Json;
using objectClassLibrary;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Xml;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string uploadDirPath;
        private string downloadDirPath;
        private UserInfo user;
        private string ServerIp;

        public MainWindow()
        {

            try
            {
                InitializeComponent();
                ServerIp = System.IO.File.ReadAllText(@"ServerIp.txt");
                Hide();
            
                //start the client, check for a valid configuration file. 
                //If there is a problem – a message will appear.
                user = new UserInfo();

                // if config file exist
                if (File.Exists(Properties.Settings.Default.XML_FILE_NAME))
                {
                    getDetailsFromXmlFile();
                    if (!Directory.Exists(uploadDirPath))
                    {
                        Directory.CreateDirectory(uploadDirPath);
                    }
                    if (!Directory.Exists(downloadDirPath))
                    {
                        Directory.CreateDirectory(downloadDirPath);
                    }
                    user.fillMyFiles(uploadDirPath);

                    if (SignInToServer(user))
                        // If the Configuration file is OK, open a socket and wait for incoming request.
                        new TorrentWindow(downloadDirPath, uploadDirPath, user, ServerIp).Show();
                    else
                    {
                        MessageBox.Show("Connection to server failed! please enter details again");
                        Show();
                    }
                }
                else
                {
                    //if the config does not exist  
                    Show();
                }
            }
            catch (Exception ex)
            {
                Show();
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private bool SignInToServer(UserInfo user)
        {
            EndpointAddress ep = new EndpointAddress(
                "http://"+ServerIp+":8089/wcf");
            ChannelFactory<ISignInService> factory = new ChannelFactory<ISignInService>(new BasicHttpBinding(), ep);
            ISignInService proxy = factory.CreateChannel();
            var t = proxy.ClientSignInAsync((JsonConvert.SerializeObject(user)));
            return t.Result;
        }

        private void up_dir_btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog uploadFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            uploadFolderDialog.Description = "Select Your Upload Folder Path";
            if (uploadFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                uploadDirPath = uploadFolderDialog.SelectedPath;
                upload_text.Text = uploadFolderDialog.SelectedPath;
            }
        }

        private void down_dir_btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog downloadFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            downloadFolderDialog.Description = "Select Your Download Folder Path";
            if (downloadFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                downloadDirPath = downloadFolderDialog.SelectedPath;
                download_text.Text = downloadFolderDialog.SelectedPath;
            }
        }

        private void XMLconfigCreate()
        {
            XmlWriterSettings xmlSetings = new XmlWriterSettings();
            xmlSetings.Indent = true;
            xmlSetings.IndentChars = "\t";
            using (XmlWriter xmlWriter = XmlWriter.Create(Properties.Settings.Default.XML_FILE_NAME))
            {

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Client");

                xmlWriter.WriteStartElement("UserName");
                xmlWriter.WriteString(username_text.Text);
                xmlWriter.WriteEndElement();//UserName

                xmlWriter.WriteStartElement("Password");
                xmlWriter.WriteString(password_text.Text);
                xmlWriter.WriteEndElement();//Password

                xmlWriter.WriteStartElement("Ip");
                xmlWriter.WriteString(getIpAddress());
                xmlWriter.WriteEndElement();//Ip

                xmlWriter.WriteStartElement("UploadPath");
                xmlWriter.WriteString(uploadDirPath);
                xmlWriter.WriteEndElement();//UploadFolder

                xmlWriter.WriteStartElement("DownloadPath");
                xmlWriter.WriteString(downloadDirPath);
                xmlWriter.WriteEndElement();//DownloadFolder

                xmlWriter.WriteStartElement("PortIn");
                xmlWriter.WriteString(port_in_text.Text);
                xmlWriter.WriteEndElement();//PortIn

                xmlWriter.WriteStartElement("PortOut");
                xmlWriter.WriteString(port_out_text.Text);
                xmlWriter.WriteEndElement();//PortOut

                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
        }

        private string getIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "";
        }



        private void getDetailsFromXmlFile()
        {

            XmlTextReader xmlReader = new XmlTextReader(Properties.Settings.Default
                .XML_FILE_NAME);

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    switch (xmlReader.Name)
                    {
                        case "UserName":
                            xmlReader.Read();
                            user.UserName = xmlReader.Value;
                            break;
                        case "Password":
                            xmlReader.Read();
                            user.Password = xmlReader.Value;
                            break;
                        case "Ip":
                            xmlReader.Read();
                            user.Ip = xmlReader.Value;
                            break;
                        case "UploadPath":
                            xmlReader.Read();
                            uploadDirPath = xmlReader.Value;
                            break;
                        case "DownloadPath":
                            xmlReader.Read();
                            downloadDirPath = xmlReader.Value;
                            break;
                        case "PortIn":
                            xmlReader.Read();
                            user.PortIn = int.Parse(xmlReader.Value);
                            break;
                        case "PortOut":
                            xmlReader.Read();
                            user.PortOut = int.Parse(xmlReader.Value);
                            break;
                        default:
                            break;
                    }
                }
            }
            xmlReader.Close();
            user.Ip = getIpAddress();
        }


        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            if (!checkMainWindowValidtion())
                return;
            XMLconfigCreate();
            getDetailsFromXmlFile();
            if (SignInToServer(user))
            {
                TorrentWindow t = new TorrentWindow(downloadDirPath, uploadDirPath, user,ServerIp);

                t.Show();
                Close();
            }
            else
                MessageBox.Show("Connection to server failed! password/user name incorrect");
        }



        private bool checkMainWindowValidtion()
        {
                bool valid = true;
            //port in
            int portIn = int.Parse(port_in_text.Text);
            int portOut = int.Parse(port_out_text.Text);
            if (portIn < 1023 || portIn > 65535)
                {
                    MessageBox.Show("PortIn must be number between 0-65535");
                    port_in_text.Text = "";
                    valid = false;
                }
                //port out
                else if (portOut < 1023 || portOut > 65535)
                {
                    MessageBox.Show("Port out must be number between 0-65535");
                    port_in_text.Text = "";
                    valid = false;

                }
                //Upload Directory
                else if (!System.IO.Directory.Exists(upload_text.Text))
                {
                    MessageBox.Show("Upload Directory path is not valid");
                    valid = false;

                }
                //Download Directory
                else if (!System.IO.Directory.Exists(download_text.Text))
                {
                    MessageBox.Show("Download Directory path is not valid");
                    valid = false;

                }
            return valid; 
        }
    }
        
}
