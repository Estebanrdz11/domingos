using Spectre.Console;
decimal monto = AnsiConsole.Ask<decimal>("Ingrese el monto del préstamo:");
decimal tasaAnual = AnsiConsole.Ask<decimal>("Ingrese la tasa de interés anual (%):");
int meses = AnsiConsole.Ask<int>("Ingrese el plazo del préstamo (meses):");

decimal tasaMensual = (tasaAnual / 12) / 100;

decimal cuota = monto *
    (tasaMensual * (decimal)Math.Pow((double)(1 + tasaMensual), meses))
    / ((decimal)Math.Pow((double)(1 + tasaMensual), meses) - 1);

decimal saldo = monto;

var tabla = new Table();

tabla.Border(TableBorder.Rounded);

tabla.AddColumn("Cuota");
tabla.AddColumn("Pago");
tabla.AddColumn("Interés");
tabla.AddColumn("Abono Capital");
tabla.AddColumn("Saldo");

for (int i = 1; i <= meses; i++)
{
    decimal interes = saldo * tasaMensual;
    decimal abonoCapital = cuota - interes;
    saldo -= abonoCapital;

    if (saldo < 0)
        saldo = 0;

    tabla.AddRow(
        i.ToString(),
        cuota.ToString("N2"),
        interes.ToString("N2"),
        abonoCapital.ToString("N2"),
        saldo.ToString("N2")
    );
}

AnsiConsole.Write(tabla);