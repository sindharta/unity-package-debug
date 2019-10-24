using System;
using System.Net;

namespace org.shin.package_debug {

    public static class NetUtility {

    static void DownloadSync(string uri, string destPath, Action<string> onSuccess, Action<WebException> onFail) {

        WebClient client = new WebClient();
        try {
            client.DownloadFile(new System.Uri(uri), destPath);
            onSuccess(destPath);
        } catch (WebException e) {
            onFail(e);
        }
    }

//---------------------------------------------------------------------------------------------------------------------

}

} //namespace