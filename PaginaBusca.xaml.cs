using System.Collections.ObjectModel;
using MauiAppMinhasCompras.Models; // Ajuste para o namespace do seu projeto

namespace MauiAppMinhasCompras.Views;

public partial class PaginaBusca : ContentPage
{
    // ObservableCollection para notificar a interface automaticamente
    ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();

    public PaginaBusca()
    {
        InitializeComponent();

        // Carrega os dados iniciais (simulando a busca do SQLite da aplicação)
        CarregarProdutos();
    }

    private void CarregarProdutos()
    {
        // Aqui no seu projeto real, você buscaria do SQLite: 
        // ex: List<Produto> temp = App.Db.GetAll();

        var produtosDoBanco = new List<Produto>
        {
            new Produto { Descricao = "Arroz 5kg", Preco = 25.90m },
            new Produto { Descricao = "Feijão Preto", Preco = 8.50m },
            new Produto { Descricao = "Óleo de Soja", Preco = 6.99m },
            new Produto { Descricao = "Macarrão Espaguete", Preco = 4.50m }
        };

        lista_produtos.Clear();
        foreach (var p in produtosDoBanco)
        {
            lista_produtos.Add(p);
        }

        // Vincula a ObservableCollection na ListView
        lst_produtos.ItemsSource = lista_produtos;
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            // Se o campo de busca estiver vazio, recarrega todos
            if (string.IsNullOrWhiteSpace(q))
            {
                CarregarProdutos();
            }
            else
            {
                // Faz a filtragem dinâmica em tempo real
                // No seu banco SQLite real, você faria uma query com LIKE: 
                // ex: App.Db.Search(q)
                var resultado = lista_produtos
                    .Where(p => p.Descricao.ToLower().Contains(q.ToLower()))
                    .ToList();

                lst_produtos.ItemsSource = resultado;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}