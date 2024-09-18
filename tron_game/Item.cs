using System;
using tron_game;
using TronGame;

public abstract class Item
{
    public Nodo Posicion { get; private set; }  // Posición del ítem en el grid
    public string Tipo { get; protected set; }  // Tipo de ítem para facilitar la identificación

    public Item(Nodo posicion)
    {
        Posicion = posicion;
        Posicion.Ocupado = true;  // El nodo ahora está ocupado por el ítem
    }

    // Método abstracto que aplicará el efecto del ítem a la moto
    public abstract void AplicarEfecto(Moto moto);

    // Liberar el nodo donde estaba el ítem
    public void Remover()
    {
        Posicion.Ocupado = false;  // Liberar el nodo cuando el ítem se elimina
        
    }


}

public class CeldaCombustible : Item
{
    private int capacidadCombustible;

    public CeldaCombustible(Nodo posicion) : base(posicion)
    {
        Tipo = "CeldaCombustible";
        capacidadCombustible = new Random().Next(10, 51);  // Capacidad aleatoria entre 10 y 50 unidades
    }

    public override void AplicarEfecto(Moto moto)
    {
        if (moto.Combustible < 200)  // Supongamos que 200 es el combustible máximo
        {
            moto.Combustible += capacidadCombustible;
            if (moto.Combustible > 200)
            {
                moto.Combustible = 200;  // Limitar el combustible al máximo
            }
            Console.WriteLine($"Combustible incrementado en {capacidadCombustible}. Combustible actual: {moto.Combustible}");
        }
    }

}

public class CrecimientoEstela : Item
{
    private int incrementoEstela;

    public CrecimientoEstela(Nodo posicion) : base(posicion)
    {
        Tipo = "CrecimientoEstela";
        incrementoEstela = new Random().Next(1, 3);  // Incremento aleatorio entre 1 y 10
    }

    public override void AplicarEfecto(Moto moto)
    {
        moto.TamañoEstela += incrementoEstela;
    }
}

public class Bomba : Item
{
    public Bomba(Nodo posicion) : base(posicion)
    {
        Tipo = "Bomba";
    }

    public override void AplicarEfecto(Moto moto)
    {
        // La bomba causa una explosión y "mata" a la moto al vaciar el combustible
        moto.Combustible = 0;  // Simulamos la muerte vaciando el combustible
        Console.WriteLine("La bomba ha explotado. Combustible de la moto: 0");
    }
}



