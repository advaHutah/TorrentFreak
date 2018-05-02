using AttributeProject;
using MediationServerSrevices;
using Newtonsoft.Json;
using objectClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace ClientApp
{
    /// <summary>
    /// Interaction logic for TorrentWindow.xaml
    /// </summary>
    public partial class TorrentWindow : Window
    {
        List<SearchResult> searchResultList = new List<SearchResult>();
        List<DownloadInfo> downloadsInfoList = new List<DownloadInfo>();
        List<UploadInfo> uploadsInfoList = new List<UploadInfo>();
        string serverIp;

        private string downlodPathFolder;
        private string uploadPathFolder;
        private UserInfo user;
        private ClientFileUpload uploadListener;
     
        public TorrentWindow(string downlodPathFolder, string uploadPathFolder, UserInfo user,string ServerIp)
        {
            InitializeComponent();
            this.downlodPathFolder = downlodPathFolder;
            this.uploadPathFolder = uploadPathFolder;
            this.user = user;
            searchResultsview.ItemsSource = searchResultList;
            DownloadsView.ItemsSource = downloadsInfoList;
            UploadsView.ItemsSource = uploadsInfoList;
            updateUploadView();
            updateDownloadsView();
            this.serverIp = ServerIp;

            Task.Factory.StartNew(() =>
            {
                uploadListener = new ClientFileUpload();
                uploadListener.uploadEventSchedule += updateUploadView;
                uploadListener.StartUploadsListener(uploadPathFolder,user.PortIn);
            });
        }

        private void updateUploadView()
        {
            foreach (KeyValuePair<string, long> file in user.FileDict)
            {
                uploadsInfoList.Add(new UploadInfo(file.Key, file.Value, ""));
            }
            UploadsView.Items.Refresh();
        }

        private void updateDownloadsView()
        {
            FileInfo[] myFiles = new DirectoryInfo(downlodPathFolder).GetFiles();
            foreach (FileInfo info in myFiles)
            {
                downloadsInfoList.Add(new DownloadInfo(info.Name, info.Length, 0, "", ""));
            }
            DownloadsView.Items.Refresh();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            String fileName = SearchtextBox.Text;
            if (fileName == "")
            {
                System.Windows.MessageBox.Show("Enter a file name!");
                return;
            }
            else
            {
                //fins list of files with the name 
                EndpointAddress ep = new EndpointAddress("http://" + serverIp + ":8090/wcf");
                ChannelFactory<IFileService> factory = new ChannelFactory<IFileService>(new BasicHttpBinding(), ep);
                IFileService proxy = factory.CreateChannel();
                try
                {
                    var t = proxy.ClientFileRequestAsync((JsonConvert.SerializeObject(fileName)));
                    var r = t.Result;
                    List<SearchResult> rr = JsonConvert.DeserializeObject<List<SearchResult>>(r);
                    searchResultList.Clear();
                    foreach (SearchResult l in rr)
                    {
                        searchResultList.Add(l);
                    }
                    //show list in result window
                    searchResultsview.Items.Refresh();
                }
                catch
                {
                    System.Windows.MessageBox.Show(fileName + "doesn't exist");
                    return;
                }

            }

        }

        private void searchResultsview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Are you sure you want to download this file", "Download File", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //no
            }
            else
            {
                //yes
                SearchResult selection = (SearchResult)searchResultsview.SelectedItem;
                downloadFile(selection.SourcesIps, selection.FileName, (int)selection.FileSize);
            }
        }

        private void downloadFile(List<string> sourceIpsList, string filename, int size)
        {
            Task.Factory.StartNew(() =>
            {
                ClientFileRequest download = new ClientFileRequest(filename, sourceIpsList, size, this.downlodPathFolder, this.uploadPathFolder,user.PortIn);
                download.downloadEventSchedule += updateDownloadView;
                download.RequsetFileFromPeers();
            });
            if(File.Exists(this.downlodPathFolder + "\\" + filename))
            {
                UpdateMeAsFileSource(filename, size);
            }
        }
        private void updateUploadView(UploadInfo info, bool isFinished)
        {
            if (!isFinished)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    foreach (UploadInfo up in uploadsInfoList)
                    {
                        if (up.FileName.Equals(info.FileName))
                            up.Status = info.Status;
                    }
                    UploadsView.Items.Refresh();
                }));
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    foreach (UploadInfo up in uploadsInfoList)
                    {
                        if (up.FileName.Equals(info.FileName))
                            up.Status = info.Status;
                    }
                    UploadsView.Items.Refresh();
                }));
            }
        }


        private void updateDownloadView(DownloadInfo info, bool isFinished)
        {
            if (!isFinished)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    downloadsInfoList.Add(info);
                    DownloadsView.Items.Refresh();
                }));
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {

                    DownloadsView.Items.Refresh();
                }));
            }
        }

        private void SignOutFromServer()
        {
            EndpointAddress ep = new EndpointAddress(
                "http://" + serverIp + ":8091/wcf");
            ChannelFactory<ISignOutService> factory = new ChannelFactory<ISignOutService>(new BasicHttpBinding(), ep);
            ISignOutService proxy = factory.CreateChannel();
            proxy.ClientSignOutAsync((JsonConvert.SerializeObject(user)));

        }

        private void UpdateMeAsFileSource(string filename,int filesize)
        {
            UpdateSourceInfo info = new UpdateSourceInfo(user.UserName, filename, user.Ip, filesize);
            EndpointAddress ep = new EndpointAddress(
                "http://" + serverIp + ":8090/wcf");
            ChannelFactory<IFileService> factory = new ChannelFactory<IFileService>(new BasicHttpBinding(), ep);
            IFileService proxy = factory.CreateChannel();
            proxy.UpdateFileSourceAsync((JsonConvert.SerializeObject(info)));
            
        }

     



        private void button_Duck_Click(object sender, RoutedEventArgs e)
        {
            string[] files = Directory.GetFiles(downlodPathFolder, "*.dll", SearchOption.AllDirectories);
            foreach (string filename in files)
            {
                Assembly assembly = Assembly.LoadFile(filename);
                Type[] allTypes = assembly.GetTypes();
                foreach (Type t in allTypes)
                {
                    IdAttribute idAtt = (IdAttribute)Attribute.GetCustomAttribute(t, typeof(IdAttribute));
                    if (idAtt == null)
                    {
                    }
                    else
                    {
                        if (idAtt.Id.Equals(2))
                        {
                            MethodInfo m = t.GetMethod("quack");
                            object obj = Activator.CreateInstance(t);
                            object[] objects = { };
                            m.Invoke(obj, objects);

                        }
                    }
                }
            }

        }

        private void button_Car_Click(object sender, RoutedEventArgs e)
        {
            string[] files = Directory.GetFiles(downlodPathFolder, "*.dll", SearchOption.AllDirectories);
            foreach (string filename in files)
            {
                Assembly assembly = Assembly.LoadFile(filename);
                Type[] allTypes = assembly.GetTypes();
                foreach (Type t in allTypes)
                {
                    IdAttribute idAtt = (IdAttribute)Attribute.GetCustomAttribute(t, typeof(IdAttribute));
                    if (idAtt == null)
                    {
                    }
                    else
                    {
                        if (idAtt.Id.Equals(1))
                        {
                            MethodInfo m = t.GetMethod("drive");
                            object obj = Activator.CreateInstance(t);
                            object[] objects = { };
                            m.Invoke(obj, objects);

                        }
                    }
                }
            }
        
    }

        private void Window_Closed(object sender, EventArgs e)
        {
            SignOutFromServer();
            Close();
        }

        //When you close the client, it will sign out. 
        //Don't forget to dispose all resources, on closing Client, Server.
        private void signout_button_Click(object sender, RoutedEventArgs e)
        {
            SignOutFromServer();
            new MainWindow().Show();
        }
    }

}
