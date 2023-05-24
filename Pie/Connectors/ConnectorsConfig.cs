using Pie.Connectors.Connector1c;

namespace Pie.Connectors
{
    public class ConnectorsConfig
    {
        public const string Section = "Connectors";


        public Client1cConfig? Client1cConfig { get; set; }
        public bool UseProxy { get; set; }
    }
}
