using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace PaynodeFTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FtpClientController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IList<string>> Index()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://151.217.46.28/mirror/iso/latest");
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential ("anonymous","foo@bar.net");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var result = new List<string>(reader.ReadToEnd().Split("\r\n"));
            result.RemoveAll(x => x.Length == 0);

            reader.Close();
            response.Close();

            return result;
        }

        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }
    }
}
