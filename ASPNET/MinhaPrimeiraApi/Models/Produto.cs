namespace MinhaPrimeiraApi.Models
{
    public class Produto
    {
        // Propriedades => get (lido) set (escrito)
        public int Id { get; set; }
        public string? Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public DateOnly DataValidade { get; set; }

        // Construtor
        public Produto(int id, string nome, decimal preco, int quantidadeEmEstoque, DateOnly dataValidade)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            QuantidadeEmEstoque = quantidadeEmEstoque;
            DataValidade = dataValidade;
        }
        
        public Produto() { }
    }
}