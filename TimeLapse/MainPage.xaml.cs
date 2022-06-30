
using System.Security.Cryptography;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using uPLibrary.Networking.M2Mqtt;

namespace HelloWorldMAUI;

public partial class MainPage : ContentPage
{
	int count = 0;

    private const int BrokerPort = 8883;
    private const string Host = "a2o930rbhoed0u-ats.iot.us-east-1.amazonaws.com";

    MqttClient client = new MqttClient("broker.hivemq.com");

    public MainPage()
	{
		InitializeComponent();

        byte code = client.Connect(Guid.NewGuid().ToString());
    }

    private bool OnRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
           => true;

    private void OnPauseClicked(object sender, EventArgs e)
	{
        if (this.pauseBtn.Text == "Pause")
            client.Publish("pauseClicked", Encoding.Default.GetBytes("Pause"));
        else
            client.Publish("resumeClicked", Encoding.Default.GetBytes("Resume")); 

        this.pauseBtn.Text = this.pauseBtn.Text == "Pause" ? "Resume" : "Pause";
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        client.Publish("cancelClicked", Encoding.Default.GetBytes("Cancel"));
        this.pauseBtn.Text = "Pause";
    }
}

