using Renci.SshNet;

namespace Pie.Admin.Api.Modules.Ssh
{
    interface ISshService
    {
        string SendCommand(SshConfig config, string command);
    }

    class SshService : ISshService
    {
        public string SendCommand(SshConfig config, string command)
        {
            string sshResult = string.Empty;
            using (SshClient sshClient = new SshClient(config.Host, config.UserName, config.Password))
            {
                sshClient.Connect();

                if (sshClient.IsConnected)
                {
                    SshCommand sshCommand = sshClient.CreateCommand(command);
                    sshResult = sshCommand.Execute();

                    Console.WriteLine($"SSH Command Result: {sshResult}");

                    sshClient.Disconnect();
                }
                else
                {
                    Console.WriteLine("SSH Connection failed.");
                }
            }
            return sshResult;
        }
    }
}
