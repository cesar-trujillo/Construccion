using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubNaval
{
    internal class Enumeradores
    {
        public enum CargoPersona
        {
            SOCIO = 1,
            CAPITAN = 2,
            SOCIO_CAPITAN = 3
        }

        public enum EstadosSalida
        {
            EN_PROCESO,
            FINALIZADA
        }

        public static void EnumToListBox(Type EnumType, ListControl TheListBox, bool valorNumerico)
        {
            if (valorNumerico)
            {
                var values = Enum.GetValues(EnumType);
                var items = new List<KeyValuePair<string, int>>();
                foreach (int value in values)
                {
                    items.Add(new KeyValuePair<string, int>(
                        Enum.GetName(EnumType, value), value));
                }
                TheListBox.DataSource = items;
                TheListBox.DisplayMember = "Key";
                TheListBox.ValueMember = "Value";
            }
            else
            {
                TheListBox.DataSource = Enum.GetValues(EnumType);
            }
        }
    }
}
