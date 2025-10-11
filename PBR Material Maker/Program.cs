using Newtonsoft.Json;

using System;
using System.IO;
using System.Windows.Forms;

namespace PBR_Material_Maker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static string GetProgramConfigFilePath()
        {
            string exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            return Path.Combine(exeDir, "ProgramConfig.json");
        }


        static ProgramConfig _programConfig = null;

        public static ProgramConfig ProgramConfig
        {
            get
            {


                if (_programConfig == null)
                {
                    string configFilePath = GetProgramConfigFilePath();
                    if (File.Exists(configFilePath))
                    {
                        try
                        {
                            _programConfig = JsonConvert.DeserializeObject<ProgramConfig>(File.ReadAllText(configFilePath));
                        }
                        catch (Exception)
                        {
                            // Just swallow this, someones probably messed with the file.
                        }
                    }

                    if (_programConfig == null)
                    {
                        _programConfig = new ProgramConfig();
                    }


                }
                return _programConfig;
            }

        }

        public static void SaveProgramConfig()
        {
            var cfg = ProgramConfig;
            string json = JsonConvert.SerializeObject(cfg);
            File.WriteAllText(GetProgramConfigFilePath(), json);
        }




    }
}
