namespace PBR_Material_Maker
{
    internal class ImageToken
    {
        public string mimeType = "image/png";
        public string name = "spaghetti";
        public string uri;

        public ImageToken(string uri)
        {
            this.uri = "." + uri;
        }
    }
}
