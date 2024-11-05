using System;
using System.Collections.Generic;
using System.Globalization;

public class Alumno
{
    public string Nombre { get; set; }
    public List<float> Notas { get; set; }

    public Alumno(string nombre)
    {
        Nombre = nombre;
        Notas = new List<float>();
    }

    public float Promedio()
    {
        if (Notas.Count == 0) return 0;
        float suma = 0;
        foreach (var nota in Notas)
        {
            suma += nota;
        }
        return (float)Math.Round(suma / Notas.Count, 2); // Redondear a 2 decimales
    }

    public string ObtenerEstado()
    {
        float promedio = Promedio();
        if (promedio < 5.5) return "DEBE RECURSAR LA MATERIA";
        if (promedio >= 5.5 && promedio < 6) return "POSIBLE REGULAR";
        if (promedio >= 6 && promedio <= 7.5) return "REGULAR";
        if (promedio > 7.5 && promedio < 8) return "POSIBLE PROMOCION";
        return "PROMOCIONADO";
    }
}

public class Materia
{
    public string Nombre { get; set; }
    public List<Alumno> Alumnos { get; set; }

    public Materia(string nombre)
    {
        Nombre = nombre;
        Alumnos = new List<Alumno>();
    }
}

class Program
{
    static List<Materia> materias = new List<Materia>
    {
        new Materia("Analisis De Sistemas de Informacion"),
        new Materia("Analisis Matematico II"),
        new Materia("Física II"),
        new Materia("Ingenieria Y Sociedad"),
        new Materia("Ingles II"),
        new Materia("Paradigmas de Programacion"),
        new Materia("Sintaxis Y Semantica de los Lenguajes"),
        new Materia("Sistemas Operativos")
    };

    static void Main(string[] args)
    {
        // Inicialización de alumnos
        InicializarAlumnos();

        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("Gestión de Alumnos - INGENIERIA EN SISTEMAS");
            Console.WriteLine("1. Ver Listado de Alumnos por Materia");
            Console.WriteLine("2. Alta de Alumnos");
            Console.WriteLine("3. Baja de Alumnos");
            Console.WriteLine("4. Ingresar Notas");
            Console.WriteLine("5. Modificar Nota por Recuperatorios");
            Console.WriteLine("6. Ver Promedio y Estado de Alumnos");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Por favor ingrese un número válido.");
                Console.ReadLine();
                continue;
            }

