using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class Contatto
    {
        //la classe rappresenta i contatti di un agenda, composti da nome, indirizzo ip e porta che usano per la chat.
        //ho dei campi privati accessibili attraverso delle proprtità che in modalità set, fanno dei controlli sulla stringa che ricevono.
        public Contatto(string nome, string ip, string porta)
        {
            Nome = nome;
            Ip = ip;
            Port = porta;
        }

        private string _nome;
        private string _ip;
        private string _porta;

        public string Nome 
        { 
            
            get
            {
                return _nome;
            }
            set
            {
                if (value != "" && value != null)
                    _nome = value;
                else
                    throw new Exception("Valori non validi");
            }
        
        }


        public string Ip
        {

            get
            {
                return _ip;
            }
            set
            {
                if (value != "" && value != null)
                    _ip = value;
                else
                    throw new Exception("Valori non validi");
            }
        }


        public string Port
        {

            get
            {
                return _porta;
            }
            set
            {
                if (value != "" && value != null)
                    _porta = value;
                else
                    throw new Exception("Valori non validi");
            }
        }
    }
}
