using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SharePoint.Client;
using SPFile = Microsoft.SharePoint.Client.File;

namespace AIDELInformationManagementSystem
{
    public sealed class SharePointConnectionManager
    {
        public static SharePointConnectionManager Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new SharePointConnectionManager();

                return m_instance;
            }
        }

        private SharePointConnectionManager()
        {
            SecureString securePassword = new SecureString();
            foreach (char c in m_password)
            { securePassword.AppendChar(c); }

            m_onlineCredentials = new SharePointOnlineCredentials(m_userName, securePassword);
        }

        private static SharePointConnectionManager m_instance = null;

        private readonly string m_siteUrl = "???";
        private readonly string m_docLibrary = "???";
        public string ClientSubFolder { get; } = "???";
        private readonly string m_userName = "???";
        private readonly string m_password = "???";

        private readonly SharePointOnlineCredentials m_onlineCredentials;

        public void UploadFile(string _filePath, bool _showSuccessMessage = false)
        {
            UploadFile(_filePath, _showSuccessMessage, ClientSubFolder);
        }
        private void UploadFile(string _filePath, bool _showSuccessMessage, string _clientSubFolder = "")
        {
            try
            {
                #region Insert the data
                using (ClientContext cContext = new ClientContext(m_siteUrl))
                {
                    cContext.Credentials = m_onlineCredentials;
                    Web web = cContext.Web;

                    FileCreationInformation newFile = new FileCreationInformation();
                    byte[] fileContent = System.IO.File.ReadAllBytes(_filePath);
                    newFile.ContentStream = new MemoryStream(fileContent);
                    newFile.Url = Path.GetFileName(_filePath);
                    newFile.Overwrite = true;
                    List documentLibrary = web.Lists.GetByTitle(m_docLibrary);
                    Folder clientfolder = null;
                    if (_clientSubFolder == "")
                        clientfolder = documentLibrary.RootFolder;
                    else
                    {
                        clientfolder = documentLibrary.RootFolder.Folders.Add(_clientSubFolder);
                        clientfolder.Update();
                    }
                    SPFile uploadFile = clientfolder.Files.Add(newFile);
                    cContext.Load(documentLibrary);
                    cContext.Load(uploadFile);
                    cContext.ExecuteQuery();
                    
                    if (_showSuccessMessage)
                        MessageBox.Show("The File has been uploaded" + Environment.NewLine + "FileUrl -->" + m_siteUrl + "/" + m_docLibrary + "/" + _clientSubFolder + "/" + Path.GetFileName(_filePath));
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred : " + ex.Message);
            }
        }

        public void UploadAllModifiedFiles()
        {
            DataContainer container = DataContainer.Instance;
            string trueString = true.ToString();
            string baseString = "CSVModified_";
            int baseStringLength = baseString.Length;
            var fileNameIds = container.SystemDataDictionary.Where(x => x.Key.Contains(baseString) && x.Value == trueString).Select(x => Convert.ToInt32(x.Key.Substring(baseStringLength, x.Key.Length - baseStringLength)));
            foreach (var fileNameId in fileNameIds)
            {
                string fileName = container.FileNames[fileNameId - 1];
                string filePath = CoreValues.ExecutableDirectoryPath + fileName;
                UploadFile(filePath);
            }

            MessageBox.Show(container.NumOfModifiedFiles().ToString() + " files have been updated in the server!");

            container.ResetAllFileModificationStatus();
        }

        public string DownloadFile(string _fileName, string _clientSubFolder = "")
        {
            try
            {
                #region Load the data
                using (ClientContext cContext = new ClientContext(m_siteUrl))
                {
                    cContext.Credentials = m_onlineCredentials;
                    Web web = cContext.Web;

                    List documentLibrary = web.Lists.GetByTitle(m_docLibrary);
                    Folder clientfolder = null;
                    if (_clientSubFolder == "")
                        clientfolder = documentLibrary.RootFolder;
                    else
                    {
                        clientfolder = documentLibrary.RootFolder.Folders.Add(_clientSubFolder);
                        clientfolder.Update();
                    }

                    var files = clientfolder.Files;
                    cContext.Load(files);
                    cContext.ExecuteQuery();

                    SPFile downloadedFile = files.First(x => x.Name == _fileName);

                    cContext.Load(downloadedFile);
                    cContext.ExecuteQuery();

                    string filePath_localSaveFile = CoreValues.ExecutableDirectoryPath + _fileName;

                    //Open file to copy loaded data. Create it if not exists.
                    FileStream fs = System.IO.File.OpenWrite(filePath_localSaveFile);
                    fs.SetLength(0);

                    //Copy loaded data to local save file.
                    ClientResult<Stream> crStream = downloadedFile.OpenBinaryStream();
                    cContext.ExecuteQuery();
                    if (crStream.Value != null)
                        crStream.Value.CopyTo(fs);

                    fs.Close();

                    return filePath_localSaveFile;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred : " + ex.Message
                    + "\nPlease check your internet connection and try again.");
                return null;
            }
        }

        public List<string> DownloadFiles(List<string> _fileNames, string _clientSubFolder = "")
        {
            List<string> filePaths = new List<string>();

            try
            {
                #region Load the data
                using (ClientContext cContext = new ClientContext(m_siteUrl))
                {
                    cContext.Credentials = m_onlineCredentials;
                    Web web = cContext.Web;

                    List documentLibrary = web.Lists.GetByTitle(m_docLibrary);
                    Folder clientfolder = null;
                    if (_clientSubFolder == "")
                        clientfolder = documentLibrary.RootFolder;
                    else
                    {
                        clientfolder = documentLibrary.RootFolder.Folders.Add(_clientSubFolder);
                        clientfolder.Update();
                    }

                    var files = clientfolder.Files;
                    cContext.Load(files);
                    cContext.ExecuteQuery();

                    foreach (var fileName in _fileNames)
                    {
                        SPFile downloadedFile = files.First(x => x.Name == fileName);

                        cContext.Load(downloadedFile);
                        cContext.ExecuteQuery();

                        string filePath_localSaveFile = CoreValues.ExecutableDirectoryPath + fileName;

                        //Open file to copy loaded data. Create it if not exists.
                        FileStream fs = System.IO.File.OpenWrite(filePath_localSaveFile);
                        fs.SetLength(0);

                        //Copy loaded data to local save file.
                        ClientResult<Stream> crStream = downloadedFile.OpenBinaryStream();
                        cContext.ExecuteQuery();
                        if (crStream.Value != null)
                            crStream.Value.CopyTo(fs);

                        fs.Close();

                        filePaths.Add(filePath_localSaveFile);
                    }

                    return filePaths;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred : " + ex.Message);
                return null;
            }
        }

        public async Task<List<string>> DownloadFiles(List<string> _fileNames, Label _lbl_loadingStatus, string _clientSubFolder = "")
        {
            List<string> filePaths = new List<string>();

            try
            {
                #region Load the data
                using (ClientContext cContext = new ClientContext(m_siteUrl))
                {
                    cContext.Credentials = m_onlineCredentials;
                    Web web = cContext.Web;

                    List documentLibrary = web.Lists.GetByTitle(m_docLibrary);
                    Folder clientfolder = null;
                    if (_clientSubFolder == "")
                        clientfolder = documentLibrary.RootFolder;
                    else
                    {
                        clientfolder = documentLibrary.RootFolder.Folders.Add(_clientSubFolder);
                        clientfolder.Update();
                    }

                    var files = clientfolder.Files;
                    cContext.Load(files);
                    cContext.ExecuteQuery();

                    await SaveFilesAndVisualizeLoadingStatus(cContext, files, _fileNames, filePaths, _lbl_loadingStatus);

                    return filePaths;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred : " + ex.Message);
                return null;
            }
        }

        private async Task SaveFilesAndVisualizeLoadingStatus(ClientContext _cContext, FileCollection _files, List<string> _fileNames, List<string> _filePaths, Label _lbl_loadingStatus)
        {
            try
            {
                string numOfFiles = _fileNames.Count.ToString();

                int count = 0;
                foreach (var fileName in _fileNames)
                {
                    await Task.Delay(1);

                    SPFile downloadedFile = _files.First(x => x.Name == fileName);

                    _cContext.Load(downloadedFile);
                    _cContext.ExecuteQuery();

                    string filePath_localSaveFile = CoreValues.ExecutableDirectoryPath + fileName;

                    //Open file to copy loaded data. Create it if not exists.
                    FileStream fs = System.IO.File.OpenWrite(filePath_localSaveFile);
                    fs.SetLength(0);

                    //Copy loaded data to local save file.
                    ClientResult<Stream> crStream = downloadedFile.OpenBinaryStream();
                    _cContext.ExecuteQuery();
                    if (crStream.Value != null)
                        crStream.Value.CopyTo(fs);

                    fs.Close();

                    _filePaths.Add(filePath_localSaveFile);

                    _lbl_loadingStatus.Text = "Loading data from server... (" + (++count).ToString() + "/" + numOfFiles + " file(s) loaded.)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
