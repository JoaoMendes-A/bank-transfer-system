using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics.Contracts;
class Exercicio
{
    public class ContaBancaria
    {
        public string Titular { get; set; }
        public int Id;
        private double Saldo;

        public ContaBancaria(string titular, int id, double saldo)
        {
            Titular = titular;
            Id = id;
            Saldo = saldo;
        }

        public ContaBancaria()
        {
            Titular = "";
            Id = 0;
            Saldo = 0;
        }

        public double GetContaBancaria()
        {
            return Saldo;
        }

        //Função para deposito

        public void Depositar(double valor)
        {
            if (valor <= 0)
            {
                Console.WriteLine($"O valor {valor} é invalido. ");
            } else
            {
                Saldo += valor;
                Console.WriteLine("Deposito realizado com sucesso! ");
            }
        }

        // Função para saque
        public void Sacar(double valor)
        {
            if(Saldo - valor < 0)
            {
                Console.WriteLine("Erro! saldo insuficiente");
            } else
            {
                Saldo -= valor;
                Console.WriteLine("Saque realizado com sucesso! ");
            }
        }

        public void TransferirValor(double valor, int idTranferir)
        {
            if (Saldo - valor <= 0)
            {
                Console.WriteLine("Erro! saldo insuficiente para a transferencia");
            } else
            {
                Saldo -= valor;
                Console.WriteLine($"Transferencia realizado com sucesso para o ID: {idTranferir}");
            }
        }

        public void ReceberValorTransferido(double valor, int idReceber)
        {
            if (valor <= 0)
            {
                Console.WriteLine("Erro valor negativo");
            } else
            {
            Saldo += valor;
            Console.WriteLine($"Transferencia recebida do ID: {idReceber}");
            }
        }

    }

    static void Main()
    {

        string[] sim = {"SIM", "SI", "S", "YES", "Y", "CLARO"};

        // Armazena as contas

        List<ContaBancaria> contas = new List<ContaBancaria>();

        Console.WriteLine("Quantas contas deseja criar? ");

        int quantidade = int.Parse(Console.ReadLine() ?? "0");

        for(int i = 0; i < quantidade; i++)
        {
            Console.WriteLine($"Digite o nome da conta Nº {i + 1}");
            Console.WriteLine("Nome: ");

            string nome = Console.ReadLine() ?? "0";

            contas.Add(new ContaBancaria {Titular = nome, Id = GerarId()});
        }

        foreach (ContaBancaria p in contas)
        {
            Console.WriteLine($"Nome: {p.Titular} | ID: {p.Id}");
        }

        bool rodando = true;

        while(rodando)
        {
            Console.WriteLine("Selecione uma das opções: ");
            Console.WriteLine("1- Depositar..");
            Console.WriteLine("2- Sacar......");
            Console.WriteLine("3- Transferir.");
            Console.WriteLine("4- Sair.......");
            
            int escolha = int.Parse(Console.ReadLine() ?? "0");

            switch (escolha)
            {
                case 1:
                    if (escolha == 1)
                    {
                    Console.WriteLine("Digite o ID da conta: ");
                    int idCheck = int.Parse(Console.ReadLine()?? "0");
                    SacarConta(contas, idCheck);
                    }
                    break;

                case 2:
                    if (escolha == 2)
                    {
                    Console.WriteLine("Digite o ID da conta: ");
                    int idCheck = int.Parse(Console.ReadLine() ?? "0");
                    DepositarConta(contas, idCheck);
                    }
                    break;

                case 3:
                    if (escolha == 3)
                    {
                    Console.WriteLine("Valor a transferir");
                    double valor = int.Parse(Console.ReadLine());

                    Console.WriteLine("Digite o id da conta a receber: ");
                    int idReceber = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Digite o id da sua conta a transferir: ");
                    int idTransferir = int.Parse(Console.ReadLine() ?? "0");

                    Transferencia(contas, idTransferir, idReceber, valor);
                    }
                    break;

                case 4:
                    Console.WriteLine("Programa encerrado. ");
                    rodando = false;
                    break;

                default:
                    Console.WriteLine("Opção invalida. ");
                    rodando = false;
                    break;
            }
        }
    }

    private static Random rnd = new Random();

    public static int GerarId()
    {
        return rnd.Next(100, 200);
    }

    public static ContaBancaria ProcurarContaPorId(List<ContaBancaria> contas, int id)
    {
        foreach (ContaBancaria p in contas)
        {
            if (id == p.Id)
            {
                return p;
            } 
        }
        return null;
    }

    public static ContaBancaria ValidarConta(List<ContaBancaria> contas, int idCheck)
    {
        ContaBancaria ContaSelecionada = ProcurarContaPorId(contas, idCheck);

        if(ContaSelecionada == null)
        {
            Console.WriteLine("ID não identificado.");
            return null;
        } 
            return ContaSelecionada;
    }

    public static void DepositarConta(List<ContaBancaria> contas, int idCheck)
    {
        ContaBancaria conta = ValidarConta(contas, idCheck);
        if (conta != null)
        {
            Console.WriteLine("Digite o valor a depositar: ");
            double valor = double.Parse(Console.ReadLine() ?? "0");
            conta.Depositar(valor);
        }
    }

    public static void SacarConta(List<ContaBancaria> contas, int idCheck)
    {
        ContaBancaria conta = ValidarConta(contas, idCheck);
        if (conta != null)
        {
            Console.WriteLine("Digite o valor a sacar: ");
            double valor = double.Parse(Console.ReadLine() ?? "0");
            conta.Sacar(valor);
        }
    }



    static void Transferencia(List<ContaBancaria> contas, int IdTransferir, int IdReceber, double valor)
    {
        if (ValidarConta(contas, IdTransferir) != null && ValidarConta(contas, IdReceber) != null)
        {
            TransferirValor(contas, IdTransferir, valor);
            ReceberTransferencia(contas, IdReceber, valor);
        } else
        {
            Console.WriteLine("Erro! id não encontrado");
        }
    }

    public static void ReceberTransferencia(List<ContaBancaria> contas, int idCheck, double valor)
    {
        ContaBancaria conta = ValidarConta(contas, idCheck);
        if (conta != null)
        {
            conta.ReceberValorTransferido(valor, idCheck);
        }
    }


    public static void TransferirValor(List<ContaBancaria> contas, int idCheck, double valor)
    {
        ContaBancaria conta = ValidarConta(contas, idCheck);
        if (conta != null)
        {
            conta.TransferirValor(valor, idCheck);
        }
    }


}