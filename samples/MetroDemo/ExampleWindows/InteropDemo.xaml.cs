
using MiniUiAppCode;
using System.Runtime.InteropServices;

using System;
using System.ComponentModel;
//using CefSharp;
using System.Windows;
using CefSharp;
 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using CefSharp.Wpf;

namespace MetroDemo.ExampleWindows
{
    public partial class InteropDemo: IRequestHandler
    {
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);
        public string m_cookie = "";
        private string m_url = "http://127.0.0.1/rispweb/risphost/MiniMobileDeskTop.aspx";
        // private CefSharp.Wpf.ChromiumWebBrowser _view;
        private WebView _view;
        public InteropDemo()
        {
            InitializeComponent();
            string url = "http://127.0.0.1/rispweb/rispservice/ajaxSvrLogin.aspx";

            m_cookie = RealsunClientNet.m_CookieContainer.GetCookies(new System.Uri(url))[0].ToString();

            CEF.Initialize(new Settings { LogSeverity = LogSeverity.Disable, PackLoadingDisabled = true });

            BrowserSettings browserSetting = new BrowserSettings { ApplicationCacheDisabled = true, PageCacheDisabled = true };
            setCefCookie(m_cookie);
            _view = new WebView(string.Empty, browserSetting)
            {
                Address = m_url,
                RequestHandler = this,
                Background = Brushes.White
            };

            _view.RegisterJsObject("callbackObj", new CallbackObjectForJs());

            _view.LoadCompleted += _view_LoadCompleted;
            MainGrid.Children.Insert(0, _view);
            /*new  code */
            //var setting = new CefSharp.CefSettings();
            //BrowserSettings a = new BrowserSettings();
            //////CEF.Initialize(new Settings { LogSeverity = LogSeverity.Disable, PackLoadingDisabled = true });

            //////BrowserSettings browserSetting = new BrowserSettings { ApplicationCacheDisabled = true, PageCacheDisabled = true };

            //////_view = new WebView(string.Empty, browserSetting)
            //////{
            //////    Address = url,
            //////    RequestHandler = this,
            //////    Background = Brushes.White
            //////};

            //////_view.RegisterJsObject("callbackObj", new CallbackObjectForJs());

            //////_view.LoadCompleted += _view_LoadCompleted;

            //////MainGrid.Children.Insert(0, _view);
            //_view = new CefSharp.Wpf.ChromiumWebBrowser();


            //if (!Cef.IsInitialized)
            //{
            //    Cef.Initialize(setting, true, false);

            //}
            



            //_view.Loaded += _view_Loaded;
            //_view.Address = m_url;
            //MainGrid.Children.Insert(0, _view);
            ////_view.Load(m_url);
            ////CEF.Initialize(new Settings { LogSeverity = LogSeverity.Disable, PackLoadingDisabled = true });
            ///*old code */
            ////var setting = new CefSharp.CefSettings();


            ////if (!Cef.IsInitialized)
            ////{ CefSharp.Cef.Initialize(setting, true, false); }
            ////var webView = new CefSharp.Wpf.ChromiumWebBrowser();
            ////this.Content = webView;
            ////setCefCookie(m_cookie);
            ////webView.Address = m_url;

            ////setcookie(m_cookie);
            ////WebBrowser.Url = new System.Uri(m_url);




        }

        private void _view_LoadCompleted(object sender, LoadCompletedEventArgs url)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                maskLoading.Visibility = Visibility.Collapsed;
            }));
        }
 

       
        private void setcookie(string c)
        {
            string[] item = c.Split('=');
            if (item.Length == 2)
            {
                string name = item[0];
                string value = item[1];
                InternetSetCookie(m_url, name, value);

            }


        }
        private void setCefCookie(string c)
        {
            //var cookieManager = Cef.GetGlobalCookieManager();
            //cookieManager .VisitUrlCookies()
            //CefSharp.Cookie aCookie = new Cookie();
          
            string[] item = c.Split('=');
            if (item.Length == 2)
            {
                string name = item[0];
                string value = item[1];
                //InternetSetCookie(m_url, name, value);
                //      aCookie.Name = name;
                //    aCookie.Value = value;
                DateTime d = new DateTime(2020, 1, 1);
                CEF.SetCookie(m_url, "127.0.0.1", name, value, d);

            }
            //    cookieManager.SetCookieAsync(m_url, aCookie);
        }



        #region IRequestHandler
        public bool GetAuthCredentials(IWebBrowser browser, bool isProxy, string host, int port, string realm, string scheme, ref string username, ref string password)
        {
            return false;
        }

        public bool GetDownloadHandler(IWebBrowser browser, string mimeType, string fileName, long contentLength, ref IDownloadHandler handler)
        {
            return true;
        }

        public bool OnBeforeBrowse(IWebBrowser browser, IRequest request, NavigationType naigationvType, bool isRedirect)
        {
            return false;
        }

        public bool OnBeforeResourceLoad(IWebBrowser browser, IRequestResponse requestResponse)
        {
            return false;
        }

        public void OnResourceResponse(IWebBrowser browser, string url, int status, string statusText, string mimeType, WebHeaderCollection headers)
        {

        }
        #endregion

    }
    public class CallbackObjectForJs
    {
        public void showMessage(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
