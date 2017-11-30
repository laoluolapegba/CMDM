using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace CMdm.UI.Web.Helpers.UI
{
    public static class GravatarHelper
    {
        public static HtmlString GravatarImage(this HtmlHelper htmlHelper, string emailAddress, GravatarOptions options = null)
        {
            if (options == null)
                options = GravatarOptions.GetDefaults();

            var imgTag = new TagBuilder("img");

            emailAddress = string.IsNullOrEmpty(emailAddress) ? string.Empty : emailAddress.Trim().ToLower();

            // <-- adding support for CSS
            if (!string.IsNullOrEmpty(options.CssClass))
            {
                imgTag.AddCssClass(options.CssClass);
            }
            // adding support for CSS  -->

            imgTag.Attributes.Add("src",
                string.Format("http://www.gravatar.com/avatar/{0}?s={1}{2}{3}",
                    GetMd5Hash(emailAddress),
                    options.Size,
                    "&d=" + options.DefaultImageType,
                    "&r=" + options.RatingLevel
                    )
                );

            return new HtmlString(imgTag.ToString(TagRenderMode.SelfClosing));
        }

        // Source: http://msdn.microsoft.com/en-us/library/system.security.cryptography.md5.aspx
        private static string GetMd5Hash(string input)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public static string ImageOrDefault(this HtmlHelper helper, string filename)
        {
            var imagePath = uploadsDirectory + filename + ".png";
            var imageSrc = File.Exists(HttpContext.Current.Server.MapPath(imagePath))
                               ? imagePath : defaultImage;

            return imageSrc;
            //var absolute_path = Path.Combine(directoryXmlLivesIn, "..\images\image.jpg");
            //Path.GetFullPath((new Uri(absolute_path)).LocalPath);
        }

        private static string defaultImage = "/AdminLTE/dist/img/defaultavatar.png";
        //"~/assets/img/defaultavatar.png";
        private static string uploadsDirectory = "/AdminLTE/dist/img/";
    }
}