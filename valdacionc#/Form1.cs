using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace valdacionc_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Agregar controladores de eventos TextChanged a los campos
            tbEdad.TextChanged += ValidarEdad;
            tbNombre.TextChanged += ValidarNombre;
            tbEstatura.TextChanged += ValidarEstatura;
            tbApellidos.TextChanged += ValidarApellidos;
            tbTelefono.TextChanged += ValidarTelefono;

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            tbEdad.Clear();
            tbNombre.Clear();
            tbEstatura.Clear();
            tbApellidos.Clear();
            tbTelefono.Clear();
            rbHombre.Checked= false;
            rbMujer.Checked= false;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombres = tbNombre.Text;
            string apellidos = tbApellidos.Text;
            string edad = tbEdad.Text;
            string estatura = tbEstatura.Text;
            string telefono = tbTelefono.Text;

            string genero = "";
            if (rbHombre.Checked)
            {
                genero = "Hombre";
            }
            else if (rbMujer.Checked)
            {
                genero = "Mujer";
            }

            if (EsEnteroValido(edad) && EsDecimalValido(estatura) && EsEnteroValidoDe10Digitos(telefono) &&
                EsTextoValido(nombres) && EsTextoValido(apellidos))
            {
                //Crear una cadena con los datos
                string datos = ($"nombre: {nombres}\r\nApellidos:{apellidos}\r\nEdad:{edad}\r\nEstatura:{estatura}\r\nTelefono):{telefono}\r\nGenero:{genero}");
                
                //guardar los datos en u archivo de texto
                string rutaArchivo = ("C:\\Users\\Camil\\Desktop\\Programacion avanzada c#\\validacion");
                bool archivoExistente = File.Exists(rutaArchivo);
                Console.WriteLine(archivoExistente);

                if (archivoExistente == false)
                {
                    File.WriteAllText(rutaArchivo, datos);
                }
                else
                {
                    //verificar si el archivo ya existe
                    using (StreamWriter writer = new StreamWriter(rutaArchivo))
                    {
                        if (archivoExistente)
                        {
                            //si el archivo ya existe, añadiri un separador antes del nuevo registro
                            writer.WriteLine();
                        }
                        writer.WriteLine(datos);
                    }
                }
                 //mostrar un mensaje co los datos capturados
                 MessageBox.Show("Datos guardados con exito:\n\n" + datos, "informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
            else
            {
                MessageBox.Show("porfavor, ingrese datos validos en los campos.","error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private bool EsEnteroValido(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);
        }

        private bool EsDecimalValido(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor, out resultado);
        }

        private bool EsEnteroValidoDe10Digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado) && valor.Length == 10;

        }

        private bool EsTextoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s]+$"); //solo letras y espacio
        }

        private void ValidarEdad(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (EsEnteroValido(textBox.Text)) 
            {
                MessageBox.Show("por favor, ingrese una edad valida.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }

        private void ValidarEstatura(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if(!EsDecimalValido(textBox.Text))
            {
                MessageBox.Show("por favor, ingrese una estatura valida.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }

        private void ValidarTelefono(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string input = textBox.Text;
            //Eliminar espacion en blanco y guiones si es necesario
            //iput = input.Repalce("","").Replace("","");
            if(EsEnteroValidoDe10Digitos(input))
            {
                MessageBox.Show("por favor, ingrese un numero de telefono valido de 10 digitos.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
            else if(EsEnteroValidoDe10Digitos(input))
            {
                MessageBox.Show("por favor, ingrese un numero de telefono valido de 10 digitos.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidarNombre(object sender, EventArgs e) 
        { 
            TextBox textbox = (TextBox)sender;
            if (!EsTextoValido(textbox.Text))
            {
                MessageBox.Show("por favor, ingrese un nombre valido (solo letras y espacios).", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidarApellidos(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsTextoValido(textbox.Text))
            {
                MessageBox.Show("por favor, ingrese apellidos validos (solo letras y espacios).", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
