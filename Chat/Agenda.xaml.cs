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


        public void CaricaLista()
        {
            foreach(Contatto c in _contatti)
            {
                contatti_lst.Items.Add(c.Nome);
            }
        }

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

        private void add_contatto_btn_Click(object sender, RoutedEventArgs e)
        {
            string nome = nome_txt.Text;
            string ip = indirizzo_txt.Text;
            string porta = porta_txt.Text;
            Contatto c = new Contatto(nome, ip, porta);
            _contatti.Add(c);
            contatti_lst.Items.Add(c.Nome);

            ScriviFile();
        }

        public void ScriviFile()
        {
            using (StreamWriter writer = new StreamWriter("agenda.txt"))
            { 
                foreach(Contatto c in _contatti)
                {
                    string linea = c.Nome + "|" + c.Ip +"|" + c.Port;
                    writer.WriteLine(linea);
                }
            
            }
        }
    }
}
