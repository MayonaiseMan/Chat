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
        //DispatcherTimer dtimer = null;
        Thread checkChat = null;
        public MainWindow()
        {
            //Inizializzazione oggetti

            // SOCKET:
            // AddressFamily    : Enum, InterNetwork permette di lavorare con IPv4.
            // SocketType       : Enum.Dgram (DataGram). socket UPD.
            // ProtocolType     : Enum, UDP protocollo da utilizzare.
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // IP:
            IPAddress local_address = IPAddress.Any;                                      // Mittente del messaggio
            IPEndPoint local_endpoint = new IPEndPoint(local_address.MapToIPv4(), 64000); // Creazione endpoint ip del mittente
            // I messaggi ora verranno ricevuti e inviata dalla porta 66000 (not registered port)

            //Bind del local endpoint (punto di contatto locale) con la socket che quindi andrà ad arpire la porta 64000
            socket.Bind(local_endpoint);
            socket.Blocking = false;
            socket.EnableBroadcast = true;


            // sistema di aggiornamento fatto con i timer, ogni volta che il timer scadeva andava a controllare il buffer della socket attraverso un Event Handler sul metodo aggiornamento_dTimer che ho poi rinominato aggiornamento_chat
            /*
            dtimer = new DispatcherTimer();
            dtimer.Tick += new EventHandler(aggiornamento_dTimer);
            dtimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            dtimer.Start();
            */

            
            //qui chiamo il metodo per caricare la lista deii contatti salvati nel file, faccio così in modo che lo si fa una sola volta, e non tutte le volte che apro la finestra agenda, così devo solo passare la lista dei contatti in modo referenziale.
            CaricaContatti();
            InitializeComponent();


            //qui creo il thread sul quale viene eseguito il metodo di controllo della chat parallelamente al resto del programma.
            checkChat = new Thread(new ThreadStart (aggiornamento_chat));
            checkChat.Start();
        }

        private void aggiornamento_chat()
        {
            //qui il metodo che cicla in modo infinito guarda se ci sono nuovi byte nella socket, dopo di che creo un aray di byte che mi farà da buffer lungo quanto i nuovi byte da leggere.
            //poi creo un Endpoint generico che inserisco in modo referenziale nel metodo per ricevere nel buffer il contenuto del datagram UDP, rendere l'endpoint creato uguale a quello che ci ha spedito il messaggio e ottenere la lunghezza del contenuto.
            //dopo di che andiamo a ottenere l'indirizzo ip del mittente dall'endpoint, trasformiamo il buffer in una stinga codificata con UTF8, poi con un dispatcher in modo asincrono andiamo ad aggiungere il messaggio alla listbox dei messaggi, usiamo il dispatcher perché la listbox sta su un altro thread.
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

            // invio il messaggio al remote endpoint e svuoto le textbox 
            socket.SendTo(msg, remote_endpoint);
            txt_messaggio.Text = "";
            txt_ip.Text = "";
            txt_porta.Text = "";
           
            
        }

        Agenda agenda = null;
        private void agenda_txt_Click(object sender, RoutedEventArgs e)
        {
            //quando l'utente clicca sul bottone agenda crea una nuova finestra di tipo Agenda a cui passo questa finestra main e la lista di contatti.
            agenda = new Agenda(this, ref contatti);
            agenda.Show();
        }

        public List<Contatto> contatti = new List<Contatto>();
        private void CaricaContatti()
        {

            //utilizzando uno streamReader vado a leggere un txt riga per riga, e ogni riga la suddividò rispetto al simbolo | ottenendo un array che in ordine ha nome, ip, porta creo un elemento contatto che aggiungo alla lista contatti, tutti in un try catch perché l'oggetto contatto potrebbe rimandare delle Exception.
            using (StreamReader reader = new StreamReader("agenda.txt"))
            {
                try
                {
                    string[] vs;
                    while (reader.EndOfStream == false)
                    {
                        string tmp = reader.ReadLine();
                        vs = tmp.Split('|');
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

            // questo metodo viene richiamato dalla finesta agenda e serve a riempire le textbox dell'indirizzo e dalla porta con le informazioni legate all'elemento dell'agenda selezionato e poi chiude la finestra agenda.
            txt_ip.Text = _contatto.Ip;
            txt_porta.Text = _contatto.Port;
            agenda.Close();
        }


        // questo metodo prende il contenuto di un elemento selezionato della listbox su sui si fa doppio click, e lo mostra in una textbox, a volte la listbox taglia il testo se è troppo lungo.
        private void lst_messaggi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string msg = lst_messaggi.SelectedItem.ToString();
            MessageBox.Show(msg);
        }
    }

}