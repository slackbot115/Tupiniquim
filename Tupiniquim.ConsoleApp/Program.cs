using System;

namespace Tupiniquim.ConsoleApp
{
    /*
        Posição do Robô: X, Y e direção. 
        Ex: 0,0,N
        Use as orientações: N = norte, S = sul, L = leste, O = oeste.
        Letras para movimentação: E, D e M. 
        As letras E e D fazem o robô virar 90 graus para esquerda e direita respectivamente, sem sair do lugar. 
        A letra M significa se mover uma posição no grid para frente, mantendo a mesma direção. 
    */


    internal class Program
    {
        public static int[] formatarString(string input)
        {
            // Formatando os inputs de string
            string input_spaces = input.Replace(" ", "");
            char[] input_new_string = input_spaces.ToCharArray();
            int[] input_final = Array.ConvertAll(input_new_string, c => (int)Char.GetNumericValue(c));

            return input_final;
        }

        static void Main(string[] args)
        {
            /*
                Inputs
                Coordenadas do campo total (2 numeros inteiros)
                Posição inicial do robô (x, y)
                Instruções para exploração da área (Letras variadas)

                Ex:
                Grid: 5 5
                Posição Inicial: 1 2 N
                Instruções: EMEMEMEMM

                Possiveis bugs:
                Digitar um item invalido durante a sequencia de instruções
                Posição inicial fora do escopo do grid
             */

            Console.WriteLine("   +--------------+\n" +
                "   |.------------.|\n" +
                "   || AEB System ||\n" +
                "   ||            ||\n" +
                "   || 1. Entrar  ||\n" +
                "   || 2. Sair    ||\n" +
                "   |+------------+|\n" +
                "   +-..--------..-+\n" +
                "   .--------------.\n" +
                "  // ============ \\ \n" +
                " // ============== \\ \n" +
                "//__________________\\ \n");
            int op = 0;
            while(true)
            {
                try
                {
                    op = int.Parse(Console.ReadLine());
                    if (op == 2)
                    {
                        Console.WriteLine("Fechando o programa...");
                        Environment.Exit(0);
                    }
                    else if (op == 1)
                    {
                        while (true)
                        {
                            string grid_total = "";
                            string posicao_inicial = "";
                            string direcao_atual = "";
                            string instrucoes = "";

                            Console.Clear();
                            Console.WriteLine("Bem vindo a central de controle da AEB - Aguardando instruções...");
                            Console.Write("Digite as coordenadas do campo total (Ex: 5 5): ");
                            
                            grid_total = Console.ReadLine();
                            int[] grid_final = formatarString(grid_total);

                            Console.Write("\nDigite a posição inicial do robô (Ex: 1 2): ");
                            posicao_inicial = Console.ReadLine();
                            int[] posicao_final = formatarString(posicao_inicial);
                            if(posicao_final[0] > grid_final[0] || posicao_final[1] > grid_final[1])
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Posição inicial maior que o campo total, aperte qualquer tecla para digitar novamente...");
                                Console.ReadKey();
                                Console.ResetColor();
                                continue;
                            }
                            else if(posicao_final[0] < 0 || posicao_final[1] < 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Posição inicial menor que o campo total, aperte qualquer tecla para digitar novamente...");
                                Console.ReadKey();
                                Console.ResetColor();
                                continue;
                            }

                            Console.Write("\nOrientação do robô N = norte, S = sul, L = leste, O = oeste\nDigite a direção inicial do robô (Ex: N): ");
                            direcao_atual = Console.ReadLine().ToUpper();
                            Console.Write("\nInstruções: E - 90 graus para esquerda, D - 90 graus para direita e M - se mover na direção que estiver olhando.\nDigite as instruções para o robô (Ex: EMEMEMEMM): ");
                            instrucoes = Console.ReadLine().ToUpper();

                            char[] instrucoes_char = instrucoes.ToCharArray();

                            for (int i = 0; i < instrucoes_char.Length; i++)
                            {
                                if (instrucoes_char[i] == 'M')
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    if (instrucoes_char[i] == 'M' && direcao_atual == "N")
                                    {
                                        posicao_final[1] += 1; // Não pode ultrapassar o limite maximo da grid
                                        if (posicao_final[1] > grid_final[1])
                                        {
                                            posicao_final[1]--;
                                            Console.WriteLine("Ultrapassando o limite de {0}\nCoordenada atual de Y: {1}", grid_final[1], posicao_final[1]);
                                        }
                                    }
                                    else if (instrucoes_char[i] == 'M' && direcao_atual == "S")
                                    {
                                        posicao_final[1] -= 1; //Não pode ser menor que 0
                                        if (posicao_final[1] < 0)
                                        {
                                            posicao_final[1]++;
                                            Console.WriteLine("Ultrapassando o limite minimo (0)\nCoordenada atual de Y: {0}", posicao_final[1]);
                                        }
                                    }
                                    else if (instrucoes_char[i] == 'M' && direcao_atual == "L")
                                    {
                                        posicao_final[0] += 1; // Não pode ultrapassar o limite maximo da grid
                                        if (posicao_final[0] > grid_final[0])
                                        {
                                            posicao_final[0]--;
                                            Console.WriteLine("Ultrapassando o limite de {0}\nCoordenada atual de Y: {1}", grid_final[0], posicao_final[0]);
                                        }
                                    }
                                    else if (instrucoes_char[i] == 'M' && direcao_atual == "O")
                                    {
                                        posicao_final[0] -= 1; //Não pode ser menor que 0
                                        if (posicao_final[0] < 0)
                                        {
                                            posicao_final[0]++;
                                            Console.WriteLine("Ultrapassando o limite minimo (0)\nCoordenada atual de Y: {0}", posicao_final[0]);
                                        }
                                    }
                                    Console.ResetColor();
                                }
                                else if (instrucoes_char[i] == 'E')
                                {
                                    if (direcao_atual == "N")
                                    {
                                        direcao_atual = "O";
                                    }
                                    else if (direcao_atual == "O")
                                    {
                                        direcao_atual = "S";
                                    }
                                    else if (direcao_atual == "S")
                                    {
                                        direcao_atual = "L";
                                    }
                                    else if (direcao_atual == "L")
                                    {
                                        direcao_atual = "N";
                                    }
                                }
                                else if (instrucoes_char[i] == 'D')
                                {
                                    if (direcao_atual == "N")
                                    {
                                        direcao_atual = "L";
                                    }
                                    else if (direcao_atual == "O")
                                    {
                                        direcao_atual = "N";
                                    }
                                    else if (direcao_atual == "S")
                                    {
                                        direcao_atual = "O";
                                    }
                                    else if (direcao_atual == "L")
                                    {
                                        direcao_atual = "S";
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n\nPosição X final: " + posicao_final[0]);
                            Console.WriteLine("Posição Y final: " + posicao_final[1]);
                            Console.WriteLine("Direção final: " + direcao_atual + "\n");
                            Console.ResetColor();
                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("Deseja sair?\n1 - Não\n0 - Sim");
                                    int sair = int.Parse(Console.ReadLine());
                                    if (sair == 0)
                                    {
                                        Console.WriteLine("Fechando o programa...");
                                        Environment.Exit(0);
                                    }
                                    else if (sair == 1)
                                    {
                                        break;
                                    }
                                }
                                catch
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ocorreu um erro, digite novamente...");
                                    Console.ResetColor();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ocorreu um erro, digite novamente...");
                    Console.ResetColor();
                    break;
                }
            }
            Console.WriteLine("Fechando o programa...");
        }
    }
}
