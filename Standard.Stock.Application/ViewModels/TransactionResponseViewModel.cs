using Newtonsoft.Json;
using Standard.Stock.Domain.Enuns;
using System;
using System.Runtime.Serialization;

namespace Standard.Stock.Application.ViewModels
{
    [DataContract]
    public class TransactionResponseViewModel
    {
        [DataMember(Name = "transactionId")]
        public Guid TransactionId { get; set; }

        [DataMember(Name = "mainTransactionId")]
        public Guid? MainTransactionId { get; set; }

        [DataMember(Name = "initials")]
        public string Initials { get; set; }

        [JsonIgnore]
        public TransactionType Type { get; set; }

        [DataMember(Name = "type")]
        public string OperationType { get => Type == TransactionType.Buy ? "Compra" : "Venda"; }

        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "isComplete")]
        public bool IsComplete { get; set; }

        [DataMember(Name = "create")]
        public DateTime Create { get; set; }
    }
}
