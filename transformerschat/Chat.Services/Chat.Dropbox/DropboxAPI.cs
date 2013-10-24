using Spring.IO;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using Spring.Social.OAuth1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Dropbox
{
    public class DropboxAPI
    {
        private const string DropboxAppKey = "0k6i999zm014zpp";
        private const string DropboxAppSecret = "5lo512nq4u63tcv";
        private const string first = "86ag93trf42ikpvy";
        private const string second = "rud20p9zgkqmm37";

        //private const string OAuthTokenFileName = "OAuthTokenFileName.txt";

        public static string GetUrl(string path)
        {
            DropboxServiceProvider dropboxServiceProvider =
                new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

            //// Authenticate the application (if not authenticated) and load the OAuth token
            //if (!File.Exists(OAuthTokenFileName))
            //{
            //    AuthorizeAppOAuth(dropboxServiceProvider);
            //}

            //OAuthToken oauthAccessToken = LoadOAuthToken();

            // Login in Dropbox
            IDropbox dropbox = dropboxServiceProvider.GetApi(first, second);

            // Display user name (from his profile)
            DropboxProfile profile = dropbox.GetUserProfileAsync().Result;

            // Create new folder
            string newFolderName = "New_Folder_" + DateTime.Now.Ticks;
            Entry createFolderEntry = dropbox.CreateFolderAsync(newFolderName).Result;

            // Upload a file
            string extension = path.Substring(path.LastIndexOf('.'));

            Entry uploadFileEntry = dropbox.UploadFileAsync(
                new FileResource(path),
                "/" + newFolderName + "/" + newFolderName + extension).Result;

            //Upload a pic
            //Entry uploadDefaultAvatar = dropbox.UploadFileAsync(
            //    new FileResource("../../default-avatar.jpg"), "/" + newFolderName + "/default-avatar.jpg").Result;

            // Share a file
            DropboxLink sharedUrl = dropbox.GetMediaLinkAsync(uploadFileEntry.Path).Result;
            Process.Start(sharedUrl.Url);

            return sharedUrl.Url;
        }

        private static OAuthToken LoadOAuthToken()
        {
            //string[] lines = File.ReadAllLines(OAuthTokenFileName);
            OAuthToken oauthAccessToken = new OAuthToken(first, second);
            return oauthAccessToken;
        }

        //private static void AuthorizeAppOAuth(DropboxServiceProvider dropboxServiceProvider)
        //{
        //    // Authorization without callback url
        //    OAuthToken oauthToken = dropboxServiceProvider.OAuthOperations.FetchRequestTokenAsync(null, null).Result;

        //    OAuth1Parameters parameters = new OAuth1Parameters();
        //    string authenticateUrl = dropboxServiceProvider.OAuthOperations.BuildAuthorizeUrl(
        //        oauthToken.Value, parameters);
        //    Process.Start(authenticateUrl);

        //    AuthorizedRequestToken requestToken = new AuthorizedRequestToken(oauthToken, null);
        //    OAuthToken oauthAccessToken =
        //        dropboxServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;

        //    string[] oauthData = new string[] { oauthAccessToken.Value, oauthAccessToken.Secret };
        //    File.WriteAllLines(OAuthTokenFileName, oauthData);
        //}
    }
}
