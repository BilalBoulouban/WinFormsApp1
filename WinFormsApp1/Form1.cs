using WinFormsApp1.Model;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Cargar el archivo XML
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivo XML (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                int rowsAffected = BD.EsborrarDades();
                DMCManager.CarregarModel(openFileDialog.FileName);
               // BD.getDades();
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

            string pathFitxer = openFileDialog1.FileName;
            // Aquí pots fer el que necessitis amb el fitxer XML seleccionat
            // per exemple, carregar-lo a una base de dades o processar les seves dades.
            // També pots mostrar la ruta del fitxer en un TextBox o una etiqueta.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
   }

}