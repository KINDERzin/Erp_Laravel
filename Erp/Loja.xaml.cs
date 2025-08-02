using Microsoft.Maui.Controls;

namespace Erp
{
    public partial class Loja : ContentPage
    {
        public Loja()
        {
            InitializeComponent();
        }

        private void Comprar_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Compra", "Item adicionado ao carrinho!", "OK");
        }
    }
}