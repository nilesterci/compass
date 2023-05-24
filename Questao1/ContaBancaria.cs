using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public int numero { get; set; }
        public string titular { get; set; }

        public double? depositoInicial1 { get; set; } = 0;

        public override string ToString()
        {
            return $"Conta {this.numero}, Titular: {this.titular}, Saldo: {(this.depositoInicial1 != null ? this.depositoInicial1?.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")) : "0.00")}";
        }

        public ContaBancaria(int numero, string titular)
        {
            this.numero = numero;
            this.titular = titular;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial1)
        {
            this.numero = numero;
            this.titular = titular;
            this.depositoInicial1 = depositoInicial1;
        }



        internal void Deposito(double quantia)
        {
            this.depositoInicial1 += quantia;
        }

        internal void Saque(double quantia)
        {
            this.depositoInicial1 -= quantia + 3.50;
        }
    }
}
