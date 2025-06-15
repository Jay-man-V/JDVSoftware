using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

using RestSharp;

using Foundation.Core;
namespace Foundation.Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private String WebApiServer => "https://localhost:44395/";
        private String HeartbeatService => $"{WebApiServer}api/Heartbeat";

        private Timer HeartbeatTimer { get; set; }

        private void TestFunctionButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                GetHeartbeat();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void StartHeartbeatButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (HeartbeatTimer != null)
                {
                    HeartbeatTimer.Stop();
                    HeartbeatTimer.Enabled = false;
                    HeartbeatTimer = null;
                }

                HeartbeatTimer = new Timer();
                HeartbeatTimer.Interval = (Int32)new TimeSpan(0, 0, 10).TotalMilliseconds;
                HeartbeatTimer.Tick += (o, args) =>
                {
                    Timer thisTimer = (Timer)o;

                    thisTimer.Stop();
                    GetHeartbeat();
                    thisTimer.Start();
                };

                HeartbeatTimer.Start();

                GetHeartbeat();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void GetHeartbeat()
        {
            // send GET request with RestSharp
            RestClient client = new RestClient(HeartbeatService);
            RestRequest request = new RestRequest(nameof(IHeartbeatController.GetHeartbeat), Method.Get);

            RestResponse<HeartbeatResult> response = client.Execute<HeartbeatResult>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }

            HeartbeatResult result = response.Data;

            Debug.WriteLine($"{result}");
        }
    }
}
