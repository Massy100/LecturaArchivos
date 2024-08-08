using System;
using System.IO;

class Program
{
    static void Main()
    {
        IngresarConceptosYDefiniciones();
        MostrarContenidoYContarPalabras();
    }

    static void IngresarConceptosYDefiniciones()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(basePath, "data.txt");

        bool agregarOtro = true;

        while (agregarOtro)
        {
            Console.Write("Ingrese el concepto y su definición (concepto:definición): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || !input.Contains(":"))
            {
                Console.WriteLine("Formato incorrecto. Asegúrese de usar el formato concepto:definición.");
                continue;
            }

            try
            {
                File.AppendAllText(filePath, input + Environment.NewLine);
                Console.WriteLine("Concepto y definición almacenados en " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al almacenar el concepto y definición: " + ex.Message);
            }

            Console.Write("¿Desea agregar otro concepto y definición? (s/n): ");
            string respuesta = Console.ReadLine().Trim().ToLower();

            if (respuesta != "s")
            {
                agregarOtro = false;
            }
        }
    }

    static void MostrarContenidoYContarPalabras()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(basePath, "data.txt");

        try
        {
            string contenido = File.ReadAllText(filePath);
            Console.WriteLine("Contenido del archivo:");
            Console.WriteLine(contenido);

            string[] lineas = contenido.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var linea in lineas)
            {
                if (!string.IsNullOrWhiteSpace(linea) && linea.Contains(":"))
                {
                    string[] partes = linea.Split(new[] { ':' }, 2);
                    if (partes.Length == 2)
                    {
                        Console.WriteLine($"Concepto: {partes[0].Trim()}, Definición: {partes[1].Trim()}");
                    }
                }
            }

            int cantidadPalabras = ContarPalabras(contenido);
            Console.WriteLine($"Cantidad de palabras en el archivo: {cantidadPalabras}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al leer el archivo: " + ex.Message);
        }
    }

    static int ContarPalabras(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            return 0;
        }

        string[] palabras = texto.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        return palabras.Length;
    }
}
