using Newtonsoft.Json;

namespace PBR_Material_Maker
{
    public class ProgramConfig
    {
        [JsonProperty]
        public int WindowHeight = 940;
        
        [JsonProperty]
        public int WindowWidth = 1200;
    }
}
