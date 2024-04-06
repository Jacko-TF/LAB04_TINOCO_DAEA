using LAB04_TINOCO_DAEA.models;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LAB04_TINOCO_DAEA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1504-15\\SQLEXPRESS;Initial Catalog=NeptunoDB;User Id=jacko;Password=admin123";

            List <Producto> productos = new List<Producto>();

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                SqlCommand command = new SqlCommand("USP_ListProducts", connection);

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32("idproducto");
                    string nom =  reader.GetString("nombreProducto");
                    string cant = reader.GetString("cantidadPorUnidad");
                    decimal pre = reader.GetDecimal("precioUnidad");
                    string cat;
                    if (!reader.IsDBNull("categoriaProducto"))
                    {
                        cat = reader.GetString("categoriaProducto");
                    }
                    else
                    {
                        cat = null;
                    }

                    productos.Add(new Producto(id, nom, cant, pre, cat));
                }

                connection.Close();

                dgProductos.ItemsSource = productos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1504-15\\SQLEXPRESS;Initial Catalog=NeptunoDB;User Id=jacko;Password=admin123";

            List<Categoria> categorias = new List<Categoria>();

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                SqlCommand command = new SqlCommand("USP_ListCategories", connection);

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32("idcategoria");
                    string nom = reader.GetString("nombrecategoria");
                    string des = reader.GetString("descripcion");
                    bool act = reader.GetBoolean("Activo");
                    string cod = reader.GetString("CodCategoria");

                    categorias.Add(new Categoria(id, nom, des, act, cod));
                }

                connection.Close();

                dgCategorias.ItemsSource = categorias;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}