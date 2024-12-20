using Entidades.Final;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    ///Agregar manejo de excepciones en TODOS los lugares críticos!!!

    public delegate void DelegadoThreadConParam(object param);

    public partial class FrmPrincipal : Form
    {
        protected Task hilo;
        protected CancellationTokenSource cts;

        public FrmPrincipal()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            this.Text = "Cambiar por su apellido y nombre";
            MessageBox.Show(this.Text);         
        }

        ///
        /// CRUD
        ///
        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListado frm = new FrmListado();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.Show(this);
        }

        ///
        /// VER LOG
        ///
        private void verLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo de usuarios";
           // DialogResult rta = DialogResult.Cancel;///Reemplazar por la llamada al método correspondiente del OpenFileDialog
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "Archivos de log (*.log)|*.log";
            ofd.DefaultExt = ".log";
            ofd.FileName = "usuarios";
            DialogResult rta = ofd.ShowDialog();
            if (rta == DialogResult.OK)
            {
                /// Mostrar en txtUsuariosLog.Text el contenido del archivo .log
                txtUsuariosLog.Text = File.ReadAllText(ofd.FileName);
            }
            else
            {
                MessageBox.Show("No se muestra .log");
            }
        }

        ///
        /// DESERIALIZAR JSON
        ///
        private void deserializarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Entidades.Final.Usuario> listado = null;
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "usuarios_repetidos.json"); ; /// Reemplazar por el path correspondiente
            bool todoOK = Manejadora.DeserializarJSON(path, out listado); /// Reemplazar por la llamada al método correspondiente de Manejadora

            if (todoOK)
            {
                this.txtUsuariosLog.Clear();
                foreach (var usuario in listado)
                {
                    txtUsuariosLog.Text += usuario.ToString() + Environment.NewLine;
                }
                /// Mostrar en txtUsuariosLog.Text el contenido de la deserialización.
            }
            else
            {
                MessageBox.Show("NO se pudo deserializar a JSON");
            }
        }

        ///
        /// TASK
        ///
        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cts = new CancellationTokenSource();
            this.hilo = Task.Run(() =>
            {
                {
                    while (!cts.IsCancellationRequested)
                    {
                        ActualizarListadoUsuarios(this);

                        lstUsuarios.BackColor = System.Drawing.Color.Black;
                        lstUsuarios.ForeColor = System.Drawing.Color.White;

                        Thread.Sleep(1500);

                        lstUsuarios.BackColor = System.Drawing.Color.White;
                        lstUsuarios.ForeColor = System.Drawing.Color.Black;
                        Thread.Sleep(1500);
                    }
                }
            });
            /// inicializar tarea
            ///Se desasocia al manejador de eventos.
            this.taskToolStripMenuItem.Click -= new EventHandler(this.taskToolStripMenuItem_Click);
            
        }


        ///PARA ACTUALIZAR LISTADO DESDE BD EN HILO
        public void ActualizarListadoUsuarios(object param)
        {
            /// Implementar...
            List<Usuario> listaUsers = ADO.ObtenerTodos();

            this.Invoke((MethodInvoker)delegate
            {
                lstUsuarios.Items.Clear();
                foreach (Usuario usuario in listaUsers)
                {
                    lstUsuarios.Items.Add(usuario);
                }
            });

        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            ///CANCELAR HILO
            try
            {
                this.cts.Cancel();
            }
            catch
            {
                this.Close();
            }
        }
    }
}