            switch (opcion)
            {
                case 1:
                    VerListadoAlumnosPorMateria();
                    break;
                case 2:
                    AltaAlumno();
                    break;
                case 3:
                    BajaAlumno();
                    break;
                case 4:
                    IngresarNotas();
                    break;
                case 5:
                    ModificarNotaPorRecuperatorios();
                    break;
                case 6:
                    VerSituacionAlumnos();
                    break;
                case 7:
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    Console.ReadLine();
                    break;
            }
        } while (opcion != 7);
    }

    static void InicializarAlumnos()
    {
        // Inicializa alumnos en todas las materias
        string[] nombres = { "Carril, Nicolas", "Chiapello, Gianella", "Nolla, Lautaro", "Druetta, Emiliano", "Chavez, Nicolas", "Valcovich, Gustavo" };

        foreach (var nombre in nombres)
        {
            foreach (var materia in materias)
            {
                materia.Alumnos.Add(new Alumno(nombre));
            }
        }
    }

    static void VerListadoAlumnosPorMateria()
    {
        Console.Clear();
        Console.WriteLine("Seleccione una materia para ver los alumnos (o -1 para regresar):");
        for (int i = 0; i < materias.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {materias[i].Nombre}");
        }

        if (!int.TryParse(Console.ReadLine(), out int materiaIndex) || materiaIndex < -1 || materiaIndex > materias.Count)
        {
            Console.WriteLine("Materia no válida.");
            Console.ReadLine();
            return;
        }

        if (materiaIndex == -1) return; // Regresar al menú anterior

        var alumnos = materias[materiaIndex - 1].Alumnos;
        if (alumnos.Count == 0)
        {
            Console.WriteLine("No hay alumnos inscriptos en esta materia.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"Listado de Alumnos en {materias[materiaIndex - 1].Nombre}:");
        for (int i = 0; i < alumnos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {alumnos[i].Nombre}");
        }
        Console.ReadLine();
    }

    static void AltaAlumno()
    {
        Console.Clear();
        Console.Write("Ingrese el nombre del alumno a dar de alta (o -1 para regresar): ");
        string nombre = Console.ReadLine();
        if (nombre == "-1") return; // Regresar al menú anterior

        Console.WriteLine("Seleccione la materia para dar de alta:");
        for (int i = 0; i < materias.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {materias[i].Nombre}");
        }

        if (!int.TryParse(Console.ReadLine(), out int materiaIndex) || materiaIndex < 1 || materiaIndex > materias.Count)
        {
            Console.WriteLine("Materia no válida.");
            Console.ReadLine();
            return;
        }

        var alumno = new Alumno(nombre);
        materias[materiaIndex - 1].Alumnos.Add(alumno);
        Console.WriteLine($"Alumno {nombre} dado de alta en {materias[materiaIndex - 1].Nombre}.");
        Console.ReadLine();
    }

    static void BajaAlumno()
    {
        Console.Clear();
        Console.WriteLine("Seleccione la materia de la que desea dar de baja a un alumno (o -1 para regresar):");
        for (int i = 0; i < materias.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {materias[i].Nombre}");
        }

        if (!int.TryParse(Console.ReadLine(), out int materiaIndex) || materiaIndex < -1 || materiaIndex > materias.Count)
        {
            Console.WriteLine("Materia no válida.");
            Console.ReadLine();
            return;
        }

        if (materiaIndex == -1) return; // Regresar al menú anterior

        var alumnos = materias[materiaIndex - 1].Alumnos;
        if (alumnos.Count == 0)
        {
            Console.WriteLine("No hay alumnos inscriptos en esta materia.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Seleccione el alumno que desea dar de baja:");
        for (int i = 0; i < alumnos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {alumnos[i].Nombre}");
        }

        if (!int.TryParse(Console.ReadLine(), out int alumnoIndex) || alumnoIndex < 1 || alumnoIndex > alumnos.Count)
        {
            Console.WriteLine("Alumno no válido.");
            Console.ReadLine();
            return;
        }

        alumnos.RemoveAt(alumnoIndex - 1);
        Console.WriteLine("Alumno dado de baja.");
        Console.ReadLine();
    }

    static void IngresarNotas()
    {
        Console.Clear();
        Console.WriteLine("Seleccione la materia para ingresar notas (o -1 para regresar):");
        for (int i = 0; i < materias.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {materias[i].Nombre}");
        }

        if (!int.TryParse(Console.ReadLine(), out int materiaIndex) || materiaIndex < -1 || materiaIndex > materias.Count)
        {
            Console.WriteLine("Materia no válida.");
            Console.ReadLine();
            return;
        }

        if (materiaIndex == -1) return; // Regresar al menú anterior

        var alumnos = materias[materiaIndex - 1].Alumnos;
        if (alumnos.Count == 0)
        {
            Console.WriteLine("No hay alumnos inscriptos en esta materia.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Seleccione el alumno:");
        for (int i = 0; i < alumnos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {alumnos[i].Nombre}");
        }

        if (!int.TryParse(Console.ReadLine(), out int alumnoIndex) || alumnoIndex < 1 || alumnoIndex > alumnos.Count)
        {
            Console.WriteLine("Alumno no válido.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese la nota del 1° parcial: ");
        float primerParcial = LeerNota();
        Console.Write("Ingrese la nota del 2° parcial: ");
        float segundoParcial = LeerNota();

        alumnos[alumnoIndex - 1].Notas.Add(primerParcial);
        alumnos[alumnoIndex - 1].Notas.Add(segundoParcial);
        Console.WriteLine("Notas ingresadas exitosamente.");
        Console.ReadLine();
    }

    static void ModificarNotaPorRecuperatorios()
    {
        Console.Clear();
        Console.WriteLine("Seleccione la materia para modificar nota por recuperatorio (o -1 para regresar):");
        for (int i = 0; i < materias.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {materias[i].Nombre}");
        }

        if (!int.TryParse(Console.ReadLine(), out int materiaIndex) || materiaIndex < -1 || materiaIndex > materias.Count)
        {
            Console.WriteLine("Materia no válida.");
            Console.ReadLine();
            return;
        }

        if (materiaIndex == -1) return; // Regresar al menú anterior

        var alumnos = materias[materiaIndex - 1].Alumnos;
        var alumnosPosibles = alumnos.FindAll(a => a.ObtenerEstado() == "POSIBLE REGULAR" || a.ObtenerEstado() == "POSIBLE PROMOCION");

        if (alumnosPosibles.Count == 0)
        {
            Console.WriteLine("No hay alumnos en condición de Posible Regular o Posible Promoción.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Seleccione el alumno:");
        for (int i = 0; i < alumnosPosibles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {alumnosPosibles[i].Nombre}");
        }

        if (!int.TryParse(Console.ReadLine(), out int alumnoIndex) || alumnoIndex < 1 || alumnoIndex > alumnosPosibles.Count)
        {
            Console.WriteLine("Alumno no válido.");
            Console.ReadLine();
            return;
        }

        Alumno alumno = alumnosPosibles[alumnoIndex - 1];

        // Mostrar notas anteriores
        Console.WriteLine($"Notas actuales de {alumno.Nombre}:");
        for (int i = 0; i < alumno.Notas.Count; i++)
        {
            Console.WriteLine($"Nota {i + 1}: {alumno.Notas[i]}");
        }

        Console.Write("¿Desea modificar la nota del 1° o 2° parcial? (1 o 2): ");
        int notaIndex;
        while (!int.TryParse(Console.ReadLine(), out notaIndex) || notaIndex < 1 || notaIndex > 2)
        {
            Console.WriteLine("Selección no válida. Ingrese 1 o 2.");
        }

        Console.Write("Ingrese la nueva nota: ");
        float nuevaNota = LeerNota();
        alumno.Notas[notaIndex - 1] = nuevaNota;

        Console.WriteLine("Nota modificada exitosamente.");
        Console.ReadLine();
    }

    static void VerSituacionAlumnos()
    {
        Console.Clear();
        Console.WriteLine("Seleccione la materia para ver la situación de los alumnos (o -1 para regresar):");
        for (int i = 0; i < materias.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {materias[i].Nombre}");
        }

        if (!int.TryParse(Console.ReadLine(), out int materiaIndex) || materiaIndex < -1 || materiaIndex > materias.Count)
        {
            Console.WriteLine("Materia no válida.");
            Console.ReadLine();
            return;
        }

        if (materiaIndex == -1) return; // Regresar al menú anterior

        var alumnos = materias[materiaIndex - 1].Alumnos;
        if (alumnos.Count == 0)
        {
            Console.WriteLine("No hay alumnos inscriptos en esta materia.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"Situación de los alumnos en {materias[materiaIndex - 1].Nombre}:");
        foreach (var alumno in alumnos)
        {
            Console.WriteLine($"{alumno.Nombre}: {alumno.ObtenerEstado()} (Promedio: {alumno.Promedio()})");
        }
        Console.ReadLine();
    }

    static float LeerNota()
    {
        float nota;
        while (true)
        {
            string entrada = Console.ReadLine();
            entrada = entrada.Replace(',', '.'); // Cambiar la coma por un punto
            if (float.TryParse(entrada, NumberStyles.Float, CultureInfo.InvariantCulture, out nota) && nota >= 0 && nota <= 10)
                return nota;

            Console.Write("Nota no válida. Ingrese nuevamente (0 a 10): ");
        }
    }
}
