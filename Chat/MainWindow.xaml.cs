using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Oggetti
        Socket socket = null;
        DispatcherTimer dtimer = null;
        Thread checkChat = null;
        public MainWindow()
        {
            //Inizializzazione oggetti

            // SOCKET:
            // AddressFamily    : Enum, InterNetwork permette di lavorare con IPv4.
            // SocketType       : Enum.Dgram (DataGram). socket UPD.
            // ProtocolType     : Enum, di protocollo da utilizzare.
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // IP:
            IPAddress local_address = IPAddress.Any;                                      // Mittente del messaggio
            IPEndPoint local_endpoint = new IPEndPoint(local_address.MapToIPv4(), 64000); // Creazione endpoint ip del mittente
            // I messaggi ora verranno ricevuti e inviata dalla porta 66000 (not registered port)

            //Bind dell'indirizzo ip sulla socket
            socket.Bind(local_endpoint);
            socket.Blocking = false;
            socket.EnableBroadcast = false;

            /*
            dtimer = new DispatcherTimer();
            dtimer.Tick += new EventHandler(aggiornamento_dTimer);
            dtimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            dtimer.Start();
            */

            

            CaricaContatti();
            InitializeComponent();

            checkChat = new Thread(new ThreadStart (aggiornamento_chat));
            checkChat.Start();
        }

        private void aggiornamento_chat()
        {
            while (true)
            {
                if (socket.Available > 0)
                {
                    byte[] buffer = new byte[socket.Available];

                    EndPoint remoteEp = new IPEndPoint(IPAddress.Any, 0);

                    int lenght = socket.ReceiveFrom(buffer, ref remoteEp);

                    string from = ((IPEndPoint)remoteEp).Address.ToString();

                    string messaggio = Encoding.UTF8.GetString(buffer, 0, lenght);

                    Dispatcher.BeginInvoke(new Action( () => {
                        lst_messaggi.Items.Add(from + " : " +messaggio);
                    }));

                   

                }
            }
        }

        

        private void btn_invia_Click(object sender, RoutedEventArgs e)
        {
            IPAddress remote_address = IPAddress.Parse(txt_ip.Text);                                             // Destinatario
            IPEndPoint remote_endpoint = new IPEndPoint(remote_address.MapToIPv4(), int.Parse(txt_porta.Text));  // Endpoint del destinatario
            // I messaggi saranno inviati all'ip scelto sulla porta scelta

            // Conversione del messaggio in un array di byte
            byte[] msg = Encoding.UTF8.GetBytes(txt_messaggio.Text);    // Uso la codifica UTF-8

            // Ora posso inviare il messaggio
            socket.SendTo(msg, remote_endpoint);
            txt_messaggio.Text = "";
            txt_ip.Text = "";
           
            
        }

        Agenda agenda = null;
        private void agenda_txt_Click(object sender, RoutedEventArgs e)
        {
            agenda = new Agenda(this, ref contatti);
            agenda.Show();
        }


        private void lst_messaggi_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            string msg = lst_messaggi.SelectedItem.ToString();
            MessageBox.Show(msg);
        }


        public List<Contatto> contatti = new List<Contatto>();
        private void CaricaContatti()
        {
            using (StreamReader reader = new StreamReader("agenda.txt"))
            {
                try
                {
                    while (reader.EndOfStream == false)
                    {
                        string tmp = reader.ReadLine();
                        string[] vs = tmp.Split('|');
                        Contatto c = new Contatto(vs[0], vs[1], vs[2]);
                        contatti.Add(c);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }



        public void RecuperaContatto(Contatto _contatto)
        {
            txt_ip.Text = _contatto.Ip;
            txt_porta.Text = _contatto.Port;
            agenda.Close();
        }

    }

}