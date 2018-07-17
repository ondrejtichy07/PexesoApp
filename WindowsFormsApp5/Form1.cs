using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PexesoApp
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        /*
         *  List složený ze znaků, které se budou zobrazovat ve čtvercích. 
         */
        List<string> znaky = new List<string>()
        {
            "F", "F", "K", "K", "q", "q", "k", "k",
            "a", "a", "s", "s", "w", "w", "z", "z"
        };

        /*
         *  Proměnné
         */
        Label prvniClicknuti, druheClicnutik;
        int skore;
        int highSkore;
        int pocetHer = 0;


        public Form1() // Konstruktor
        {
            InitializeComponent();
            pridelitIkonuCtverci();
            updateSkore();
        }


        /* 
         *      Aktualizace skoreLabel a highSkoreLabel.
         */
        private void updateSkore()
        {
            skoreLabel.Text = "Skóre: " + skore.ToString();
            highSkoreLabel.Text = "High Skóre: " + highSkore.ToString();
        }

        
        private void label_Click(object sender, EventArgs e)
        {
            


            if (prvniClicknuti != null && druheClicnutik != null)
                return;

            Label kliknutyLabel = sender as Label;

            if (kliknutyLabel == null)
                return;

            if (kliknutyLabel.ForeColor == Color.Black)
                return;

            if (prvniClicknuti == null)
            {
                prvniClicknuti = kliknutyLabel;
                prvniClicknuti.ForeColor = Color.Black;
                return;
            }

            druheClicnutik = kliknutyLabel;
                      
            skore++;
            
            updateSkore();
            druheClicnutik.ForeColor = Color.Black;

            kontrolaKonce();

            if (prvniClicknuti.Text == druheClicnutik.Text)
            {
                prvniClicknuti = null;
                druheClicnutik = null;
            }
            else
                timer1.Start();

        }


        /*
         *   Nastaví barvu textu všech labelů na barvu pozadí.
         *   Resetuje proměnné počítající skóre.
         *   Změní hodnotu highSkore, je-li dosaženo dosud nejlepšího výsledku.
         */
        private void restart()
        {
            Label label;
            for (int i = 0; i < tabule.Controls.Count; i++)
            {
                label = tabule.Controls[i] as Label;
                label.ForeColor = Color.LightCoral;

                pocetHer++;
                skore = 0;
                updateSkore();
            }

        }


        /*
         *   Kontrola barev textu všech labelů, pokud jsou všechny černé, ukončí hru a vyvolá Message Box.
         *   Změní hodnotu highSkore, je-li dosaženo dosud nejlepšího výsledku.
         */
        private void kontrolaKonce()
        {
            Label label;
            for (int i = 0; i < tabule.Controls.Count; i++)
            {
                label = tabule.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }

                    
            MessageBox.Show("Konec hry. Vaše skóre je " + skore + ".");

            if (pocetHer == 0)
            {
                highSkore = skore;
            }
            else
            {
                if (skore < highSkore)
                {
                    highSkore = skore;
                }
            }

            restart();
        }

        
        /*
         *   Časovač po kliknutí na dvě neshodující se znaky.
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            prvniClicknuti.ForeColor = prvniClicknuti.BackColor;
            druheClicnutik.ForeColor = druheClicnutik.BackColor;

            prvniClicknuti = null;
            druheClicnutik = null;
        }

        
        /*
         *      Náhodně přidělení znaky každému čtverci.
         */
        private void pridelitIkonuCtverci()
        {
            Label label;
            int nahodneCislo;


            for (int i = 0; i < tabule.Controls.Count; i++)
            {
                if (tabule.Controls[i] is Label)
                    label = (Label)tabule.Controls[i];
                else
                    continue;

                nahodneCislo = random.Next(0, znaky.Count);
                label.Text = znaky[nahodneCislo];

                znaky.RemoveAt(nahodneCislo);
            }

        }
    }
}
