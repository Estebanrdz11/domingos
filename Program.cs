using Spectre.Console;
using GymManager.Models;
using GymManager.Services;

MiembroService servicio = new();

bool salir = false;

while (!salir)
{
    AnsiConsole.Clear();

    var opcion = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[cyan]=== SISTEMA DE GIMNASIO ===[/]")
            .AddChoices(new[]
            {
                "Listar miembros",
                "Registrar miembro",
                "Eliminar miembro",
                "Salir"
            }));

    switch (opcion)
    {
        case "Listar miembros":

            var tabla = new Table();

            tabla.Border(TableBorder.Rounded);

            tabla.AddColumn("ID");
            tabla.AddColumn("Nombre");
            tabla.AddColumn("Edad");
            tabla.AddColumn("Plan");

            foreach (var miembro in servicio.ObtenerTodos())
            {
                tabla.AddRow(
                    miembro.Id.ToString(),
                    miembro.Nombre,
                    miembro.Edad.ToString(),
                    miembro.Plan);
            }

            AnsiConsole.Write(tabla);
            Console.ReadKey();

            break;

        case "Registrar miembro":

            int id = AnsiConsole.Ask<int>("ID:");

            string nombre = AnsiConsole.Ask<string>("Nombre:");

            int edad = AnsiConsole.Ask<int>("Edad:");

            string plan = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Seleccione un plan")
                .AddChoices("Mensual", "Trimestral", "Anual"));

            servicio.Agregar(new Miembro(id, nombre, edad, plan));

            AnsiConsole.MarkupLine("[green]Miembro registrado correctamente.[/]");
            Console.ReadKey();

            break;

        case "Eliminar miembro":

            int eliminar = AnsiConsole.Ask<int>("Digite el ID del miembro:");

            if (servicio.Eliminar(eliminar))
                AnsiConsole.MarkupLine("[green]Miembro eliminado correctamente.[/]");
            else
                AnsiConsole.MarkupLine("[red]No existe un miembro con ese ID.[/]");

            Console.ReadKey();

            break;

        case "Salir":
            salir = true;
            break;
    }
}
