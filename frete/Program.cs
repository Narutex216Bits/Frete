using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();

        AnimacaoInicial();

        var pedido = new Pedido();

        while (true)
        {
            Console.Clear();
            DesenharTela();

            Console.SetCursorPosition(10, 12);
            Console.Write("PESO (kg)........: ");
            string pesoInput = Console.ReadLine();

            Console.SetCursorPosition(10, 13);
            Console.Write("DISTÂNCIA (km)...: ");
            string distanciaInput = Console.ReadLine();

            if (!double.TryParse(pesoInput, out double peso) ||
                !double.TryParse(distanciaInput, out double distancia))
            {
                ExibirErro("VALOR INVÁLIDO! APERTE QUALQUER TECLA PARA CORRIGIR...");
                continue;
            }

            Console.SetCursorPosition(10, 14);
            Console.Write("MODALIDADE.......: ");
            string modalidade = Console.ReadLine();

            Console.SetCursorPosition(10, 16);
            Console.Write("PROCESSANDO...");
            Thread.Sleep(800);

            try
            {
                double frete = pedido.CalcularFrete(modalidade, peso, distancia);

                Console.SetCursorPosition(10, 17);
                Console.WriteLine("█ RESULTADO DO CÁLCULO █".PadRight(50));
                Console.SetCursorPosition(10, 18);
                Console.WriteLine($"FRETE R$ {frete:F2}");
                Console.SetCursorPosition(10, 19);
                Console.WriteLine("MODALIDADE: {0}", ObterNomeModalidade(modalidade));
            }
            catch (ArgumentException)
            {
                ExibirErro("MODALIDADE INVÁLIDA! APERTE QUALQUER TECLA...");
            }

            Console.SetCursorPosition(10, 22);
            Console.Write(">>> TECLE <ENTER> PARA NOVO CÁLCULO | <ESC> SAIR...");

            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
                break;
        }

        Console.Clear();
        Console.SetCursorPosition(15, 12);
        Console.WriteLine("SISTEMA ENCERRADO. VOLTE SEMPRE!");
        Console.SetCursorPosition(15, 13);
        Console.WriteLine("──────▄▀▄─────▄▀▄");
        Console.SetCursorPosition(15, 14);
        Console.WriteLine("─────▄█░░▀▀▀▀▀░░█▄");
        Console.SetCursorPosition(15, 15);
        Console.WriteLine("─▄▄──█░░░░░░░░░░░█──▄▄");
        Console.SetCursorPosition(15, 16);
        Console.WriteLine("█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    static void AnimacaoInicial()
    {
        Console.Clear();
        string[] logo = {
            "╔══════════════════════════════════════╗",
            "║     SISTEMA DE FRETE 486 DX2 v1.0   ║",
            "║        ─────────────────────         ║",
            "║    ███████╗██████╗ ███████╗████████╗ ║",
            "║    ██╔════╝██╔══██╗██╔════╝╚══██╔══╝ ║",
            "║    █████╗  ██████╔╝█████╗     ██║    ║",
            "║    ██╔══╝  ██╔══██╗██╔══╝     ██║    ║",
            "║    ██║     ██║  ██║███████╗   ██║    ║",
            "║    ╚═╝     ╚═╝  ╚═╝╚══════╝   ╚═╝    ║",
            "╚══════════════════════════════════════╝"
        };

        for (int i = 0; i < logo.Length; i++)
        {
            Console.SetCursorPosition(10, 5 + i);
            Console.WriteLine(logo[i]);
            Thread.Sleep(100);
        }

        Console.SetCursorPosition(15, 16);
        Console.Write("INICIALIZANDO");
        for (int i = 0; i < 5; i++)
        {
            Console.Write(".");
            Thread.Sleep(300);
        }
        Console.Clear();
    }

    static void DesenharTela()
    {
        Console.SetCursorPosition(5, 2);
        Console.WriteLine("┌────────────────────────────────────────────────────────┐");
        Console.SetCursorPosition(5, 3);
        Console.WriteLine("│               CALCULADORA DE FRETE RETRÔ              │");
        Console.SetCursorPosition(5, 4);
        Console.WriteLine("├────────────────────────────────────────────────────────┤");

        for (int i = 5; i <= 20; i++)
        {
            Console.SetCursorPosition(5, i);
            Console.Write("│");
            Console.SetCursorPosition(70, i);
            Console.Write("│");
        }

        Console.SetCursorPosition(5, 21);
        Console.WriteLine("└────────────────────────────────────────────────────────┘");

        Console.SetCursorPosition(10, 6);
        Console.WriteLine("█ MODALIDADES DISPONÍVEIS:");
        Console.SetCursorPosition(10, 7);
        Console.WriteLine("   [1] PAC         ──► 10% DO VALOR BASE");
        Console.SetCursorPosition(10, 8);
        Console.WriteLine("   [2] SEDEX       ──► 20% DO VALOR BASE");
        Console.SetCursorPosition(10, 9);
        Console.WriteLine("   [3] TRANSP.     ──► 30% + R$10,00");
        Console.SetCursorPosition(10, 10);
        Console.WriteLine("   [4] SAIR        ──► ENCERRAR SISTEMA");
    }

    static void ExibirErro(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(10, 24);
        Console.Write("█ ERRO: " + mensagem + " █");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.ReadKey();
    }

    static string ObterNomeModalidade(string codigo)
    {
        return codigo switch
        {
            "1" => "PAC",
            "2" => "SEDEX",
            "3" => "TRANSPORTADORA",
            _ => "DESCONHECIDA"
        };
    }
}

public class Pedido
{
    public double CalcularFrete(string modalidade, double peso, double distancia)
    {
        if (modalidade.Equals("1", StringComparison.OrdinalIgnoreCase))
            return peso * distancia * 0.1;  // Corrigido: peso + distância * 0.1 estava errado

        if (modalidade.Equals("2", StringComparison.OrdinalIgnoreCase))
            return peso * distancia * 0.2;  // Corrigido

        if (modalidade.Equals("3", StringComparison.OrdinalIgnoreCase))
            return peso * distancia * 0.3 + 10;

        throw new ArgumentException("Modalidade de frete inválida");
    }
}