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
using System.Windows.Shapes;
using System.IO;

namespace Chat
{
    /// <summary>
    /// Logica di interazione per Agenda.xaml
    /// </summary>
    public partial class Agenda : Window
    {
        List<Contatto> _contatti = null;
        MainWindow main = null;
        
        public Agenda(MainWindow mainwindow, ref List<Contatto> c)
        {
            main = mainwindow;
            _contatti = c;
            InitializeComponent();
            CaricaLista();
        }

        //metodo che riempie la listbox dei contatti con i nomi dei contatti
        public void CaricaLista()
        {
            foreach(Contatto c in _contatti)
            {
                contatti_lst.Items.Add(c.Nome);
            }
        }


        //quando si clicca seleziona, recupero l'indice dell'elemento selezionato nella listbox e lo passo al metodo RecuperaContatto nella mainwindow, che poi andrà a recuperare il contatto corrispondente dalla lista contatti.
        private void Seleziona_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = contatti_lst.SelectedIndex;
                main.RecuperaContatto(_contatti[index]);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //quando clicco su aggiungi contatto, recupero il contenuto delle textbox e le uso per creare un nuovo oggetto contatto, se questo non genera errori, lo inserisco nella lista contatti, e solo il nome nella listbox dell'agenda, poi aggiorno il file che li memorizza.
        private void add_contatto_btn_Click(object sender, RoutedEventArgs e)
        {
            string nome = nome_txt.Text;
            string ip = indirizzo_txt.Text;
            string porta = porta_txt.Text;
            Contatto c = null;
            try
            {
                c = new Contatto(nome, ip, porta);
                _contatti.Add(c);
                contatti_lst.Items.Add(c.Nome);
                ScriviFile();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        //qui apro il file in append e stampo come ultima riga l'ultimo elemento della lista, ovvere il contatto appena inserito.
        public void ScriviFile()
        {
            using (StreamWriter writer = new StreamWriter("agenda.txt", true))
            {
                Contatto c = _contatti.Last();
                string linea = c.Nome + "|" + c.Ip +"|" + c.Port;
                writer.WriteLine(linea);
                
            
            }
        }
    }
}
